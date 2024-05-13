
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Reflection;
using Microsoft.OpenApi.Models;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.Filters;
using ProgQuizWebsite.Services.Interfaces;
using ProgQuizWebsite.Infrastracture.Contexts;
using Core.Base.Service.Interfaces;
using Core.Base.Service.Implementations;
using ProgQuizWebsite.Services.Implementations.MainServices;
using ProgQuizWebsite.Infrastracture.Mappers;
using ProgQuizWebsite.Infrastracture.Validators.PostModelValidators;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Infrastracture.UnitOfWork;
using ProgQuizWebsite.Services.Implementations.AdditionalServices;
using ProgQuizWebsite.Infrastracture.Filters;
using Core.Constants;
using ProgQuizWebsite.Infrastracture.Startups;
using Core.Redis;

var builder = WebApplication.CreateBuilder(args);
var defaultPolicyName = "FrontPolicy";
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddCors(options =>
{
    options.AddPolicy(defaultPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:5173").WithExposedHeaders(SpecialConstants.ContentCountHeaderName)
        .AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.Converters.Add(new StringEnumConverter
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        });
    })
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDomain(builder.Configuration);

builder.Services.AddRedis(builder.Configuration);

builder.Services.AddScoped<QuizElementsExceptionFilter>();

builder.Services.AddServices();


builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(QuizMapper)));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CategoryValidator));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer().AddSwaggerGenNewtonsoftSupport();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo() { Title = "QuizAPI" });
    opt.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(defaultPolicyName);

app.UseAuthorization();

app.MapControllers();

using (var scope =
  app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<QuizAppContext>())
	context.Database.Migrate();

app.Run();
