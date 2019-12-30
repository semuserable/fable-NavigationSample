module Client.Game

open Elmish
open Fable.React
open Fable.React.Props

type Model = {
    Title: string
    Content: string
}

type Msg =
    | BtnClicked
    
type Props = {
    Model: Model
    Dispatch: Msg -> unit
}
    
let init _ = { Title = "Game page title!"; Content = "Game page content" }, Cmd.none

let update (msg:Msg) model =
    match msg with
    | BtnClicked -> { model with Content = "Game Content clicked!!" }, Cmd.none
    
open Client.Utils

let view = functionalComponent "Game" (fun (props: Props) ->
    div [] [
        h1 [] [ str props.Model.Title ]
        h2 [] [ str props.Model.Content ]
        
        button [ OnClick (fun _ -> props.Dispatch BtnClicked) ] [ str "Click 2" ]
    ]
)