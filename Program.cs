using BlazorApp.Components;
using Microsoft.EntityFrameworkCore;
using BlazorApp.Security;
using BlazorApp.Connection;
using BlazorApp.Tools;
using BlazorApp.Controllers;
using BlazorApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<EmployeeController>();
builder.Services.AddScoped<NotificationController>();
builder.Services.AddScoped<ArgonService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<SessionService>();
builder.Services.AddScoped<LoginController>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

if (args.Contains("--register-user"))
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await RegisterUser.Run(db);
    return;
}

app.Run();
