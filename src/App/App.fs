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
    | "#/login" -> Login.view ()
    | "#/music" -> Music.view ()
    | "#/music/artists" -> Artists.view ()
    | "#/wishlist" -> Wishlist.view ()
    | "#/cannes" -> Cannes.view ()
    | "#/v2" -> V2.view ()
    | _ -> Hero.view ()

let app () =
    Auth.dispatch Auth.Login

    // Hidden shortcut: press ']' to toggle the v2 theme showcase.
    document.addEventListener (
        "keydown",
        fun (e: Browser.Types.Event) ->
            let ke = e :?> Browser.Types.KeyboardEvent
            if ke.key = "]" then
                let next =
                    if window.location.hash = "#/v2" then "#/"
                    else "#/v2"
                window.location.hash <- next
    )

    Html.divc "h-screen bg-cover bg-center lg:bg-right bg-[url(images/hbg-sm.webp)] bg-byzantium-200" [
        Navigable.bindHash pages
    ]

app () |> Program.mount
