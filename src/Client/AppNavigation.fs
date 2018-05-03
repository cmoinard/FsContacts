module AppNavigation 
 
open Elmish.Browser.Navigation 
open Elmish.Browser.UrlParser 
 
type Page = 
| PersonsPage
| EditionPage of int
| CreationPage
 
let private editionUrlName = "edition" 
let private personsUrlName = "persons" 
 
let pageParser: Parser<Page->Page,Page> = 
    oneOf [ 
        map PersonsPage (s personsUrlName)
        map EditionPage (s editionUrlName </> i32)
        map CreationPage (s editionUrlName)
    ]

let goToPersonsPage () =
    Navigation.newUrl <| "#" + personsUrlName

let goToCreationPage () =
    Navigation.newUrl <| "#" + editionUrlName

let goToEditionPage (id: int) =
    Navigation.newUrl <| "#" + editionUrlName + "/" + string id