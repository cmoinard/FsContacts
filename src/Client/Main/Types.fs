module Main.Types

open AppNavigation

type Model = {
    page: Page
    persons: Persons.Types.Model
    edition: Edition.Types.Model
}

type Msg =
| PersonsMsg of Persons.Types.Msg
| EditionMsg of Edition.Types.Msg