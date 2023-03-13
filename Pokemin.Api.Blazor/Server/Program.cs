using Microsoft.AspNetCore.ResponseCompression;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();

var app = builder.Build();

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    //options.SerializerSettings.Converters.Add(new StringEnumConverter());
    // Use the default property (Pascal) casing
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
    var settings = options.SerializerSettings;
    settings.DateFormatString = "yyyy-MM-ddTHH:mm:ss";
    settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    settings.MissingMemberHandling = MissingMemberHandling.Ignore;
    settings.NullValueHandling = NullValueHandling.Ignore;
    settings.DefaultValueHandling = DefaultValueHandling.Ignore;
    settings.Formatting = Formatting.Indented;
    settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
    options.AllowInputFormatterExceptionMessages = false;
    //settings.Converters.Add(new TiimeOnlyJsonConverter());
});

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
