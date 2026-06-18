module Landing

open Fable.Core
open Fable.Core.JsInterop
open Browser
open Browser.Dom
open Browser.Types
open Sutil
open Sutil.Core
open Sutil.CoreElements
open Fetch

let versions = {| fontAwesome = "6.3.0" |}

[
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/fontawesome.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/brands.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/solid.min.css"
    "tailwind.css"
]
|> List.iter (DomHelpers.setHeadStylesheet document)

DomHelpers.setHeadTitle Dom.document "UnstoppableMango"

type CompileState = Idle | Running | Done | Failed of string

let compileState = Store.make Idle
let terminalLines = Store.make ([] : string list)
let iframeUrl = Store.make (None : string option)

let addLine line =
    Store.modify (fun lines -> lines @ [line]) terminalLines

let buildIframeHtml (code: string) =
    let importMap = """{"imports":{"fable-library-js/":"/fable-library-js/"}}"""
    $"""<!doctype html>
<html lang="en">
<head>
  <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <script type="importmap">{importMap}</script>
  <link rel="stylesheet" href="/tailwind.css">
</head>
<body>
  <div id="sutil-app"></div>
  <script type="module">
{code}
  </script>
</body>
</html>"""

let startCompile () =
    Store.set compileState Running
    Store.set terminalLines []
    Store.set iframeUrl None
    addLine "Starting F# compiler..."

    let worker: obj = emitJsExpr () "new Worker('/compiler/worker.min.js')"

    let send (msg: obj[]) =
        worker?postMessage(JS.JSON.stringify(msg))

    worker?onmessage <- fun (ev: MessageEvent) ->
        match ev.data with
        | :? string as json ->
            let data: obj[] = unbox (JS.JSON.parse json)
            let case: string = unbox data.[0]
            match case with
            | "Loaded" ->
                let version: string = unbox data.[1]
                addLine $"Fable compiler {version} ready"
                addLine "Fetching v1 F# source..."
                fetch "/v1-source.json" []
                |> Promise.bind (fun r -> r.json<obj> ())
                |> Promise.tap (fun sourceMap ->
                    let keys: string[] = emitJsExpr sourceMap "Object.keys($0)"
                    let files =
                        keys
                        |> Array.map (fun name -> {| Name = name; Content = (unbox<string> sourceMap?(name)) |})
                    addLine $"Compiling {files.Length} files..."
                    send [| box "CompileFiles"; box files; box "javascript"; box [||] |]
                )
                |> Promise.catchEnd (fun ex ->
                    addLine $"Fetch error: {ex.Message}"
                    Store.set compileState (Failed ex.Message)
                    worker?terminate()
                )

            | "CompilationsFinished" ->
                let codes: string[] = unbox data.[1]
                let errors: obj[] = unbox data.[3]
                let hasErrors = errors |> Array.exists (fun e -> not (unbox<bool> e?IsWarning))

                for err in errors do
                    let msg: string = unbox err?Message
                    let isWarn: bool = unbox err?IsWarning
                    addLine $"""{if isWarn then "⚠" else "✗"} {msg}"""

                if hasErrors then
                    Store.set compileState (Failed "Compilation failed")
                else
                    addLine $"Done — {codes.Length} files compiled"
                    let combined = codes |> String.concat "\n"
                    let html = buildIframeHtml combined
                    let blob: obj = emitJsExpr html "new Blob([$0], {type:'text/html'})"
                    let url: string = emitJsExpr blob "URL.createObjectURL($0)"
                    Store.set iframeUrl (Some url)
                    Store.set compileState Done
                worker?terminate()

            | "LoadFailed" ->
                addLine "Compiler failed to load"
                Store.set compileState (Failed "Load failed")
                worker?terminate()

            | "CompilerCrashed" ->
                let msg: string = if data.Length > 1 then unbox data.[1] else "Unknown error"
                addLine $"Compiler crashed: {msg}"
                Store.set compileState (Failed msg)
                worker?terminate()

            | _ -> ()
        | _ -> ()

let compileButton () =
    Bind.el(compileState, fun state ->
        match state with
        | Idle ->
            Html.buttonc "text-sm px-4 py-2 rounded border border-thistle-600 text-thistle-300 hover:border-thistle-400 hover:text-thistle-100 transition-colors font-mono" [
                Ev.onClick (fun _ -> startCompile())
                text "$ ./launch-classic"
            ]
        | Running ->
            Html.buttonc "text-sm px-4 py-2 rounded border border-thistle-800 text-thistle-600 font-mono cursor-not-allowed" [
                attr("disabled", true)
                text "compiling..."
            ]
        | Done ->
            Html.buttonc "text-sm px-4 py-2 rounded border border-green-700 text-green-400 font-mono cursor-default" [
                text "✓ running"
            ]
        | Failed _ ->
            Html.buttonc "text-sm px-4 py-2 rounded border border-red-700 text-thistle-300 hover:border-red-500 hover:text-white transition-colors font-mono" [
                Ev.onClick (fun _ -> startCompile())
                text "$ ./launch-classic --retry"
            ]
    )

let terminal () =
    Bind.el(compileState, fun state ->
        match state with
        | Idle -> Html.none
        | _ ->
            Html.divc "font-mono text-sm bg-black/60 rounded-lg border border-thistle-800 p-4 max-h-48 overflow-y-auto" [
                Bind.each(
                    terminalLines,
                    fun line ->
                        Html.pc "text-green-400 leading-relaxed" [text line]
                )
                Bind.el(compileState, fun s ->
                    match s with
                    | Failed msg ->
                        Html.divc "mt-3 pt-3 border-t border-thistle-800 flex gap-3 items-center" [
                            Html.spanc "text-red-400 text-xs" [text msg]
                            Html.a [
                                Attr.addClass "text-xs text-thistle-400 hover:text-thistle-200 underline transition-colors"
                                Attr.href "/old"
                                text "load pre-compiled"
                            ]
                        ]
                    | _ -> Html.none
                )
            ]
    )

let iframeView () =
    Bind.el(iframeUrl, fun url ->
        match url with
        | None -> Html.none
        | Some u ->
            Html.iframe [
                attr("class", "w-full rounded-lg border border-thistle-700 bg-eerie-black mt-4")
                attr("src", u)
                attr("style", "height: 600px;")
                attr("sandbox", "allow-scripts allow-same-origin")
            ]
    )

let app () =
    Html.divc "min-h-screen bg-eerie-black text-white flex flex-col" [
        Html.divc "flex-1 flex flex-col justify-center max-w-3xl w-full mx-auto px-8 py-20 gap-16" [

            Html.divc "flex flex-col gap-3" [
                Html.h1 [
                    Attr.addClass "text-5xl font-bold text-thistle-50"
                    text "UnstoppableMango"
                ]
                Html.spanc "text-lg text-thistle-300 font-mono" [
                    text "Developer · Open Source · Metalhead"
                ]
            ]

            Html.divc "flex gap-6" [
                Html.a [
                    Attr.addClass "text-thistle-100 hover:text-white font-semibold transition-colors"
                    Attr.href "https://github.com/UnstoppableMango"
                    Attr.target "_blank"
                    Html.i [ Attr.addClass "fa-brands fa-github mr-2" ]
                    text "GitHub"
                ]
            ]

            Html.divc "flex flex-col gap-6" [
                Html.h2 [
                    Attr.addClass "text-2xl font-semibold text-thistle-100"
                    text "Projects"
                ]
                Html.spanc "text-thistle-400 italic" [
                    text "Coming soon."
                ]
            ]

            Html.divc "flex flex-col gap-4" [
                Html.h2 [
                    Attr.addClass "text-2xl font-semibold text-thistle-100"
                    text "Classic"
                ]
                Html.spanc "text-sm text-thistle-500" [
                    text "Compile and run v1 in the browser — the real F# compiler, running as JavaScript."
                ]
                compileButton ()
                terminal ()
                iframeView ()
            ]

        ]

        Html.divc "border-t border-thistle-700 px-8 py-4 flex justify-between items-center" [
            Html.spanc "text-xs text-thistle-600 font-mono" [ text "F# → Fable → JS" ]
            Html.a [
                Attr.addClass "text-sm text-thistle-500 hover:text-thistle-300 transition-colors"
                Attr.href "/old"
                text "Classic site →"
            ]
        ]
    ]

app () |> Program.mount
