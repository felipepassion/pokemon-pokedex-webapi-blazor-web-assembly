using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Pokemin.Api.Blazor.Server.HttpClient;
using Pokemin.Api.Blazor.Server.Middlewares;
using Pokemon.Data;
using Pokemon.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IConfigureOptions<HttpClientFactoryOptions>>
        (new ConfigureBaseUrlOptions(builder.Configuration["PokeApiUrl"]));

builder.Services.AddRazorPages();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonDatabase, PokemonDatabase>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
