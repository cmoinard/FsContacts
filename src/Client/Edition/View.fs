module Edition.View

open Types

open Fable.Helpers.React

open Fulma.Elements

let root (model: Model) dispatch =
    div []
        [
            Button.a
                [ Button.OnClick (fun _ -> dispatch GoBackToPersons)]
                [ str "Cancel" ]
            Button.a
                [ Button.IsLoading model.isBusy
                  Button.OnClick (fun _ -> dispatch (Save (Loading ()))) ]
                [ str "Save" ]
        ]