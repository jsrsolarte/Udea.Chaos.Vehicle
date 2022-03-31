using Flurl.Http.Configuration;
using MediatR;
using System.Reflection;
using Udea.Chaos.Owner.Infrastructure.Adapters;
using Udea.Chaos.Vehicle.Application.Ports;
using Udea.Chaos.Vehicle.Infrastructure.Extensions;

var applicationAssembly = Assembly.Load("Udea.Chaos.Vehicle.Application");

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddHealthChecks();
builder.Services.AddTransient(typeof(IFlurlClientFactory), typeof(PerBaseUrlFlurlClientFactory));
builder.Services.AddTransient<IJourneyService, JourneyService>();
builder.Services.AddPersistence(config);
builder.Services.AddMediatR(applicationAssembly, typeof(Program).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting().UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();