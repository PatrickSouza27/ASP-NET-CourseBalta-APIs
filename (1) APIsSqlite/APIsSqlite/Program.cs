using Microsoft.AspNetCore.Mvc;

using APIsSqlite.Controllers;
using APIsSqlite.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
