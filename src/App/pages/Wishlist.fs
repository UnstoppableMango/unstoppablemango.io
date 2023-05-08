module Wishlist

open Sutil

let view () =
    Html.divc "h-screen bg-eerie-black/50 flex items-center lg:justify-center px-2" [
        Html.divc "lg:w-1/3 flex flex-col lg:gap-2" [
            Html.h1c "text-thistle-50 font-extrabold text-5xl" [
                text "Spend your money on something useful"
            ]
            Html.a [
                Attr.addClass "text-thistle-50 text-xs"
                Attr.href "https://donate.wikimedia.org"
                text "https://donate.wikimedia.org"
            ]
        ]
    ]
