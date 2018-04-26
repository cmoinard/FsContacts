module AppNavigation 
 
open Elmish.Browser.Navigation 
open Elmish.Browser.UrlParser 
 
type Page = 
| PersonsPage 
| EditionPage
 
let private editionUrlName = "edition" 
let private personsUrlName = "persons" 
 
let private toUrlName = 
    function 
    | PersonsPage -> personsUrlName 
    | EditionPage -> editionUrlName 
 
let private toHash page = 
    "#" + (toUrlName page) 
 
let pageParser: Parser<Page->Page,Page> = 
    oneOf [ 
        map PersonsPage (s personsUrlName) 
        map EditionPage (s editionUrlName) 
    ] 
 
let newUrl page = 
    page 
    |> toHash 
    |> Navigation.newUrl 
 
let modifyUrl page = 
    page 
    |> toHash 
    |> Navigation.modifyUrl