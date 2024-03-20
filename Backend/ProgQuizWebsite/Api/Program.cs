
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

var builder = WebApplication.CreateBuilder(args);
var defaultPolicyName = "FrontPolicy";
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddCors(options =>
{
    options.AddPolicy(defaultPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
    });
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.Converters.Add(new StringEnumConverter
        {
            NamingStrategy = new CamelCaseNamingStrategy()
        });
    })
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<QuizAppContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("QuizDB"),
    m => m.MigrationsAssembly("ProgQuizWebsite")), ServiceLifetime.Scoped);

builder.Services.AddScoped<QuizElementsExceptionFilter>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IService<QuizSubcategory>, SubcategoryService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IService<Question>, QuestionService>();
builder.Services.AddScoped<IService<Answer>, AnswerService>();

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
