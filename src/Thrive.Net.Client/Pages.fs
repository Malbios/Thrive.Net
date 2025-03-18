module Thrive.Net.Client.Pages

open Bolero.Html
open Thrive.Net.Client.Components

let home (model: Model.Model) dispatch =
    div {
        attr.``class`` "login-container"
        ecomp<LoginForm,_,_> model.Credentials (fun n -> dispatch n) { attr.empty() }
    }