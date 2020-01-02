module Client.Interop

open Elmish

type Model = {
    Title: string
}

type Msg =
    | BtnClicked

type Props = {
    Model: Model
    Dispatch: Msg -> unit
}
    
let init _ = { Title = "Interop page!" }, Cmd.none

open Client.Utils
open Client.Experiments
open Fable.React

let view = functionalComponent "Interop" (fun (props: Props) ->
    div [] [
        h1 [] [ str props.Model.Title ]
        h4 [] [ str CustomFileInterop.run ]
    ]
)
