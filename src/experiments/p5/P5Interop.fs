namespace Client.Experiments
open Browser.Types
open Browser.Types
open Browser.Types
open Fable.React

module P5Interop =
    open Fable.Core
    open Fable.Import
    open Fable.React.Props
    open Fable.Import.p5
    open Browser.Types
    
    let inline log obj = JS.console.log obj

    type Props = {
        id: string
        sketch: p5 -> unit
    }
    
    type P5Wrapper(props) =
        inherit Component<Props, obj>(props)
        
        let mutable canvas: p5 option = None
        
        override this.componentDidMount() =
            canvas <- Some (p5(this.props.sketch, this.props.id))
                
        override this.render() = div [ Id this.props.id ] []
        
        override this.componentWillUnmount() =
            match canvas with
            | Some c -> c.remove ()
            | None -> ()
    
    let inline p5Wrapper props = ofType<P5Wrapper,_,_> props []
    
    let sketch1 (it: p5) =
        it.draw <- fun() -> it.ellipse(50., 50., 80., 80.) |> ignore
        
    let sketch2 (it: p5) =
        it.setup <- fun() -> it.createCanvas(700., 410.)
        it.draw <- fun() ->
            if it.mouseIsPressed then
                it.fill (0. |> U4.Case1)
            else
                it.fill (255. |> U4.Case1)
                
            it.rect(it.mouseX, it.mouseY, 50., 50.) |> ignore
            
    let sketch3D (it: p5) =
        it.setup <- fun() -> it.createCanvas(100., 100., WebGL)
        it.draw <- fun() ->
            it.background(255)
            it.rotateX(it.millis() / 1000.)
            it.box()
            
    let sketch3DAnother (it: p5) =
        it.setup <- fun() -> it.createCanvas(710., 400., WebGL)
        it.draw <- fun() ->
            it.background(250)
            it.translate(0, 0, 0)
            it.normalMaterial()
            it.push()
            it.rotateZ(it.frameCount * 0.01)
            it.rotateX(it.frameCount * 0.01)
            it.rotateY(it.frameCount * 0.01)
            it.box(70., 70., 70.)
            it.pop();
        
    let run () =
        let p5Inst = p5(sketch1, "p5sketch")
        p5(sketch1) |> ignore
        
        p5Inst.width <- 50.
        let data = [
            p5Inst.PI,
            p5Inst.width
        ]
        log data
        let str = (", ", data |> Seq.map (fun v -> string v)) |> System.String.Join
        "P5Interop " + str
        
    let runView =
        div [] [
            h2 [] [ str "P5.js interop" ]
            p5Wrapper { id = "container1"; sketch = sketch1 }
            //p5Wrapper { id = "container2"; sketch = sketch2 }
            p5Wrapper { id = "container3"; sketch = sketch3D }
            p5Wrapper { id = "container4"; sketch = sketch3DAnother }
        ]