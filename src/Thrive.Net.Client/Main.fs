module Thrive.Net.Client

open Elmish
open Bolero
open Bolero.Html
open Bolero.Templating.Client
open Microsoft.AspNetCore.Components.Web
open Radzen.Blazor

type Page =
    | [<EndPoint "/">] Home

type Model =
    {
        page: Page
        counter: int
        error: string option
    }

let initModel =
    {
        page = Home
        counter = 0
        error = None
    }

type Message =
    | SetPage of Page
    | Increment
    | Decrement
    | SetCounter of int
    | Error of exn
    | ClearError

let update message model =
    match message with
    | SetPage page ->
        { model with page = page }, Cmd.none

    | Increment ->
        { model with counter = model.counter + 1 }, Cmd.none
    | Decrement ->
        { model with counter = model.counter - 1 }, Cmd.none
    | SetCounter value ->
        { model with counter = value }, Cmd.none
        
    | Error exn ->
        { model with error = Some exn.Message }, Cmd.none
    | ClearError ->
        { model with error = None }, Cmd.none

let router = Router.infer SetPage _.page

let view model dispatch =
    div {
        p {
            comp<RadzenButton> {
                "Text" => "-"
                attr.callback<MouseEventArgs> "Click" (fun _ -> dispatch Decrement)
            }
            comp<RadzenLabel> {
                attr.style "margin: 0em 1em 0em 1em;"
                "Text" => $"{model.counter}"
            }
            comp<RadzenButton> {
                "Text" => "+"
                attr.callback<MouseEventArgs> "Click" (fun _ -> dispatch Increment)
            }
        }
    }

type App() =
    inherit ProgramComponent<Model, Message>()
    
    override _.CssScope = CssScopes.App
    
    override this.Program =
        Program.mkProgram (fun _ -> initModel, Cmd.none) update view
        |> Program.withRouter router
#if DEBUG
        |> Program.withHotReload
#endif
