module Edition.Model

open Shared
open Fields
open Edition.AddressFields

type Model = {
    saving: bool
    canSave: bool
    fields: Fields
    id: int option
}

let private updateFieldsWith fields model =
    { model with
        fields = fields
        canSave = Fields.canSave fields }

let private updateAddressFieldsWith addressFields model =
    let fields = { model.fields with address = addressFields }
    { model with
        fields = fields
        canSave = Fields.canSave fields }

let setFirstName firstName model =
    model
    |> updateFieldsWith {
        model.fields with 
            firstName = Fields.setFirstName firstName }

let setLastName lastName model =
    model
    |> updateFieldsWith {
        model.fields with 
            lastName = Fields.setLastName lastName }

let setNumber number model =
    model
    |> updateAddressFieldsWith {
        model.fields.address with
            number = AddressFields.setNumber number }

let setStreet street model =
    model
    |> updateAddressFieldsWith {
        model.fields.address with
            street = AddressFields.setStreet street }

let setPostalCode postalCode model =
    model
    |> updateAddressFieldsWith {
        model.fields.address with
            postalCode = AddressFields.setPostalCode postalCode }

let setCity city model =
    model
    |> updateAddressFieldsWith {
        model.fields.address with
            city = AddressFields.setCity city }

let init () =
    let fields = Fields.init ()
    { saving = false
      canSave = Fields.canSave fields
      fields = fields
      id = None
    }

let initFromPerson (p:Person) =
    let model = init ()

    { model with id = Some p.id }
    |> setFirstName p.firstName
    |> setLastName p.lastName
    |> setNumber (Some p.address.number)
    |> setStreet p.address.street
    |> setPostalCode p.address.postalCode
    |> setCity p.address.city