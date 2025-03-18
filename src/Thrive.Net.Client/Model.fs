namespace Thrive.Net.Client.Model

open Bolero

type Credentials = {
    Username: string
    Password: string
    IsLoggedIn: bool
}

type Page =
    | [<EndPoint "/">] Home

type Model =
    {
        page: Page
        Credentials: Credentials
        error: string option
    }
    
module Model =
    let initModel =
        {
            page = Home
            Credentials = {
                Username = ""
                Password = ""
                IsLoggedIn = false
            }
            error = None
        }
    
type Message =
    | SetPage of Page
    | SetUsername of string
    | SetPassword of string
    | CheckCredentials
    | Error of exn
    | ClearError