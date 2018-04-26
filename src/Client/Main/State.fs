module Main.State

open Types
open Elmish
open AppNavigation
open Fable.Import.Browser

let urlUpdate (result: Option<Page>) (model: Model) =
    match result with
    | None ->
        console.error("Error parsing url")
        model, model.page |> modifyUrl
    | Some page ->
        { model with page = page }, Cmd.none

let init page =
    let (pModel, pCmd) = Persons.State.init ()
    let (model, cmd) =
        urlUpdate
            page 
            {
                page = PersonsPage
                persons = pModel
            }
    model, Cmd.batch [
        cmd                
        Cmd.map PersonsMsg pCmd
    ]

let update msg model : Model * Cmd<Msg> =
    match msg with
    | PersonsMsg pMsg ->
        let (pModel, pCmd) = Persons.State.update pMsg model.persons
        let model' =
            { model with
                persons = pModel
            }
        model', Cmd.map PersonsMsg pCmd
