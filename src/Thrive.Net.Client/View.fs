module Thrive.Net.Client.View

let view (model: Models.Model) dispatch =
    match model.page with
    | _ -> Pages.login model dispatch