
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Backend.Models;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddSqlServer<ApplicationDbContext>(connectionString);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.MapScalarApiReference(options => 
    {
        options.Theme = ScalarTheme.BluePlanet;
    });
    
    
}

app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/", () => "Hello world!");

app.Run();


