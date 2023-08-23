module Login

open Sutil

type Provider = { Name: string; Path: string }

let providers = [
    { Name = "Microsoft"; Path = "aad" }
    { Name = "GitHub"; Path = "github" }
    { Name = "Twitter"; Path = "twitter" }
]

let view () =
    page [
        Html.divc
            "xl:mx-auto xl:w-1/2 px-2 py-6 xl:py-28 flex flex-col gap-10 justify-center h-screen bg-eerie-black/75"
            [
                Html.h1c "xl:mx-auto text-thistle-50 font-bold text-3xl" [
                    text "Login with..."
                ]
                Html.divc "xl:mx-auto xl:w-[90%] flex flex-col gap-3" [
                    for item in providers do
                        Html.a [
                            Attr.addClass
                                "py-3 w-full rounded-full shadow backdrop-blur-2xl bg-white/50 font-bold text-eerie-black text-center uppercase"
                            Attr.href $"/.auth/login/{item.Path}"
                            text item.Name
                        ]
                ]
            ]
    ]
