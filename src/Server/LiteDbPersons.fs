module Server.LiteDbPersons

open Shared

open LiteDB
open LiteDB.FSharp

let repository  =
    let mapper = FSharpBsonMapper()

    use db = new LiteDatabase("persons.db", mapper)
    let personsDb = db.GetCollection<Person>("persons")

    {
        getAll = fun () ->
            async {
                
                let persons =
                    personsDb.FindAll()
                    |> Seq.toList

                do! Async.Sleep(500)
                return persons
            }

        create = fun p ->
            async {
                do! Async.Sleep(500)

                // lastId <- lastId + 1

                let newPerson = {
                    id = 0
                    firstName = p.firstName
                    lastName = p.lastName
                    address = p.address
                }

                personsDb.Insert(newPerson) |> ignore
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

                personsDb.Update(editedPerson) |> ignore
            }

        delete = fun p ->
            async {
                do! Async.Sleep(500)

                personsDb.Delete(BsonValue(p.id)) |> ignore
            }
    }