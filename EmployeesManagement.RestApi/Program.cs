using EmployeesManagement.RestApi.Contracts.Responses;
using EmployeesManagement.RestApi.Database;
using EmployeesManagement.RestApi.Repositories;
using EmployeesManagement.RestApi.Repositories.Interfaces;
using EmployeesManagement.RestApi.Services;
using EmployeesManagement.RestApi.Services.Interfaces;
using EmployeesManagement.RestApi.Validation;

using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddFastEndpoints(x => x.IncludeAbstractValidators = true);
builder.Services.AddSwaggerDoc(shortSchemaNames: true, removeEmptySchemas: true);
Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

builder.Services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(config.GetValue<string>("Database:ConnectionString")!));
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddSingleton<IPositionRepository, PositionRepository>();
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
builder.Services.AddSingleton<IPositionService, PositionService>();

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseFastEndpoints(x =>
{
    x.Errors.ResponseBuilder = (failures, context, statusCode) =>
        new ValidationFailureResponse { Errors = failures.Select(y => y.ErrorMessage).ToList() };
});
app.UseSwaggerGen();

var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await databaseInitializer.InitializeAsync();

app.Run();
