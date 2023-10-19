global using MudBlazor.Services;
global using DiplomskiBlazor.Client.Services.KorisnikService;
global using DiplomskiBlazor.Shared;
global using DiplomskiBlazor.Client.Services.DeoTelaService;
global using DiplomskiBlazor.Client.Services.VezbaService;
global using DiplomskiBlazor.Client.Services.StatistikaService;
global using DiplomskiBlazor.Client.Services.WorkoutService;
global using Blazored.LocalStorage;
global using Microsoft.AspNetCore.Components.Authorization;

using DiplomskiBlazor.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IKorisnikService, KorisnikService>();
builder.Services.AddScoped<IVezbaService, VezbaService>();
builder.Services.AddScoped<IDeoTelaService, DeoTelaService>();
builder.Services.AddScoped<IStatistikaService, StatistikaService>();
builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();

await builder.Build().RunAsync();