using Microsoft.Extensions.Options;
using SignalR.Extensions;
using SignalR.Application.DI;
using SignalR.Infrastructure.DI;
using System.Text.Json.Serialization;
using SignalR.Infrastructure;
using SignalR.Application.Hubs.Chat;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDataBaseContext(builder.Configuration);

builder.Services.AddIdentityServices();

builder.Services.AddAAuthenticationService(builder.Configuration);

builder.Services.AddLocalizationService();

builder.Services.AddApplicationStrapping();

builder.Services.AddInfrastructureStrapping();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddSwaggerDocumentationService();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var service = builder.Services.BuildServiceProvider();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLocalization(service.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/Chat-hub");

app.Run();
