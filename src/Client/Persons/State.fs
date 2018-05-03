module Persons.State

open Types
open Elmish
open AppNavigation

let init () : Model * Cmd<Msg> =
    let model = None
    let cmd =
        Cmd.ofMsg (LoadPersons (Loading ()))
    model, cmd

let update (msg: Msg) (model: Model) : Model * Cmd<Msg> =
    let toNormal p = { person = p ; isBusy = false }
    let deletePerson p ps =
        if ps.person = p
        then { ps with isBusy = true}
        else ps

    let cmd =
        match msg with
        | LoadPersons (Loading _) -> 
            CmdExt.ofAsyncToLoadable
                Server.api.getAll
                ()
                LoadPersons
        | Delete (Loading p) ->
            CmdExt.ofAsyncToLoadableWithParameter
                Server.api.delete
                p
                Delete
        | GoToPersonCreation ->
            goToCreationPage ()
        | GoToPersonEdition id ->
            goToEditionPage id
        | _ ->
            Cmd.none

    let model' =
        match msg, model with
        | LoadPersons (Loading _), _ ->
            None
        | LoadPersons (Loaded persons), _ ->
            Some (
                persons
                |> List.sortBy (fun p -> p.id)
                |> List.map toNormal)
        | Delete (Loading personToDelete), Some persons ->
            Some (
                persons
                |> List.map (deletePerson personToDelete))
        | Delete (Loaded deletedPerson), Some persons ->
            Some (
                persons
                |> List.filter (fun ps -> ps.person <> deletedPerson))
        | _ -> model

    model', cmd