using FullStackWorkAPI.Exceptions;
using FullStackWorkAPI.Helpers;
using FullStackWorkAPI.Interfaces;
using FullStackWorkAPI.Repository;
using FullStackWorkAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Incident Management API", Version = "v1" });
    c.SchemaFilter<EnumSchemaFilter>();
});

// Add problem details for better error handling
builder.Services.AddProblemDetails();

// Register dependencies
builder.Services.AddSingleton<IIncidentRepository, InMemoryIncidentRepository>();
builder.Services.AddScoped<IIncidentService, IncidentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// Add custom middleware for global exception handling
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
