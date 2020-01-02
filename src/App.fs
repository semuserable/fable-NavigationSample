module App

open Client
open Elmish
open Elmish.Navigation
open Client.Pages

type PageModel =
    | HomeModel of Home.Model
    | GameModel of Game.Model
    | InteropModel of Interop.Model

type Model = {
    PageModel: PageModel
}
    
type Message =
    | HomeMsg of Home.Msg
    | GameMsg of Game.Msg
    | InteropMsg of Interop.Msg
    
let urlUpdate (result: Option<Page>) model =
    match result with
    | None ->
        model, Cmd.none
    | Some Page.Home ->
        let subModel, cmd = Home.init ()
        { model with PageModel = HomeModel subModel }, cmd
    | Some Page.Game ->
        let subModel, cmd = Game.init ()
        { model with PageModel = GameModel subModel }, cmd
    | Some Page.Interop ->
        let subModel, cmd = Interop.init ()
        { model with PageModel = InteropModel subModel }, cmd

let init page =
    let initialModel = HomeModel { Title = "Main Title"; Content = "Main Content" }
    urlUpdate page { PageModel = initialModel }

let update msg model =
    match msg, model.PageModel with
    | HomeMsg homeMsg, HomeModel homeModel ->
        let ms, cmd = Home.update homeMsg homeModel
        { model with PageModel = HomeModel ms }, Cmd.map HomeMsg cmd
    | GameMsg gameMsg, GameModel gameModel ->
        let ms, cmd = Game.update gameMsg gameModel
        { model with PageModel = GameModel ms }, Cmd.map GameMsg cmd
    | _ -> model, Cmd.none

open Fable.React
open Fable.React.Props

let view (model: Model) (dispatch: Dispatch<Message>) =    
    div [] [
        ul [] [
            li [] [
                a [ Href (toPage Page.Home) ] [ str "Home" ]
            ]
            li [] [
                a [ Href (toPage Page.Game) ] [ str "Game" ]
            ]
            li [] [
                a [ Href (toPage Page.Interop) ] [ str "Interop" ]
            ]
        ]
        div [] [
            match model.PageModel with
            | HomeModel m -> yield Home.view { Model = m; Dispatch = HomeMsg >> dispatch }
            | GameModel m -> yield Game.view { Model = m; Dispatch = GameMsg >> dispatch }
            | InteropModel m -> yield Interop.view { Model = m; Dispatch = InteropMsg >> dispatch }
        ]
    ]

open Elmish.React

Program.mkProgram init update view
|> Program.toNavigable routeParser urlUpdate
|> Program.withReactSynchronous "elmish-app"
|> Program.withConsoleTrace
|> Program.run
