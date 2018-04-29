module Edition.Types

open Types
open Shared
open ValidationState
open Validations

type Fields = {
    firstName: ValidatedValue<string>
    lastName: ValidatedValue<string>
}

module Fields =
    let private getAllValidatedValues fields =
        seq {
            yield fields.firstName.state
            yield fields.lastName.state
        }

    let canSave fields =
        fields
        |> getAllValidatedValues
        |> Seq.forall ((=) Validated)

    let setFirstName firstName =
        { value = firstName
          state = validateFirstName firstName
        }

    let setLastName lastName =
        { value = lastName
          state = validateLastName lastName
        }

    let init () =
        { firstName = setFirstName ""
          lastName = setLastName ""
        }

type Model = {
    saving: bool
    canSave: bool
    fields: Fields
}

module Model =
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


type Msg =
| GoBackToPersons
| Save of LoadableResult<PersonForEdition,unit>
| FirstNameChanged of string
| LastNameChanged of string