module Main.View

open Types

open Fable.Helpers.React
open Fable.Helpers.React.Props

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

let containerBox content =
    Box.box' [ ]
        [ Form.Field.div [ Form.Field.IsGrouped ] 
            [ Form.Control.p [ Form.Control.CustomClass "is-expanded"] 
                [ content ]
            ]
        ]

let view content =
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
                   [ containerBox content ] ] ] ]
      Hero.foot [ ]
        [ Container.container [ ]
            [ Tabs.tabs [ Tabs.IsCentered ]
                [ ul [ ]
                    [ li [ ]
                        [ a [ ]
                            [ str "And this at the bottom" ] ] ] ] ] ] ]



let root model dispatch =
    let personView = Persons.View.view model.persons (PersonsMsg >> dispatch)
    view personView