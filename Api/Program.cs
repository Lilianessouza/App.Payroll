using Application.Interface;
using Application.Service;
using Common.IoC;
using Domain.Interfaces;
using Infra.Context;
using Infra.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var sqlConnection = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<DbSqlContext>(options => options.UseSqlServer(sqlConnection));

builder.Services.AddAutoMapper(builder.Configuration);
builder.Services.AddService(builder.Configuration);
builder.Services.AddInfra(builder.Configuration);

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
