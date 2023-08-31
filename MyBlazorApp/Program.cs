using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MyBlazorApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Configure HTTP client for PlayerManagementService
builder.Services.AddHttpClient("PlayerService", client =>
{
    client.BaseAddress = new Uri("http://host.docker.internal:5224"); //"http://localhost:5224/"
});

// Configure HTTP client for GameManagementService
builder.Services.AddHttpClient("GameService", client =>
{
    client.BaseAddress = new Uri("http://host.docker.internal:5074"); //"http://localhost:5074/"
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
