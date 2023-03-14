using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Pokemin.Api.Blazor.Server.HttpClient;
using Pokemin.Api.Blazor.Server.Middlewares;
using Pokemon.Data;
using Pokemon.Services;
using System.Text.Json.Serialization;
using Pokemon.Api;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddSingleton<IConfigureOptions<HttpClientFactoryOptions>>
        (new ConfigureBaseUrlOptions(builder.Configuration["PokeApiUrl"]));

builder.Services.AddRazorPages();
builder.Services.AddScoped<IPokemonService, PokemonService>();
builder.Services.AddScoped<IPokemonDatabase, PokemonDatabase>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddSwaggerSetup(builder.Configuration);

builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

ConfigureControllers(builder.Services);

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
app.UseSwaggerSetup(builder.Configuration);

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();


void ConfigureControllers(IServiceCollection services)
{
    services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
        // Use the default property (Pascal) casing
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();

        var settings = options.SerializerSettings;
        settings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        settings.NullValueHandling = NullValueHandling.Include;
        settings.DefaultValueHandling = DefaultValueHandling.Include;
        settings.Formatting = Formatting.Indented;
    });
}