namespace Thrive.Net.Client.Models

open Bolero

type Page =
    | [<EndPoint "/">] Login
        
type Message =
    | SetPage of Page
    | SetUsername of string
    | SetPassword of string
    | CheckCredentials
    | Error of exn
    | ClearError

type Credentials = {
    Username: string
    Password: string
    IsLoggedIn: bool
}

module Credentials =
    let none =
        { Username = ""; Password = ""; IsLoggedIn = false }

type Model =
    {
        page: Page
        Credentials: Credentials
        error: string option
    }
    
module Model =
    let initModel =
        {
            page = Login
            Credentials = Credentials.none
            error = None
        }