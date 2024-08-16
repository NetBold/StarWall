using Blazored.LocalStorage;
using StarWall.UI.APIServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddBlazoredLocalStorage();   // local storage
builder.Services.AddBlazoredLocalStorage(config =>
    config.JsonSerializerOptions.WriteIndented = true);
builder.Services.AddScoped<CustomAuthStateHandler>();
builder.Services.AddScoped<AccountAPIService>();
builder.Services.AddScoped<HomeAPIService>();
builder.Services.AddScoped<AdminAPIService>();
builder.Services.AddHttpClient("StarWallApi", c =>
{
    c.BaseAddress = new Uri(builder.Configuration["ApiConnections:StarWallUrl"]);
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
