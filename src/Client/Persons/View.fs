module Persons.View

open Shared
open Types

open Fable.Helpers.React

open Fulma.Elements

let personHeader =
    tr  []
        [
          th [] [ str "First name" ]
          th [] [ str "Last name" ]
          th [] [ str "Address" ]
          th [] []
        ] 

let personLine dispatch ps =
    let p = ps.person
    tr  []
        [
            td [] [ str p.firstName ]
            td [] [ str p.lastName ]
            td [] [ str (Address.toString p.address) ]
            td [] [ 
                Button.a 
                    [
                        Button.IsLoading ps.isBusy
                        Button.OnClick (fun _ -> dispatch (Delete (Loading p)))
                    ]
                    [ str "delete"] ]
        ]

let personsTable dispatch persons =
    let lines =
        persons
        |> List.map (personLine dispatch)

    Table.table [ Table.IsHoverable ]
        [ thead [] [ personHeader ]
          tbody [] lines
        ]

let view model dispatch =
    match model with
    | None -> str "Loading persons…"
    | Some persons -> personsTable dispatch persons 