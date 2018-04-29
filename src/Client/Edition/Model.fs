module Edition.Model

open Fields

type Model = {
    saving: bool
    canSave: bool
    fields: Fields
}

let private updateFieldsWith fields model =
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