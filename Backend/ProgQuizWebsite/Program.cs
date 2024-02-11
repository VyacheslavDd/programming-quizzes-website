using Business_Layer.Services.Implementations;
using Business_Layer.Services.Interfaces;
using Business_Layer.Mappers;
using Data_Layer.Contexts;
using Data_Layer.Repositories.Implementations;
using Data_Layer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Text.Json.Serialization;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizModels;
using Data_Layer.UnitOfWork;
using Data_Layer.Models.QuizContentModels;
using Business_Layer.Validators.PostModelValidators;

var builder = WebApplication.CreateBuilder(args);
var defaultPolicyName = "FrontPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(defaultPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<QuizAppContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("QuizDB"),
    m => m.MigrationsAssembly("ProgQuizWebsite")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IService<LanguageCategory>, CategoryService>();
builder.Services.AddScoped<IService<QuizSubcategory>, SubcategoryService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IService<Question>, QuestionService>();
builder.Services.AddScoped<IService<Answer>, AnswerService>();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(QuizMapper)));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(CategoryValidator));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.Run();
