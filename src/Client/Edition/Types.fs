module Edition.Types

open Types
open Shared

type Msg =
| GoBackToPersons
| Save of LoadableResult<PersonForEdition,unit>
| FirstNameChanged of string
| LastNameChanged of string