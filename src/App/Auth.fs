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
    abstract clientPrincipal: IClientPrincipal

let me () =
    fetch "/.auth/me" [] |> Promise.bind (fun x -> x.json<IMe> ())

type Message =
    | Error of Exception
    | Success of IMe
    | Login

type Model = { Me: IMe option; Loading: bool }

let private update msg model =
    match msg with
    | Error x -> { model with Loading = false }, Cmd.none
    | Success x ->
        {
            model with
                Loading = false
                Me = Some x
        },
        Cmd.none
    | Login -> { model with Loading = true }, Cmd.OfPromise.either me () Success Error

let private init current =
    { Me = current; Loading = false }, Cmd.none

let model, dispatch = None |> Store.makeElmish init update ignore
