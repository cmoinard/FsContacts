module Persons2

open Shared

open Elmish

open Fable.Helpers.React

open Fulma.Elements

type Model = Person list option

type Msg =
| Init of Result<Person list, exn>


module Server = 
    open Fable.Remoting.Client
  
    let api : PersonRepository = 
        Proxy.remoting<PersonRepository> {
            use_route_builder Route.builder
        }
    
let init () : Model * Cmd<Msg> =
    let model = None
    let cmd =
        Cmd.ofAsync 
            Server.api.getAll
            () 
            (Ok >> Init)
            (Error >> Init)
    model, cmd

let update (msg: Msg) (model: Person list option) : Model * Cmd<Msg> =
    let model' =
        match model,  msg with
        | None, Init (Ok x) -> Some x
        | _ -> None
    model', Cmd.none


let personHeader =
    tr  []
        [
          th [] [ str "first name" ]
          th [] [ str "last name" ]
          th [] [ str "address" ]
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
    