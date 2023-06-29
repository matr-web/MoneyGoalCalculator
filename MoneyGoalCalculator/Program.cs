using Microsoft.EntityFrameworkCore;
using MoneyGoalCalculator.db;
using MoneyGoalCalculator.Interfaces;
using MoneyGoalCalculator.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoneyGoalContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICalculationService, CalculationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
