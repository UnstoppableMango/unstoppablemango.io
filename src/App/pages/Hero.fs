module Hero

open Sutil

type NavItem = { Name: string; Href: string }

let items = [
    {
        Name = "GitHub"
        Href = "https://github.com/UnstoppableMango"
    }
    { Name = "Music"; Href = "#/music" }
    {
        Name = "Wishlist"
        Href = "https://www.youtube.com/watch?v=dQw4w9WgXcQ"
    }
    {
        Name = "When in Cannes"
        Href = "#/cannes"
    }
]

let view () =
    Html.divc "h-screen overflow-y-scroll" [
        Html.divc "h-screen grid content-center" [
            Html.divc "w-full py-5 backdrop-blur bg-gradient-to-r from-transparent via-black/75 to-transparent" [
                Html.divc "h-full flex flex-wrap items-center justify-center gap-6" [
                    Html.imgc "h-48 rounded-full" [
                        Attr.src "images/panigel.png"
                    ]
                    Html.divc "flex flex-col justify-center items-center" [
                        Html.spanc "text-4xl lg:text-5xl text-thistle-50 font-bold py-1" [
                            text "UnstoppableMango"
                        ]
                        Html.spanc "lg:text-xl text-thistle-200 py-2 font-mono" [
                            text "Developer | Metalhead | Idiot"
                        ]
                    ]
                ]
            ]
        ]
        Html.divc "flex flex-col items-center gap-5 p-5 mb-32" [
            for item in items do
                Html.a [
                    Attr.addClass
                        "py-3 w-full lg:w-1/2 xl:w-1/3 rounded-full shadow backdrop-blur-2xl bg-white/50 font-bold text-eerie-black text-center uppercase"
                    Attr.href item.Href
                    text item.Name
                ]
        ]
    ]
