using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.Json;
using Microsoft.OData.ModelBuilder;
using WebApplicationTestoData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DefaultJsonWriterFactory>();

builder.Services.AddControllers()
.AddOData(opt =>
    opt.AddRouteComponents(GetEdmModel()).Filter().Select().OrderBy().SkipToken().Expand()
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new();
    builder.EntitySet<WeatherForecast>("WeatherForecast");
    return builder.GetEdmModel();
}