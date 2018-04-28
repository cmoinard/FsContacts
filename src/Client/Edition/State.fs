module Edition.State

open Types
open AppNavigation
open Elmish

let init () =
    { isBusy = false
      hasError = false }, Cmd.none

let update msg model =
    match msg with
    | GoBackToPersons ->
        model, newUrl PersonsPage
    | Save (Loading _) ->
        { model with isBusy = true },
        CmdExt.ofAsyncToLoadable
            Server.api.create
                ()
                Save
    | Save (Loaded _) ->
        { model with isBusy = false }, newUrl PersonsPage
    | Save (Error _) ->
        { isBusy = false; hasError = true }, Cmd.none