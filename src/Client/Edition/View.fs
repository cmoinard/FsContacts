module Edition.View

open Types
open Shared

open ValidationState

open Fable.Helpers.React

open Fulma
open Fulma.FontAwesome

let nameEdition =
    div [] [
        Input.text [ ]
        Input.text []
    ]

let textWithValidation validationValue onChange =
    let color =
        match validationValue.state with
        | Validated -> Color.IsSuccess
        | Errors _ -> Color.IsDanger

    let icon = 
        match validationValue.state with
        | Validated -> Fa.I.Check
        | Errors _ -> Fa.I.Warning

    let message =
        match validationValue.state with
        | Validated -> div [] []
        | Errors err -> Help.help [ Help.Color color ] [ str err ]

    Field.div []
        [ Control.div
            [ Control.HasIconRight ]
            [ Input.text
                [ Input.Color color
                  Input.Value validationValue.value
                  Input.OnChange (fun e -> onChange e.Value) ]
              Icon.faIcon [ Icon.Size IsSmall; Icon.IsRight ] [ Fa.icon icon ] ]

          message ]

let root (model: Model) dispatch =
    let dummyPerson = {
        firstName = "John"
        lastName = "Doe"
    }

    form []
        [ Field.div []
            [ Label.label [] [ str "Name" ] 
              
              Columns.columns []
                [ Column.column [] [
                    textWithValidation
                        model.fields.firstName
                        (FirstNameChanged >> dispatch) ]
                  Column.column [] [
                    textWithValidation
                        model.fields.lastName
                        (LastNameChanged >> dispatch) ]
                ] ]

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
                      Button.OnClick (fun _ -> dispatch (Save (Loading dummyPerson))) ]
                    [ str "Save" ] ] ] ]