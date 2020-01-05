namespace Fable.Import
open Fable.Core

module p5 =
    open Fable.Core
    open Fable.Core.JsInterop

    let [<Literal>] libPath = "p5/lib/p5.js"
    
    [<StringEnum>]
    type Renderer =
        | [<CompiledName("p2d")>] P2D
        | [<CompiledName("webgl")>] WebGL
        
    type [<AllowNullLiteral>] [<Import("Color", libPath)>] Color() =
        class end
        
    type [<AllowNullLiteral>] [<Import("*", libPath)>] p5(?sketch: p5 -> unit, ?id: string) =
        // TODO: check with just 'abstract'
        member __.PI with get(): float = jsNative
        member __.width with get(): float = jsNative and set(v: float): unit = jsNative
        member __.height with get(): float = jsNative and set(v: float): unit = jsNative        
        member __.setup with get(): unit -> unit = jsNative and set(v: unit -> unit): unit = jsNative  
        member __.draw with get(): unit -> unit = jsNative and set(v: unit -> unit): unit = jsNative
        member __.createCanvas(w: float, h: float, ?renderer: Renderer): unit = jsNative
        
        /// <summary>Draws an ellipse (oval) to the screen. An ellipse
        /// with equal width and height is a circle. By
        /// default, the first two parameters set the
        /// location, and the third and fourth parameters set
        /// the shape's width and height. If no height is
        /// specified, the value of width is used for both the
        /// width and height. If a negative height or width is
        /// specified, the absolute value is taken. The origin
        /// may be changed with the ellipseMode() function.</summary>
        /// <param name="x">x-coordinate of the ellipse.</param>
        /// <param name="y">y-coordinate of the ellipse.</param>
        /// <param name="w">width of the ellipse.</param>
        /// <param name="h">height of the ellipse.</param>
        member __.ellipse(x: float, y: float, w: float, h: float): p5 = jsNative
        member __.rect(x: float, y: float, w: float, h: float, ?tl: float, ?tr: float, ?br: float, ?bl: float): p5 = jsNative
        member __.remove(): unit = jsNative
        member __.mouseIsPressed with get(): bool = jsNative
        member __.mouseX with get(): float = jsNative
        member __.mouseY with get(): float = jsNative
        member __.frameCount with get(): float = jsNative
        member __.fill(v1: U4<float, ResizeArray<obj>, string, Color>, ?v2: float, ?v3: float, ?a: float): unit = jsNative
        
        member __.background(value: int, ?opacity: int): unit = jsNative
        member __.translate(x: int, y: int, ?z: int): unit = jsNative
        member __.normalMaterial(): unit = jsNative
        member __.millis(): float = jsNative
        member __.push(): unit = jsNative
        member __.pop(): unit = jsNative
        member __.box(?width: float, ?height: float, ?depth: float): unit = jsNative
        
        member __.rotateX(angle: float): unit = jsNative
        member __.rotateY(angle: float): unit = jsNative
        member __.rotateZ(angle: float): unit = jsNative
