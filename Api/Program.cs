using dotenv.net;
using Api.Modules;
using Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Api.Models;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(Environment.GetEnvironmentVariable("BD_CONNECTION")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddApiServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Personalized .Net 9");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapIdentityApi<User>();

app.Run();
