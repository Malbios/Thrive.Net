namespace Thrive.Net.Client.Components

open Bolero
open Bolero.Html
open Microsoft.AspNetCore.Components
open Radzen
open Radzen.Blazor
open Thrive.Net.Client.Model

type Input() =
    inherit ElmishComponent<string, string>() // <model, message>
    
    [<Parameter>]
    member val Placeholder = "" with get, set
    
    override this.ShouldRender(oldModel, newModel) =
        oldModel <> newModel

    override this.View model dispatch =
        comp<RadzenTextBox> {
            "Placeholder" => $"Enter {this.Placeholder}..."
            "Value" => model
            attr.callback<string> "Change" dispatch
        }

type LoginForm() =
    inherit ElmishComponent<Credentials, Message>()
    
    override this.ShouldRender(oldModel, newModel) =
        oldModel.Username <> newModel.Username || oldModel.Password <> newModel.Password
    
    override this.View model dispatch =
        comp<RadzenStack> {
            "Orientation" => Orientation.Vertical
            "AlignItems" => AlignItems.Center
            "JustifyContent" => JustifyContent.Center
            
            comp<RadzenLabel> {
                "Text" => $"Username: {model.Username}"
            }
            comp<RadzenLabel> {
                "Text" => $"Password: {model.Password}"
            }
            
            ecomp<Input,_,_> model.Username (fun n -> dispatch (SetUsername n)) { "Placeholder" => "username" }
            ecomp<Input,_,_> model.Password (fun n -> dispatch (SetPassword n)) { "Placeholder" => "password" }
        }