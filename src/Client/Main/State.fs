module Main.State

open Types
open Elmish
open AppNavigation
open Fable.Import.Browser

let urlUpdate (result: Option<Page>) (model: Model) =
    match result with
    | None ->
        console.error("Error parsing url")
        model, goToPersonsPage ()
        
    | Some page ->
        let model' =
            match page, model.persons with
            | EditionPage id, Some persons ->
                let personToEdit =
                    persons
                    |> List.map (fun ps -> ps.person)
                    |> List.find (fun p -> p.id = id)
                let eModel = Edition.Model.initFromPerson personToEdit
                { model with edition = eModel }
            | _ ->
                { model with edition = Edition.Model.init () }

        let cmd =
            match page with
            | PersonsPage -> Cmd.ofMsg (PersonsMsg (Persons.Types.LoadPersons (Loading ())))
            | _ -> Cmd.none

        { model' with page = page }, cmd

let init page =
    let (pModel, pCmd) = Persons.State.init ()
    let (eModel, eCmd) = Edition.State.init ()
    let (model, cmd) =
        urlUpdate
            page 
            {
                page = PersonsPage
                persons = pModel
                edition = eModel
            }
    model, Cmd.batch [
        cmd                
        Cmd.map PersonsMsg pCmd
        Cmd.map EditionMsg eCmd
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
    | EditionMsg eMsg ->
        let (eModel, eCmd) = Edition.State.update eMsg model.edition
        let model' =
            { model with
                edition = eModel
            }
        model', Cmd.map EditionMsg eCmd


