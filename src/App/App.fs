module App

open Browser
open Sutil
open Sutil.Bulma
open Sutil.CoreElements
open type Feliz.length // For CSS units like px

DomHelpers.setHeadTitle Dom.document "UnstoppableMango"

let versions = {|
    bulma = "0.9.4"
    bulmaPrefersDark = "0.1.0-beta.1"
    fontAwesome = "6.3.0"
|}

[
    $"https://cdnjs.cloudflare.com/ajax/libs/bulma/{versions.bulma}/css/bulma.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/bulma-prefers-dark/{versions.bulmaPrefersDark}/bulma-prefers-dark.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/fontawesome.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/brands.min.css"
    $"https://cdnjs.cloudflare.com/ajax/libs/font-awesome/{versions.fontAwesome}/css/solid.min.css"
]
|> List.iter (DomHelpers.setHeadStylesheet Dom.document)

let app () =
    bulma.hero [
        color.isPrimary
        hero.isBold
        hero.isFullheightWithNavbar
        bulma.heroHead [
            bulma.navbar [
                navbar.isFixedTop
                color.hasBackgroundDark
                bulma.container [
                    bulma.navbarBrand.div [
                        bulma.navbarItem.a [
                            bulma.text.span (Html.text "UnstoppableMango")
                        ]
                    ]
                    bulma.navbarMenu [
                        bulma.navbarEnd.div [
                            bulma.navbarItem.div [
                                bulma.button.a [
                                    color.hasTextBlack
                                    Attr.href "https://github.com/UnstoppableMango"
                                    bulma.icon (Html.ic "fab fa-github" [])
                                    Html.span "GitHub"
                                ]
                            ]
                        ]
                    ]
                ]
            ]
        ]
        bulma.heroBody [
            bulma.title.h1 "UnstoppableMango IO"
            bulma.subtitle.p "yeet"
            bulma.columns [
                bulma.column [
                    bulma.card [
                        color.hasBackgroundDanger
                        bulma.cardHeader [
                            bulma.cardHeaderTitle.p "Test"
                        ]
                        bulma.cardContent [
                            Html.text "test"
                        ]
                    ]
                ]
                bulma.column [
                    bulma.card [
                        color.hasBackgroundInfo
                        bulma.cardHeader [
                            bulma.cardHeaderTitle.p "Test"
                        ]
                        bulma.cardContent [
                            Html.text "test"
                        ]
                    ]
                ]
                bulma.column [
                    bulma.card [
                        color.hasBackgroundPrimary
                        bulma.cardHeader [
                            bulma.cardHeaderTitle.p "Test"
                        ]
                        bulma.cardContent [
                            Html.text "test"
                        ]
                    ]
                ]
            ]
        ]
    ]

app () |> Program.mount
