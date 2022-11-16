using Microsoft.AspNetCore.HttpOverrides;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("ocelot.json").Build();
builder.Services.AddCors();
builder.Services.AddMvc(option =>
{
    // Respect the request response type (Accept option in request header)
    option.RespectBrowserAcceptHeader = true;
});
// Enable Forwarded Headers
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.All;
});

builder.Services.AddOcelot(configuration);

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .WithExposedHeaders("*")
            );

app.UseRouting();

app.UseForwardedHeaders();

app.UseOcelot().Wait();

app.UseStaticFiles();

app.Run();