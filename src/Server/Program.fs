open System.IO

open Giraffe
open Saturn

open Fable.Remoting.Giraffe

open Shared

let clientPath = Path.Combine("..","Client") |> Path.GetFullPath
let port = 8085us

let browserRouter = scope {
  get "/" (htmlFile (Path.Combine(clientPath, "index.html")))
}

let personsWebApp =
    remoting Server.Persons.repository {
        use_route_builder Route.builder
    }

let mainRouter =
    scope {
        forward "" browserRouter
        forward "" personsWebApp
    }

let app = application {
    router mainRouter
    url ("http://0.0.0.0:" + port.ToString() + "/")
    memory_cache
    use_static clientPath
    use_gzip
}

run app