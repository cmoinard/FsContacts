module Server.Persons

open Shared

open Bogus

type private AddressDto() =
    member val Number = 1 with get, set
    member val Street = "" with get, set
    member val PostalCode = "" with get, set
    member val City = "" with get, set
    
type private PersonDto() = 
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
let private mapToPerson (p: PersonDto) =
    {
        firstName = p.FirstName
        lastName = p.LastName
        address = p.Address |> mapToAddress
    }

let repository  =
    let addressGenerator =
        Faker<AddressDto>()
            .Rules( fun f a -> 
                    a.Number <- f.Random.Number(1, 99) 
                    a.Street <- f.Address.StreetName()
                    a.PostalCode <- f.Address.ZipCode()
                    a.City <- f.Address.City())

    let personGenerator =
        Faker<PersonDto>()
            .Rules( fun f p -> 
                    p.FirstName <- f.Name.FirstName() 
                    p.LastName <- f.Name.LastName()
                    p.Address <- addressGenerator.Generate() )

    let persons = 
        personGenerator.Generate(10)
        |> List.ofSeq
        |> List.map mapToPerson

    {
        getAll = fun () ->
            async {
                do! Async.Sleep(500)
                return persons
            }
    }