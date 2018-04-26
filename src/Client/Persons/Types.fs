module Persons.Types

open Shared

type PersonWithState = {
    person: Person
    isBusy: bool
}

type Model = PersonWithState list option

type LoadableResult<'TLoading, 'TLoaded> =
| Loading of 'TLoading
| Loaded of 'TLoaded
| Error of exn

type Msg =
| LoadPersons of LoadableResult<unit, Person list>
| Delete of LoadableResult<Person, Person>
| GoToPersonCreation