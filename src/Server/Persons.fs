module Server.Persons

open Shared

open Bogus

type private AddressDto() =
    member val Number = 1 with get, set
    member val Street = "" with get, set
    member val PostalCode = "" with get, set
    member val City = "" with get, set
    
type private PersonDto() = 
  member val Id = 1 with get, set
  member val FirstName = "" with get, set
  member val LastName = "" with get, set
  member val Address = AddressDto() with get,set

let private mapToAddress (a: AddressDto) =
    {
        number = a.Number
        street = a.Street
        postalCode = a.PostalCode
        city = a.City
    }

let private mapToPerson id (p: PersonDto) =
    {
        id = id
        firstName = p.FirstName
        lastName = p.LastName
        address = p.Address |> mapToAddress
    }

let repository  =
    let addressGenerator =
        Faker<AddressDto>("fr")
            .Rules( fun f a -> 
                    a.Number <- f.Random.Number(1, 99) 
                    a.Street <- f.Address.StreetName()
                    a.PostalCode <- f.Address.ZipCode()
                    a.City <- f.Address.City())

    let personGenerator =
        Faker<PersonDto>("fr")
            .Rules( fun f p ->
                    p.FirstName <- f.Name.FirstName() 
                    p.LastName <- f.Name.LastName()
                    p.Address <- addressGenerator.Generate() )

    let mutable persons =
        personGenerator.Generate(10)
        |> List.ofSeq
        |> List.mapi (fun i p -> mapToPerson (i + 1) p)

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

        update = fun p id ->
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