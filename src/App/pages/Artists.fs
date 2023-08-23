module Artists

open Sutil

let view () =
    Html.divc "h-screen bg-eerie-black/50 flex items-center lg:justify-center px-2" [
        Html.divc "lg:w-1/2 flex flex-col lg:gap-2" [
            Html.h1c "text-thistle-50 font-extrabold text-5xl" [
                text "There's gonna be so many artists here you're gonna lose your socks"
            ]
            Html.pc "text-thistle-50 text-xs" [
                text "WIP don't touch"
            ]
        ]
    ]
