module Server.Persons

open Shared

let repository  =
    let address = {
        number = 5
        street = "sesame street"
        postalCode = "12000"
        city = "New Yort"
    }

    let person = {
        firstName = "John"
        lastName = "Doe"
        address = address
    }

    {
        getAll = fun () ->
            async {
                do! Async.Sleep(500)
                return [person]
            }
    }