module Edition.Validations

open ValidationState

let private required label =
    function
    | "" -> Errors <| label + " is required"
    | _ -> Validated

let private requiredOption label =
    function
    | None -> Errors <| label + " is required"
    | _ -> Validated

let validateFirstName =
    required "The first name"

let validateLastName =
    required "The last name"

let validateNumber (number: int option) =
    requiredOption "The number" number

let validateStreet =
    required "The street"

let validatePostalCode =
    required "The postal code"

let validateCity =
    required "The city"