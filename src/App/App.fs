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
|> List.iter (DomHelpers.setHeadStylesheet document)

DomHelpers.setHeadTitle Dom.document "UnstoppableMango"

let pages =
    function
    | "#/music" -> Music.view ()
    | "#/music/artists" -> Artists.view ()
    | "#/wishlist" -> Wishlist.view ()
    | _ -> Hero.view ()

let app () =
    Html.divc "h-screen bg-cover bg-center lg:bg-right bg-[url(images/hbg-sm.webp)] bg-byzantium-200" [
        Navigable.bindHash pages
    ]

app () |> Program.mount
