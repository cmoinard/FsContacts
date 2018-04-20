module Persons.View

open Shared

open Fable.Helpers.React

open Fulma.Elements

let personHeader =
    tr  []
        [
          th [] [ str "First name" ]
          th [] [ str "Last name" ]
          th [] [ str "Address" ]
        ] 

let personLine p =
    tr  []
        [
            td [] [ str p.firstName ]
            td [] [ str p.lastName ]
            td [] [ str (Address.toString p.address) ]
        ]

let personsTable persons =
    let lines =
        persons
        |> List.map personLine

    Table.table [ Table.IsHoverable ]
        [ thead [] [ personHeader ]
          tbody [] lines
        ]

let view model dispatch =
    match model with
    | None -> str "Loading persons…"
    | Some persons -> personsTable persons 