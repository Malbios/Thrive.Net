module Thrive.Net.Server

open Bolero
open Bolero.Html
open Bolero.Server.Html
open Microsoft.AspNetCore.Components
open Microsoft.AspNetCore.Components.Web
open Radzen.Blazor
open Thrive.Net.Client.Main

let page = doctypeHtml {
    head {
        meta { attr.charset "UTF-8" }
        meta { attr.name "viewport"; attr.content "width=device-width, initial-scale=1.0" }
        title { "Thrive.Net" }
        ``base`` { attr.href "/" }
        link { attr.rel "stylesheet"; attr.href "Thrive.Net.Client.styles.css" }
        link { attr.rel "stylesheet"; attr.href "_content/Radzen.Blazor/css/material-base.css" }
        link { attr.rel "stylesheet"; attr.href "https://cdnjs.cloudflare.com/ajax/libs/bulma/0.7.4/css/bulma.min.css" }
        link { attr.rel "shortcut icon"; attr.href "assets/favicon.ico" }
        script { attr.src $"_content/Radzen.Blazor/Radzen.Blazor.js?v={typedefof<Radzen.Colors>.Assembly.GetName().Version}" }
        link { attr.rel "stylesheet"; attr.href "css/index.css" }
        comp<RadzenTheme> {
            "Theme" => "material"
        }
    }
    body {
        div {
            attr.id "main"
            comp<App> { attr.renderMode RenderMode.InteractiveWebAssembly }
        }
        
        boleroScript
    }
}

[<Route "/{*path}">]
type BootstrapPage() =
    inherit Component()
    override _.Render() = page
