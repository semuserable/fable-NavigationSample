module Client.Utils

open Fable.React

let inline functionalComponent name render = FunctionComponent.Of(render, name, equalsButFunctions)    
