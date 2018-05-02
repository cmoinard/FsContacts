module Edition.Fields

open ValidationState
open Validations
open AddressFields

type Fields = {
    firstName: ValidatedValue<string>
    lastName: ValidatedValue<string>
    address: AddressFields
}

let getAllValidatedStates fields =
    seq {
        yield fields.firstName.state
        yield fields.lastName.state
        yield! AddressFields.getAllValidatedStates fields.address
    }

let canSave fields =
    fields
    |> getAllValidatedStates
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
      address = AddressFields.init ()
    }