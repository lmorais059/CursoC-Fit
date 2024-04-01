using Catalog.Api.Middlewares;
using Catalog.Domain;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();
builder.Services.AddDomainServices(opt => opt.UseSqlite("DataSource=database.sqlite"));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
