using System.Reflection;
using wf.Api.Middleware.Configuration;
using wf.Api.Middleware.Cors;
using wf.Api.Middleware.ErrorHandling;
using wf.Api.Middleware.Logging;
using wf.Api.Middleware.ModelValidation;
using wf.Api.Middleware.Swagger;
using wf.Api.Middleware.Versioning;
using wf.Business;
using wf.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureTelemetry();

builder.Services.ConfigureEnvOptions(builder.Configuration);
builder.Services.ConfigureVersioning();
builder.Services.ConfigureSwagger(Assembly.GetExecutingAssembly().GetName().Name!);
builder.Services.ConfigureModelValidation();
builder.Services.ConfigureCors();

builder.Services.BootstrapDataAccess();
builder.Services.BootstrapBusiness();

var app = builder.Build();

app.ConfigureErrorHandling();
app.ConfigureSwagger();
app.ConfigureCors();

app.UseRouting();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
