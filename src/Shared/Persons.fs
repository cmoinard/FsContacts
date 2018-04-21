namespace Shared

type Address = {
    number: int
    street: string
    postalCode: string
    city: string
}

module Address =
    let toString address =
        string address.number
        + " " + address.street
        + " " + address.postalCode
        + " " + address.city

type Person = {
    firstName: string
    lastName: string
    address: Address
}

type PersonRepository = {
    getAll: unit -> Async<List<Person>>
    delete: Person -> Async<unit>
}