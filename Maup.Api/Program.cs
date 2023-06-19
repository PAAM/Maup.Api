using AutoMapper;
using Maup.Core.Entities;
using Maup.Core.Interfaces;
using Maup.Core.Services;
using Maup.Infrastructure.Data;
using Maup.Infrastructure.Filters;
using Maup.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container and ReferenceLoopHandling.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
}).AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});


//Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:Secret"]))
    };
});


//.AddFluentValidation(options =>
//{
//    // Validate child properties and root collection elements
//    options.ImplicitlyValidateChildProperties = false;
//    options.ImplicitlyValidateRootCollectionElements = true;

//    // Automatic registration of validators in assembly
//    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//});



//(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);



//Database connection 
builder.Services.AddDbContext<MUPContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MUP")));

//Service Register
builder.Services.AddTransient<IPropertyService, PropertyService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<Pagination>(builder.Configuration.GetSection("Pagination"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.EnableAnnotations();
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MAUP Api",
        Version = "v1",
        Description = "<b>Introduction:</b>\r\nMAUP Api provides a set of endpoints to manage real estate listings and related information. This API is designed to facilitate common operations in the real estate business, allowing users to create, retrieve, update, and delete real estate listings.\r\n\r\n<b>Authentication:</b>\r\nThe API requires authentication using API key-based authentication. To access the endpoints, you need to include your API key in the request headers."
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();