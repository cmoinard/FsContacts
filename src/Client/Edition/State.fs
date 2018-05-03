module Edition.State

open Types
open AppNavigation
open Elmish
open Model

let init () =
    Model.init (), Cmd.none

let private updateWhenSave l model =
    match l with
    | Loading p ->
        let save =
            match model.id with
            | None -> Server.api.create
            | Some id -> Server.api.update id
        
        let model' = { model with saving = true }
        let cmd = CmdExt.ofAsyncToLoadable save p Save

        model', cmd

    | Loaded _ ->
        { model with
            saving = false
            fields = Fields.init () }, goToPersonsPage ()
            
    | _ ->
        { model with saving = false }, goToPersonsPage ()

let private updateWhenAddressChanged msg model =
    match msg with
    | NumberChanged n ->
        Model.setNumber n model, Cmd.none
    | StreetChanged s ->
        Model.setStreet s model, Cmd.none
    | PostalCodeChanged pc ->
        Model.setPostalCode pc model, Cmd.none
    | CityChanged c ->
        Model.setCity c model, Cmd.none

let update msg model =
    match msg with
    | GoBackToPersons ->
        model, goToPersonsPage ()
    | Save l ->
        updateWhenSave l model
    | FirstNameChanged n ->
        Model.setFirstName n model, Cmd.none
    | LastNameChanged n ->
        Model.setLastName n model, Cmd.none
    | AddressChanged a ->
        updateWhenAddressChanged a model