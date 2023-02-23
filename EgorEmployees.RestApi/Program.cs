using EgorEmployees.RestApi.Contracts.Responses;
using EgorEmployees.RestApi.Database;
using EgorEmployees.RestApi.Repositories.Interfaces;
using EgorEmployees.RestApi.Services;
using EgorEmployees.RestApi.Services.Interfaces;

using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IPositionService, PositionService>();

var app = builder.Build();

app.UseFastEndpoints();

app.Run();
