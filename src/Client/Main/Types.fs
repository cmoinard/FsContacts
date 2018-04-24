module Main.Types

type Model = {
    persons: Persons.Types.Model
}

type Msg =
| PersonsMsg of Persons.Types.Msg