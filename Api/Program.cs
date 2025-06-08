using dotenv.net;
using Api.Modules;
using Api.Database;
using Microsoft.EntityFrameworkCore;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseSqlServer(Environment.GetEnvironmentVariable("BD_CONNECTION")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

builder.Services.AddApiServices();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.AddAuthentication()
    .AddGoogle(opts => { 
        opts.ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID") ?? string.Empty;
        opts.ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET") ?? string.Empty;
        opts.SignInScheme = IdentityConstants.ExternalScheme;
    });

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

app.Run();
