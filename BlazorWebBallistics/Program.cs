using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWebBallistics;
using BlazorWebBallistics.Models;
using BlazorWebBallistics.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ITrajectoryCalculationService, TrajectoryCalculationService_ClosedSystem>();
builder.Services.AddSingleton<SimulationParameters>();

await builder.Build().RunAsync();
