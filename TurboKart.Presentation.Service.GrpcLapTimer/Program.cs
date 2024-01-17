using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SignalR;
using TurboKart.Presentation.Service.GrpcLapTimer;
using TurboKart.Presentation.Service.GrpcLapTimer.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSignalR();

// CORS Middleware
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyHeader()
                  .AllowAnyMethod()
                  .SetIsOriginAllowed((host) => true)
                  .AllowCredentials();
        });
});

var app = builder.Build();

// Use CORS Middleware
app.UseCors();

// Configure the HTTP request pipeline.
app.MapGrpcService<LapTimerService>();
app.MapHub<LapTimerHub>("/laptimer");

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();