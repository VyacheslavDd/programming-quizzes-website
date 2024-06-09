using UserService.Infrastructure.Startups;
using Core.Logging;
using System.Reflection;
using UserService.Infrastructure.Filters;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Core.Base.Service.Interfaces;
using Core.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var frontPolicy = "FrontPolicy";

// Add services to the container.
builder.Services.AddFrontCors(frontPolicy);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Host.AddSerilog();
builder.Services.AddScoped<AuthFilter>();
builder.Services.AddScoped<RoleFilter>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseCors(frontPolicy);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
