module Persons.State

open Types
open Elmish

let inline private cmdOfAsyncToLoadable task arg toMsg =
    Cmd.ofAsync
        task
        arg
        (Loaded >> toMsg)
        (Error >> toMsg)

let inline private cmdOfAsyncToLoadableWithParameter task arg toMsg =
    Cmd.ofAsync
        task
        arg
        (fun _ -> Loaded arg |> toMsg)
        (Error >> toMsg)

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
            cmdOfAsyncToLoadable
                Server.api.getAll
                ()
                LoadPersons
        | Delete (Loading p) ->
            cmdOfAsyncToLoadableWithParameter
                Server.api.delete
                p
                Delete
        | _ ->
            Cmd.none

    let model' =
        match msg, model with
        | LoadPersons (Loaded persons), _ ->
            Some (
                persons
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