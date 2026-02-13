using JobFinder.Business.Todo.Implementations;
using JobFinder.Business.Todo.Interfaces;
using JobFinder.Data.DB;
using JobFinder.Data.Repositories.Todo.Implmentation;
using JobFinder.Data.Repositories.Todo.Interfaces;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddDbContext<JobFinderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.MapScalarApiReference(options =>
{
    options.Title = "JobFinder API Reference";
    options.Theme = ScalarTheme.Mars;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
