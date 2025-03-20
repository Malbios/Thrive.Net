module Thrive.Net.Client.Main

open Elmish
open Bolero
open Thrive.Net.Client.Models
open Thrive.Net.Client.Update
open Thrive.Net.Client.View

type App() =
    inherit ProgramComponent<Model, Message>()
    
    override _.CssScope = CssScopes.``Thrive.Net.Client``
    
    override this.Program =
        Program.mkProgram (fun _ -> Model.initModel, Cmd.none) update view
        |> Program.withRouter (Router.infer SetPage _.page)
        
// #if DEBUG
//         |> Program.withHotReload
// #endif
