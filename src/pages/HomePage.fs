module Client.Home

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
    
let init _ = { Title = "Home page title!"; Content = "Home page content" }, Cmd.none

let update (msg: Msg) model =
    match msg with
    | BtnClicked -> { model with Content = "Button Home Content Clicked!" }, Cmd.none

open Client.Utils
    
let view = functionalComponent "Home" (fun (props: Props) ->
    div [] [
        h1 [] [ str props.Model.Title ]
        h2 [] [ str props.Model.Content ]
        
        button [ OnClick (fun _ -> props.Dispatch BtnClicked) ] [ str "Click 2" ]
    ]
)
        