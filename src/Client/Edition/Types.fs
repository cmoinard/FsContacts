module Edition.Types

open Types
open Shared

type AddressEditionMsg =
| NumberChanged of int option
| StreetChanged of string
| PostalCodeChanged of string
| CityChanged of string

type Msg =
| GoBackToPersons
| Save of LoadableResult<PersonForEdition,unit>
| FirstNameChanged of string
| LastNameChanged of string
| AddressChanged of AddressEditionMsg