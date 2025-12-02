using DesafioTarget2.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(
        // Não diferencia maiúscula de minúscula ao desserializar o json,
        // para que as propriedades das classes possam usar camel case
        (JsonOptions options) => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true
    );
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(
    (DbContextOptionsBuilder options) => options.UseInMemoryDatabase("databaseName")
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
