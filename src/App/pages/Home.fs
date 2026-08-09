module Home

open Sutil

let view () =
    page [
        Html.divc "min-h-screen flex items-center justify-center px-6 py-12" [
            Html.divc
                "w-full max-w-2xl rounded-[2rem] border border-white/20 bg-eerie-black/70 p-10 text-center shadow-2xl backdrop-blur-xl"
                [
                    Html.spanc "text-sm font-bold uppercase tracking-[0.4em] text-thistle-200/80" [
                        text "UnstoppableMango.io"
                    ]
                    Html.h1c "mt-6 text-4xl font-bold text-thistle-50 lg:text-5xl" [
                        text "Work in progress"
                    ]
                    Html.pc "mt-4 text-lg text-thistle-200/90" [
                        text "A new homepage is on the way. In the meantime, you can still visit the previous version."
                    ]
                    Html.divc "mt-8 flex flex-col items-center gap-3" [
                        Html.a [
                            Attr.addClass
                                "inline-flex items-center justify-center rounded-full bg-byzantium-200 px-8 py-3 font-bold uppercase tracking-[0.2em] text-eerie-black transition hover:bg-thistle-50"
                            Attr.href "#/v1"
                            text "Open v1"
                        ]
                        Html.a [
                            Attr.addClass
                                "inline-flex items-center justify-center rounded-full border border-thistle-200/40 px-6 py-2 text-sm font-bold uppercase tracking-[0.2em] text-thistle-100 transition hover:border-thistle-50 hover:text-thistle-50 lg:hidden"
                            Attr.href "#/v2"
                            text "Preview v2 on mobile"
                        ]
                    ]
                ]
        ]
    ]
