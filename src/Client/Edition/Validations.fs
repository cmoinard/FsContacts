module Edition.Validations

open ValidationState

let private required label =
    function
    | "" -> Errors <| label + " is required"
    | _ -> Validated

let validateFirstName =
    required "The first name"

let validateLastName =
    required "The last name"