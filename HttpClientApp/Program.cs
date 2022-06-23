using HttpClientApp.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient("openweathermap", c =>
{
    var config = builder.Configuration.GetSection("WeatherApis:OpenWeatherMapOrg");
    c.BaseAddress = new Uri(config["BaseUrl"]);

    // Is it possible to pass the API KEY in the header, and not in the query parameters?
    // Something like that. It's not working for me yet.
    c.DefaultRequestHeaders.Add(config["ApiKey:Key"], config["ApiKey:Value"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
