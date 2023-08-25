module Wishlist

open App
open Sutil

type Item = {
    Name: string
    Link: string option
    Image: string option
    Description: string option
}

let items = [
    {
        Name = "Label maker"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Desk lamp"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Mug rack"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Geometric wall light thing"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Under desk RGB lights"
        Link = None
        Image = None
        Description = None
    }
    // { Name = "Behind TV lights"; Link = None; Image = None; Description = None }
    {
        Name = "Surround sound system"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Steel case desk chair"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Candles"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Settle Your Scores, Better Luck Tomorrow Vinyl"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Rosewill 15 bay server chassis"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Vest"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Chainsaw man manga"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Isaac Asimov Books"
        Link = None
        Image = None
        Description = None
    }
    // { Name = "Welcome mat"; Link = None; Image = None; Description = None }
    {
        Name = "Chop sticks"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Gaming headset"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Nice headphones"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Ember travel mug"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Paper towel holder"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "More bed sheets"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Athletic pants"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Tongue scraper"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "UWP sweatpants"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "PS5"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Dualshock 4 controllers"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Top Gear UK"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Slippers"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Spirit Island board game"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Domain Modeling Made Functional by Scott Wlaschin"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "The Road Not Taken - Harry Turtledove"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Kill it with fire book"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Mop and mop accessories"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Deathstalker by Simor R. Green"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Joking Hazard game"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Laser Printer"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Oversized Master Transmuter card/poster/wall art"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Standing mat for desk"
        Link = None
        Image = None
        Description = None
    }
    {
        Name = "Ice packs"
        Link = None
        Image = None
        Description = None
    }
]

let loggedOut () =
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

let wishlist () =
    Html.divc "h-screen overflow-y-scroll p-2 flex flex-col gap-3 bg-eerie-black/50" [
        Html.h1c "text-thistle-50 font-extrabold text-5xl" [
            text "Some things..."
        ]
        Html.pc "text-thistle-50" [
            text "Currently a flat list, descriptions and links to follow. At least there's something 🤷"
        ]
        Html.ulc "px-1" [
            for item in items do
                Html.lic "text-thistle-50" [
                    Html.a [
                        text item.Name
                    ]
                ]
        ]
    ]

let view () =
    Auth.bind (function
        | Auth.Principal p -> wishlist ()
        | _ -> loggedOut ())
