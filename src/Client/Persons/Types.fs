module Persons.Types

open Shared
open Types

type PersonWithState = {
    person: Person
    isBusy: bool
}

type Model = PersonWithState list option

type Msg =
| LoadPersons of LoadableResult<unit, Person list>
| Delete of LoadableResult<Person, Person>
| GoToPersonCreation
| GoToPersonEdition of int