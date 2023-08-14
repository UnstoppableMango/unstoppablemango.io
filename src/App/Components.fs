[<AutoOpen>]
module Components

open Sutil

let page children = Html.divc "h-screen overflow-y-scroll" children
