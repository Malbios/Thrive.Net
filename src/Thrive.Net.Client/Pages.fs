module Thrive.Net.Client.Pages

open Bolero.Html
open Radzen
open Radzen.Blazor
open Thrive.Net.Client.Components

let home (model: Model.Model) dispatch =
    div {
        attr.``class`` "login-container"
        
        img {
            attr.``class`` "bg-image"
            attr.src "/assets/thrive-logo-banner.png"
        }
        
        div {
            attr.``class`` "rz-border rz-border-radius-6 rz-border-color-info-darker login-overlay"
            attr.style "--rz-border-width: 5px;"
            
            ecomp<LoginForm,_,_> model.Credentials (fun n -> dispatch n) { attr.empty() }
        }
    }