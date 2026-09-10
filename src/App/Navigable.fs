module Navigable

open Browser
open Sutil
open Sutil.Core
open Sutil.CoreElements

let bindHash<'T> (view: string -> SutilElement) =
    let store = Store.make window.location.hash

    fragment [
        disposeOnUnmount [
            store
            Navigable.listenLocation (id, (fun l -> l.hash |> Store.set store))
            |> Helpers.disposable
        ]
        Bind.el (store, view)
    ]

let nav route = window.location.hash <- route
