
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
using ProgQuizWebsite.Services.Quizzes.Interfaces;
using Core.Base.Service.Interfaces;
using Core.Base.Service.Implementations;
using ProgQuizWebsite.Services.Quizzes.Implementations.MainServices;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Infrastracture.Quizzes.UnitOfWork;
using ProgQuizWebsite.Services.Quizzes.Implementations.AdditionalServices;
using Core.Constants;
using Core.Redis;
using Core.Logging;
using Serilog;
using ProgQuizWebsite.Infrastracture.Quizzes.Startups;
using ProgQuizWebsite.Infrastracture.Quizzes.Filters;
using ProgQuizWebsite.Infrastracture.Quizzes.Validators.PostModelValidators;
using ProgQuizWebsite.Infrastracture.Contexts;
using ProgQuizWebsite.Infrastracture.Mappers;
using UserService.Infrastructure.Filters;
using UserService.Infrastructure.Startups;
using ProgQuizWebsite.Infrastracture.Notifications.Startups;
using ProgQuizWebsite.Infrastracture.Messaging;
using Minio;
using Core.Emailing.Startup;
using Core.Emailing;
using Core.MinIO;

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

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddUserDomain(builder.Configuration);
builder.Services.AddQuizDomain(builder.Configuration);
builder.Services.AddNotificationsDomain();

builder.Host.AddSerilog();
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddMinIOService(builder.Configuration);
builder.Services.AddEmailing(builder.Configuration);
builder.Services.AddUserServices();
builder.Services.AddQuizServices();
builder.Services.AddNotificationsServices();

builder.Services.AddMassTransitMessaging(builder.Configuration);

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(QuizAppMapper)));
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

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseCors(defaultPolicyName);

app.UseAuthorization();

app.MapControllers();

app.Run();
