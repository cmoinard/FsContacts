module Edition.Types

open Types

type Model =
    {
        isBusy: bool
        hasError: bool   
    }

type Msg =
| GoBackToPersons
| Save of LoadableResult<unit,unit>