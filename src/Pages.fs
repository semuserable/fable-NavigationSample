module Client.Pages


[<RequireQualifiedAccess>]
type Page =
    | Home
    | Game

let toPage =
    function
    | Page.Home -> "#/home"
    | Page.Game -> "#/game"
    
open Elmish.UrlParser

let private routeParser': Parser<Page -> Page, _> =
    oneOf [
        map Page.Home (s "home")
        map Page.Game (s "game")
    ]

let routeParser location = parseHash routeParser' location