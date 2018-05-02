module Edition.State

open Types
open AppNavigation
open Elmish
open Model

let init () =
    let fields = Fields.init ()
    { saving = false
      canSave = Fields.canSave fields
      fields = fields
    }, Cmd.none

let private updateWhenSave l model =
    match l with
    | Loading p ->
        { model with saving = true },
        CmdExt.ofAsyncToLoadable
            Server.api.create
            p
            Save
    | Loaded _ ->
        { model with
            saving = false
            fields = Fields.init () }, newUrl PersonsPage
    | _ ->
        { model with saving = false }, newUrl PersonsPage

let updateWhenAddressChanged msg model =
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
        model, newUrl PersonsPage
    | Save l ->
        updateWhenSave l model
    | FirstNameChanged n ->
        Model.setFirstName n model, Cmd.none
    | LastNameChanged n ->
        Model.setLastName n model, Cmd.none
    | AddressChanged a ->
        updateWhenAddressChanged a model