module Landing

open Browser
open Sutil
open Sutil.Core

let versions = {| fontAwesome = "6.3.0" |}

[
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/fontawesome.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/brands.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/solid.min.css"
    "tailwind.css"
]
|> List.iter (DomHelpers.setHeadStylesheet document)

DomHelpers.setHeadTitle Dom.document "UnstoppableMango"

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

        ]

        Html.divc "border-t border-thistle-700 px-8 py-4 flex justify-end" [
            Html.a [
                Attr.addClass "text-sm text-thistle-500 hover:text-thistle-300 transition-colors"
                Attr.href "/old"
                text "Classic site →"
            ]
        ]
    ]

app () |> Program.mount
