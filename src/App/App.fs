module App

open Sutil
open type Feliz.length // For CSS units like px

let app () =
    Html.div [
        Attr.style [
            Css.margin (rem 1)
        ]
        Html.h1 [
            Attr.style [
                Css.width (percent 50)
                Css.margin (zero, auto)
                Css.textAlignCenter
                Css.fontSize 69
            ]
            text "UnstoppableMango IO"
        ]
        Html.h1 [
            Attr.style [
                Css.width (percent 50)
                Css.margin (zero, auto)
                Css.textAlignCenter
            ]
            text "yeet"
        ]
    ]

app () |> Program.mount
