module Persons.State

open Types
open Elmish
open Shared

let init () : Model * Cmd<Msg> =
    let model = None
    let cmd =
        Cmd.ofAsync 
            Server.api.getAll
            () 
            (Ok >> Init)
            (Error >> Init)
    model, cmd

let update (msg: Msg) (model: Person list option) : Model * Cmd<Msg> =
    let model' =
        match model,  msg with
        | None, Init (Ok x) -> Some x
        | _ -> None
    model', Cmd.none