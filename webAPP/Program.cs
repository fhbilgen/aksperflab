using Microsoft.ApplicationInsights.Extensibility;
using webAPP;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<ITelemetryInitializer, webAPPTeleInit>();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddServiceProfiler();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
