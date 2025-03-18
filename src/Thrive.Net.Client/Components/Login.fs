namespace Thrive.Net.Client.Components

open Bolero
open Bolero.Html
open Microsoft.AspNetCore.Components
open Radzen
open Radzen.Blazor
open Thrive.Net.Client.Model

module Helpers =
    let renderComponent<'T when 'T :> IComponent> placeholder model dispatch =
        comp<'T> {
            attr.``class`` "rz-border-radius"
            
            "Placeholder" => placeholder
            "Value" => model
            attr.callback<string> "Change" dispatch
        }

type Input() =
    inherit ElmishComponent<string, string>() // <model, message>
    
    [<Parameter>]
    member val Placeholder = "" with get, set
    
    [<Parameter>]
    member val IsPassword = false with get, set
    
    override this.ShouldRender(oldModel, newModel) =
        oldModel <> newModel

    override this.View model dispatch =
        if this.IsPassword then
            Helpers.renderComponent<RadzenPassword> this.Placeholder model dispatch
        else
            Helpers.renderComponent<RadzenTextBox> this.Placeholder model dispatch

type LoginForm() =
    inherit ElmishComponent<Credentials, Message>()
    
    override this.ShouldRender(oldModel, newModel) =
        oldModel.Username <> newModel.Username || oldModel.Password <> newModel.Password || oldModel.IsLoggedIn <> newModel.IsLoggedIn
    
    override this.View model dispatch =
        let welcomeMessage = if model.IsLoggedIn then $", {model.Username}" else ""
        
        comp<RadzenStack> {
            attr.``class`` "rz-border rz-border-radius-6 rz-border-color-info-darker login-box"
            attr.style "--rz-border-width: 5px;"
            
            "Orientation" => Orientation.Vertical
            "AlignItems" => AlignItems.Center
            "JustifyContent" => JustifyContent.Center
            
            comp<RadzenLabel> {
                attr.``class`` "login-label"
                "TextStyle" => TextStyle.Body1
                strong {
                    $"Welcome to Thrive.Net{welcomeMessage}"
                }
            }
            
            ecomp<Input,_,_> model.Username (fun n -> dispatch (SetUsername n)) {
                "Placeholder" => "Enter username..."
            }
            
            ecomp<Input,_,_> model.Password (fun n -> dispatch (SetPassword n)) {
                "Placeholder" => "Enter password..."
                "IsPassword" => true
            }
        }