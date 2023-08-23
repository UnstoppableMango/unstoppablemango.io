module App.Auth

open System
open Fetch
open Sutil

type IClaim =
    abstract typ: string
    abstract ``val``: string

type IClientPrincipal =
    abstract identityProvider: string
    abstract userId: string
    abstract userDetails: string
    abstract userRoles: string list
    abstract claims: IClaim list

type IMe =
    abstract clientPrincipal: IClientPrincipal option

let me () =
    fetch "/.auth/me" [] |> Promise.bind (fun x -> x.json<IMe> ())

type Message =
    | Error of Exception
    | Success of IMe
    | Login

type State = { Me: IMe option; Loading: bool }

let private update msg state =
    match msg with
    | Error x -> { state with Loading = false }, Cmd.none
    | Success x ->
        {
            state with
                Loading = false
                Me = Some x
        },
        Cmd.none
    | Login -> { state with Loading = true }, Cmd.OfPromise.either me () Success Error

let private init current =
    { Me = current; Loading = false }, Cmd.none

let model, dispatch = None |> Store.makeElmish init update ignore

let principal state =
    state.Me |> Option.bind (fun (me: IMe) -> me.clientPrincipal)

let (|Loading|LoggedOut|Principal|) =
    function
    | { Loading = true } -> Loading
    | { Me = Some me } ->
        match me.clientPrincipal with
        | Some p -> Principal p
        | _ -> LoggedOut
    | _ -> LoggedOut

let bind binder = Bind.el (model, binder)
