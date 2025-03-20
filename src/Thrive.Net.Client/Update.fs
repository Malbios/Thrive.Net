module Thrive.Net.Client.Update

open Elmish
open Thrive.Net.Client.Models

let update message model =
    match message with
    | SetPage page ->
        { model with page = page }, Cmd.none
        
    | SetUsername value ->
        { model with Model.Credentials.Username = value }, Cmd.ofMsg CheckCredentials
    | SetPassword value ->
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