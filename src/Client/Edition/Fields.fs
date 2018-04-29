module Edition.Fields

open ValidationState
open Validations

type Fields = {
    firstName: ValidatedValue<string>
    lastName: ValidatedValue<string>
}

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