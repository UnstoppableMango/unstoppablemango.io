module App

open Browser
open Sutil

let versions = {| fontAwesome = "6.3.0" |}

[
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/fontawesome.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/brands.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/solid.min.css"
    "tailwind.css"
]
|> List.iter (DomHelpers.setHeadStylesheet Dom.document)

DomHelpers.setHeadTitle Dom.document "UnstoppableMango"

let app () =
    Html.divc "h-screen bg-cover bg-center lg:bg-right bg-[url(images/hbg-md.webp)] bg-byzantium-200" [
        Html.divc "h-full grid content-center" [
            Html.divc "w-full py-5 backdrop-blur bg-gradient-to-r from-transparent via-[rgb(0,0,0,0.75)] to-transparent" [
                Html.divc "h-full flex flex-wrap items-center justify-center gap-6" [
                    Html.imgc "h-48 rounded-full" [
                        Attr.src "images/panigel.png"
                    ]
                    Html.divc "flex flex-col justify-center items-center font-mono" [
                        Html.spanc "text-4xl lg:text-5xl text-thistle-50 py-1" [
                            text "UnstoppableMango"
                        ]
                        Html.spanc "lg:text-xl text-thistle-200 py-2" [
                            text "Developer | Metalhead | Idiot"
                        ]
                    ]
                ]
            ]
        ]
    ]

app () |> Program.mount
