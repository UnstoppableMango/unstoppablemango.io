module App

open App
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
    | "#/v1/login" -> V1.Login.view ()
    | "#/v1/music" -> V1.Music.view ()
    | "#/v1/music/artists" -> V1.Artists.view ()
    | "#/v1/wishlist" -> V1.Wishlist.view ()
    | "#/v1/cannes" -> V1.Cannes.view ()
    | "#/v1" -> V1.Home.view ()
    | _ -> Home.view ()

let app () =
    Auth.dispatch Auth.Login

    Html.divc "h-screen bg-cover bg-center lg:bg-right bg-[url(images/hbg-sm.webp)] bg-byzantium-200" [
        Navigable.bindHash pages
    ]

app () |> Program.mount
