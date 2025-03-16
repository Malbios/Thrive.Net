namespace Thrive.Net

open Microsoft.AspNetCore.Authentication.Cookies
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Bolero.Remoting.Server
open Bolero.Server
open Bolero.Templating.Server
open Radzen

module Program =

    [<EntryPoint>]
    let main args =
        let builder = WebApplication.CreateBuilder(args)

        builder.Services.AddRadzenComponents() |> ignore
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents()
        |> ignore
        builder.Services.AddServerSideBlazor() |> ignore
        builder.Services.AddAuthorization()
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie()
        |> ignore
        builder.Services.AddBoleroComponents() |> ignore
    #if DEBUG
        builder.Services.AddHotReload(templateDir = __SOURCE_DIRECTORY__ + "/../Thrive.Net.Client") |> ignore
    #endif

        let app = builder.Build()

        if app.Environment.IsDevelopment() then
            app.UseWebAssemblyDebugging()

        app
            .UseAuthentication()
            .UseStaticFiles()
            .UseRouting()
            .UseAuthorization()
            .UseAntiforgery()
        |> ignore

    #if DEBUG
        app.UseHotReload()
    #endif
        app.MapBoleroRemoting() |> ignore
        app.MapRazorComponents<Server.BootstrapPage>()
            .AddInteractiveServerRenderMode()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof<Client.App>.Assembly)
        |> ignore

        app.Run()
        0
