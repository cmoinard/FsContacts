module CmdExt

open Types
open Elmish

let inline ofAsyncToLoadable task arg toMsg = 
    Cmd.ofAsync 
        task 
        arg 
        (Loaded >> toMsg) 
        (Error >> toMsg) 
 
let inline ofAsyncToLoadableWithParameter task arg toMsg = 
    Cmd.ofAsync 
        task 
        arg 
        (fun _ -> Loaded arg |> toMsg) 
        (Error >> toMsg)
