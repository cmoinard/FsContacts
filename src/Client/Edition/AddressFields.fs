module Edition.AddressFields

open Edition.Validations
open ValidationState

type AddressFields = {
    number: ValidatedValue<int option>
    street: ValidatedValue<string>
    postalCode: ValidatedValue<string>
    city: ValidatedValue<string>
}

let getAllValidatedStates fields =
    seq {
        yield fields.number.state
        yield fields.street.state
        yield fields.postalCode.state
        yield fields.city.state
    }

let setNumber number =
    { value = number
      state = validateNumber number
    }

let setStreet street =
    { value = street
      state = validateStreet street
    }

let setPostalCode postalCode =
    { value = postalCode
      state = validatePostalCode postalCode
    }

let setCity city =
    { value = city
      state = validateCity city
    }

let init () =
    { number = setNumber None
      street = setStreet ""
      postalCode = setPostalCode ""
      city = setCity ""
    }