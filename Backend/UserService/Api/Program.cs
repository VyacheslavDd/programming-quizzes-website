using UserService.Infrastructure.Startups;
using Core.Logging;
using System.Reflection;
using UserService.Infrastructure.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDomain(builder.Configuration);
builder.Services.AddServices();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Host.AddSerilog();
builder.Services.AddScoped<AuthFilter>();
builder.Services.AddScoped<RoleFilter>();
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