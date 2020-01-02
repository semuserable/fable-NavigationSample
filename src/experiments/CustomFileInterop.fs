namespace Client.Experiments

(*

Create 'interop.js' file in 'node_modules' folder

function triggerAlert(message) {
    alert(message);
}

const someString = "And I Like that!";

export { triggerAlert, someString };

export function getJohnName() {
    return "John"
}

export class MyClass {
    constructor(value) {
        this.awesomeInteger = value;
    }

    get awesomeInteger() {
        return this._awesomeInteger;
    }

    set awesomeInteger(newValue) {
        this._awesomeInteger = newValue;
    }

    isAwesome() {
        return this._awesomeInteger === 42;
    }

    static getPI() {
        return Math.PI;
    }
}

export function calculateSum(obj) {
    return (obj.day === "monday" || obj.day === "tuesday" || obj.day === "wednesday")
        ? obj.x + obj.y + obj.day.length
        : -1;
}

*)
module CustomFileInterop =

    open Fable.Core
    open Fable.Core.JsInterop

    type private IInterop = 
        abstract triggerAlert: message:string -> unit 
        abstract someString: string

    [<Import("*", "interop.js")>]
    let private interopLib: IInterop = jsNative

    // import { myString } from "my-lib"
    let private getJohnName (): string = import "getJohnName" "interop.js"

    type private MyClassImplementation = 
      abstract awesomeInteger: int with get, set
      abstract isAwesome: unit -> bool
      
    type private MyClass = 
      [<Emit("new $0($1...)")>]
      abstract Create: awesomeInteger:int -> MyClassImplementation 
      abstract getPI: unit -> string
      
    [<Import("MyClass", "interop.js")>]
    let private myClassStatic: MyClass = jsNative

    // let's make our object mutable to be able to change its members
    let mutable private myObject = myClassStatic.Create 40

    [<StringEnum>]
    type private CalculateDay =
        | Monday
        | Tuesday
        | [<CompiledName("wednesday")>] Wed

    type private CalculateProp =
         abstract x: float with get, set
         abstract y: float with get, set
         abstract day: CalculateDay with get, set
         
    type private CalculationObj =
        | X of float
        | Y of float
        | Day of CalculateDay

    [<Import("calculateSum", "interop.js")>]
    let private calculate (props: CalculateProp): float = jsNative

    [<Import("calculateSum", "interop.js")>]
    let private calculate2 (props: obj): float = jsNative
     
    // Fable can make the transformation at compile time when applying the list
    // literal directly to keyValueList. That's why it's usually a good idea to inline
    // the function containing the helper.
    let inline private calculate2Call (c: CalculationObj list) =
        c |> keyValueList CaseRules.LowerFirst |> calculate2
            
    let private calculateProp = createEmpty<CalculateProp>
    calculateProp.x <- 10.
    calculateProp.y <- 20.
    calculateProp.day <- CalculateDay.Wed

    let private calculateProp2 = jsOptions<CalculateProp>(fun prop ->
        prop.x <- 10.
        prop.y <- 20.
        prop.day <- CalculateDay.Wed
    )

    let run =
        JS.console.log calculateProp
        // interopLib.triggerAlert "From F#"
        interopLib.someString + " " +
        (getJohnName ()) + " " +
        (string (myObject.isAwesome ())) + " " +
        (string (myClassStatic.getPI ())) + " " +
        (string (calculate calculateProp)) + " " +
        (string (calculate2Call [X 10.; Y 20.; Day Tuesday]))
