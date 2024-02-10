using LurkingUnits.Application;
using Microsoft.Extensions.Options;
using SignalR.Application.DI;
using SignalR.Application.Hubs.Chat;
using SignalR.Extensions;
using SignalR.Infrastructure;
using SignalR.Infrastructure.DI;
using System.Text.Json.Serialization;


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

app.UseSwagger();

app.UseSwaggerUI();

app.UseRequestLocalization(service.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();

app.UseExceptionHandler(_ => { });

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/Chat-hub");

app.MapGet("time", () => ResponseModel<DateTime>.Success(DateTime.Now));

app.Run();
