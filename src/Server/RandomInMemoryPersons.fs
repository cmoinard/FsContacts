module Server.RandomInMemoryPersons

open Shared

open Bogus

let repository  =
    let addressGenerator =
        Faker<Address>("fr")
            .CustomInstantiator(fun f ->
                { number = f.Random.Number(1, 99)  
                  street = f.Address.StreetName() 
                  postalCode = f.Address.ZipCode() 
                  city = f.Address.City() })

    let personGenerator =
        Faker<Shared.Person>("fr")
            .CustomInstantiator(fun f ->
                { id = f.IndexGlobal
                  firstName = f.Name.FirstName()
                  lastName = f.Name.LastName()
                  address = addressGenerator.Generate() })

    let mutable persons =
        personGenerator.Generate(10) |> List.ofSeq

    let mutable lastId = 
        persons
        |> List.map (fun p -> p.id)
        |> List.max

    {
        getAll = fun () ->
            async {
                do! Async.Sleep(500)
                return persons
            }

        create = fun p ->
            async {
                do! Async.Sleep(500)

                lastId <- lastId + 1

                let newPerson = {
                    id = lastId
                    firstName = p.firstName
                    lastName = p.lastName
                    address = p.address
                }

                persons <-
                    newPerson::persons
            }

        update = fun id p ->
            async {
                do! Async.Sleep(500)

                let editedPerson = {
                    id = id
                    firstName = p.firstName
                    lastName = p.lastName
                    address = p.address
                }

                let edit pToChange p =
                    if p.id = pToChange.id
                    then pToChange
                    else p
                    
                persons <-
                    persons
                    |> List.map (edit editedPerson)
            }

        delete = fun p ->
            async {
                do! Async.Sleep(500)
                
                persons <-
                    persons
                    |> List.filter ((<>) p)
            }
    }