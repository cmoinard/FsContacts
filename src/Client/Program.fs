module Program

open Elmish
open Elmish.React

#if DEBUG
open Elmish.Debug
open Elmish.HMR
#endif

open Fable.Helpers.React
open Fable.Helpers.React.Props

open Shared

open Fulma
open Fulma.Layouts
open Fulma.Elements
open Fulma.Components

open Fulma.BulmaClasses.Bulma.Properties
open Fulma.Extra.FontAwesome

let navBrand =
  Navbar.Brand.div [ ] 
    [ Navbar.Item.a 
        [ Navbar.Item.Props 
            [ Href "https://safe-stack.github.io/"
              Style [ BackgroundColor "#00d1b2" ] ] ] 
        [ img [ Src "https://safe-stack.github.io/images/safe_top.png"
                Alt "Logo" ] ] 
      Navbar.burger [ ] 
        [ span [ ] [ ]
          span [ ] [ ]
          span [ ] [ ] ] ]

let navMenu =
  Navbar.menu [ ]
    [ Navbar.End.div [ ] 
        [ Navbar.Item.a [ ] 
            [ str "Home" ] 
          Navbar.Item.a [ ]
            [ str "Examples" ]
          Navbar.Item.a [ ]
            [ str "Documentation" ]
          Navbar.Item.div [ ]
            [ Button.a 
                [ Button.Size IsSmall
                  Button.Props [ Href "https://github.com/SAFE-Stack/SAFE-template" ] ] 
                [ Icon.faIcon [ ] 
                    [ Fa.icon Fa.I.Github; Fa.fw ]
                  span [ ] [ str "View Source" ] ] ] ] ]

let containerBox (model : Persons2.Model) (dispatch : Persons2.Msg -> unit) =
    Box.box' [ ]
        [ Form.Field.div [ Form.Field.IsGrouped ] 
            [ Form.Control.p [ Form.Control.CustomClass "is-expanded"] 
                [ Persons2.view model dispatch ]
            ]
        ]

let view (model : Persons2.Model) (dispatch : Persons2.Msg -> unit) =
  Hero.hero 
    [ Hero.IsFullHeight
      Hero.IsBold ]
    [ Hero.head [ ]
        [ Navbar.navbar [  ]
            [ Container.container [ ]
                [ navBrand
                  navMenu ] ] ]
      Hero.body [ ]
        [ Container.container 
            [ Container.CustomClass Alignment.HasTextCentered ]
            [ Columns.columns [ Columns.IsVCentered ]
                [ Column.column 
                    [ Column.Width (Column.All, Column.Is5) ]
                    [ Image.image [ Image.Is4by3 ]
                        [ img [ Src "http://placehold.it/800x600" ] ] ]
                  Column.column 
                   [ Column.Width (Column.All, Column.Is5)
                     Column.Offset (Column.All, Column.Is1) ]
                   [ containerBox model dispatch ] ] ] ]
      Hero.foot [ ]
        [ Container.container [ ]
            [ Tabs.tabs [ Tabs.IsCentered ]
                [ ul [ ]
                    [ li [ ]
                        [ a [ ]
                            [ str "And this at the bottom" ] ] ] ] ] ] ]

Program.mkProgram Persons2.init Persons2.update view
#if DEBUG
|> Program.withConsoleTrace
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run
