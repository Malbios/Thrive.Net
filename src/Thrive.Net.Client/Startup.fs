namespace Thrive.Net

open Microsoft.AspNetCore.Components.WebAssembly.Hosting
open Bolero.Remoting.Client
open Radzen

module Program =

    [<EntryPoint>]
    let Main args =
        let builder = WebAssemblyHostBuilder.CreateDefault(args)
        builder.Services.AddBoleroRemoting(builder.HostEnvironment) |> ignore
        builder.Services.AddRadzenComponents() |> ignore
        builder.RootComponents.Add<Client.App>("#main")
        builder.Build().RunAsync() |> ignore
        0
