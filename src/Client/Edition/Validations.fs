module Edition.Validations

open ValidationState

let private required label =
    function
    | "" -> Errors <| [label + " is required"]
    | _ -> Validated

let private isHigherOrEqualsTo1 label value =
    if 1 <= value
    then Validated
    else Errors <| [label + " must be higher than 1"]

let private requiredOption label =
    function
    | None -> Errors <| [label + " is required"]
    | _ -> Validated

let validateFirstName =
    required "The first name"

let validateLastName =
    required "The last name"

let validateNumber (number: int option) =
    match number with
    | None -> requiredOption "The number" number
    | Some n -> isHigherOrEqualsTo1 "The number" n

let validateStreet =
    required "The street"

let validatePostalCode =
    required "The postal code"

let validateCity =
    required "The city"