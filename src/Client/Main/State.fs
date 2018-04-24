module Main.State

open Types
open Elmish

let init () =
    let (pModel, pCmd) = Persons.State.init ()
    let model' = {
        persons = pModel
    }
    model', Cmd.map PersonsMsg pCmd

let update msg model : Model * Cmd<Msg> =
    match msg with
    | PersonsMsg pMsg ->
        let (pModel, pCmd) = Persons.State.update pMsg model.persons
        let model' = {
            persons = pModel
        }
        model', Cmd.map PersonsMsg pCmd