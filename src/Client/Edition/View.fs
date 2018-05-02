module Edition.View

open Types
open Model
open AddressFields
open Fields
open Shared

open ValidationState

open Fable.Helpers.React

open Fulma
open Fulma.FontAwesome

let private validationColor validationValue =
    match validationValue.state with
    | Validated -> Color.IsSuccess
    | Errors _ -> Color.IsDanger

let private validationIcon validationValue =
    match validationValue.state with
    | Validated -> Fa.I.Check
    | Errors _ -> Fa.I.Warning

let private validationMessage validationValue =
    let color = validationColor validationValue
    match validationValue.state with
    | Validated -> div [] []
    | Errors err ->
        Help.help
            [ Help.Color color ]
            (err |> List.map str)

let textWithValidation validationValue onChange =
    let color = validationColor validationValue
    let icon = validationIcon validationValue
    let message = validationMessage validationValue

    Field.div []
        [ Control.div
            [ Control.HasIconRight ]
            [ Input.text
                [ Input.Color color
                  Input.Value validationValue.value
                  Input.OnChange (fun e -> onChange e.Value) ]
              Icon.faIcon [ Icon.Size IsSmall; Icon.IsRight ] [ Fa.icon icon ] ]

          message ]

let numberWithValidation validationValue onChange =
    let color = validationColor validationValue
    let icon = validationIcon validationValue
    let message = validationMessage validationValue
    let strToInt s =
        match System.Int32.TryParse s with
        | false, _ -> None
        | true, i -> Some i
    let intToStr (i: int option) =
        match i with
        | None -> ""
        | Some i -> string i

    Field.div []
        [ Control.div
            [ Control.HasIconRight ]
            [ Input.number
                [ Input.Color color
                  Input.Value (validationValue.value |> intToStr)
                  Input.OnChange (fun e -> onChange (e.Value |> strToInt)) ]
              Icon.faIcon [ Icon.Size IsSmall; Icon.IsRight ] [ Fa.icon icon ] ]

          message ]

let addressEdition (fields: AddressFields) dispatch =
    Field.div []
        [ Label.label [] [ str "Address" ] 
          
          Columns.columns []
            [ Column.column [] [
                numberWithValidation
                    fields.number
                    (NumberChanged >> dispatch) ]
              Column.column [ Column.Width (Column.All, Column.IsFourFifths) ] [
                textWithValidation
                    fields.street
                    (StreetChanged >> dispatch) ]
            ]

          Columns.columns []
            [ Column.column [] [
                textWithValidation
                    fields.postalCode
                    (PostalCodeChanged >> dispatch) ]
              Column.column [] [
                textWithValidation
                    fields.city
                    (CityChanged >> dispatch) ]
            ] ]

let nameEdition (fields: Fields) dispatch =
    Field.div []
        [ Label.label [] [ str "Name" ]
        
          Columns.columns []
            [ Column.column [] [
                textWithValidation
                    fields.firstName
                    (FirstNameChanged >> dispatch) ]
              Column.column [] [
                textWithValidation
                    fields.lastName
                    (LastNameChanged >> dispatch) ]
            ] ]

let root (model: Model) dispatch =
    let save () =
        let dispatchSave = Loading >> Save >> dispatch        

        let address = {
            number = model.fields.address.number.value |> Option.get
            street = model.fields.address.street.value
            postalCode = model.fields.address.postalCode.value
            city = model.fields.address.city.value
        }

        dispatchSave {
            firstName = model.fields.firstName.value
            lastName = model.fields.lastName.value
            address = address
        }
        
    form []
        [ nameEdition model.fields dispatch
          addressEdition model.fields.address (AddressChanged >> dispatch)
            
          Columns.columns []
            [ Column.column []
                [ Button.button
                    [ Button.OnClick (fun _ -> dispatch GoBackToPersons)]
                    [ str "Cancel" ] ]

              Column.column [ ]
                [ Button.button
                    [ Button.IsFullWidth
                      Button.IsLoading model.saving
                      Button.Disabled (model.canSave |> not)
                      Button.Color IsPrimary
                      Button.OnClick (fun _ -> save ()) ]
                    [ str "Save" ] ] ] ]