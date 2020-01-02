module Client.Pages


[<RequireQualifiedAccess>]
type Page =
    | Home
    | Game
    | Interop

let toPage =
    function
    | Page.Home -> "#/home"
    | Page.Game -> "#/game"
    | Page.Interop -> "#/interop"
    
open Elmish.UrlParser

let private routeParser': Parser<Page -> Page, _> =
    oneOf [
        map Page.Home (s "home")
        map Page.Game (s "game")
        map Page.Interop (s "interop")
    ]

let routeParser location = parseHash routeParser' location