namespace Thrive.Net.Client.Model

open Bolero

type Credentials = {
    Username: string
    Password: string
}

type Page =
    | [<EndPoint "/">] Home

type Model =
    {
        page: Page
        Credentials: Credentials
        error: string option
    }
    
type Message =
    | SetPage of Page
    | SetUsername of string
    | SetPassword of string
    | Error of exn
    | ClearError