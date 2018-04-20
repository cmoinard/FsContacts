module Persons.Server

open Shared
open Fable.Remoting.Client

let api : PersonRepository = 
    Proxy.remoting<PersonRepository> {
        use_route_builder Route.builder
    }