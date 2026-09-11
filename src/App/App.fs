module App

open Browser
open Fable.Core.JsInterop
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

// Pages open at the top on reload rather than wherever the last visit left off.
window.history?scrollRestoration <- "manual"

// Reconciles the palette and intensity stores with the classes the no-flash
// script in index.html already set.
Pulp.Theme.init ()

let pages =
    function
    | "#/v1/music" -> V1.Music.view ()
    | "#/v1/music/artists" -> V1.Artists.view ()
    | "#/v1/wishlist" -> V1.Wishlist.view ()
    | "#/v1/cannes" -> V1.Cannes.view ()
    | "#/v1" -> V1.Home.view ()
    | "#/v2" -> V2.view ()
    | _ -> Home.view ()

let app () =
    // Hidden shortcut: press ']' to toggle the v2 theme showcase.
    document.onkeydown <-
        fun ke ->
            if ke.key = "]" then
                let next = if window.location.hash = "#/v2" then "#/" else "#/v2"
                window.location.hash <- next

    Html.divc "min-h-screen bg-cover bg-fixed bg-center lg:bg-right bg-[url(images/hbg-sm.webp)] bg-byzantium-200" [
        Navigable.bindHash pages
    ]

app () |> Program.mount
