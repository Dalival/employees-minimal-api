using EgorEmployees.RestApi.Contracts.Responses;
using EgorEmployees.RestApi.Database;
using EgorEmployees.RestApi.Repositories;
using EgorEmployees.RestApi.Repositories.Interfaces;
using EgorEmployees.RestApi.Services;
using EgorEmployees.RestApi.Services.Interfaces;

using FastEndpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastEndpoints();
var config = builder.Configuration;

Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(config.GetValue<string>("Database:ConnectionString")!));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddSingleton<IPositionRepository, PositionRepository>();
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IPositionService, PositionService>();

var app = builder.Build();

app.UseFastEndpoints();

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
