module Program

open Elmish
open Elmish.React
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open AppNavigation

open Main.State
open Main.View

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

Program.mkProgram init update root
|> Program.toNavigable (parseHash pageParser) urlUpdate
#if DEBUG
|> Program.withConsoleTrace
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
