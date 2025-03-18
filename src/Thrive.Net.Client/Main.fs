module Thrive.Net.Client.Main

open Elmish
open Bolero
open Bolero.Html
open Thrive.Net.Client.Components
open Thrive.Net.Client.Model

let update message model =
    match message with
    | SetPage page ->
        { model with page = page }, Cmd.none
        
    | SetUsername value ->
        printfn $"Setting username to '{value}'..."
        { model with Model.Credentials.Username = value }, Cmd.ofMsg CheckCredentials
    | SetPassword value ->
        printfn $"Setting password to '{value}'..."
        { model with Model.Credentials.Password = value }, Cmd.ofMsg CheckCredentials
        
    | CheckCredentials ->
        if model.Credentials.Username <> "" && model.Credentials.Password = "pass" then
            { model with Model.Credentials.IsLoggedIn = true }, Cmd.none
        else
            model, Cmd.none
        
    | Error exn ->
        { model with error = Some exn.Message }, Cmd.none
    | ClearError ->
        { model with error = None }, Cmd.none

let router = Router.infer SetPage _.page

let view model dispatch =
    Pages.home model dispatch

type App() =
    inherit ProgramComponent<Model, Message>()
    
    override _.CssScope = CssScopes.App
    
    override this.Program =
        Program.mkProgram (fun _ -> Model.initModel, Cmd.none) update view
        |> Program.withRouter router
// #if DEBUG
//         |> Program.withHotReload
// #endif
