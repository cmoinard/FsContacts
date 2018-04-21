module Persons.Types

open Shared

type Model = Person list option

type Msg =
| Init of Result<Person list, exn>
| Delete of Person