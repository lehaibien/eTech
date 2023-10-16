using eTech.Context;
using eTech.Entities;
using eTech.Services;
using eTech.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Solve circular references

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
  {
    Title = "Weather Forecasts",
    Version = "v1"
  });
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
    Name = "Authorization",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer"
  });

  c.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });
});
// Add identity service
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(i =>
{
  i.Password.RequiredUniqueChars = 0;
  i.Password.RequireDigit = false;
  i.Password.RequireLowercase = false;
  i.Password.RequireUppercase = false;
  i.Password.RequireNonAlphanumeric = false;
  i.Password.RequiredLength = 8;
})
  .AddEntityFrameworkStores<ApplicationDbContext>()
  .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
  .AddJwtBearer(options =>
  {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidAudience = configuration["JWT:ValidAudience"],
      ValidIssuer = configuration["JWT:ValidIssuer"],
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
  });

// Db Context
// Add transient lifetime for dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(configuration.GetConnectionString("eTech")), ServiceLifetime.Transient);
// Dependency Injection

builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();

// CORS
var cors = new EnableCorsAttribute("AllowAll");
builder.Services.AddCors(options =>
{
  options.AddPolicy(cors.PolicyName!, policy =>
  {
    policy.WithOrigins("http://localhost:5173")
          .AllowAnyHeader()
          .AllowAnyMethod();
  });
});

// app

var app = builder.Build();
app.UseSwagger();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwaggerUI();
}

// ef core diagrams

app.UseCors(cors.PolicyName!);

app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(
    Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
  RequestPath = "/static",
  ServeUnknownFileTypes = true,
  OnPrepareResponse = async (ctx) =>
  {
    var policyProvider = app.Services.GetRequiredService<ICorsPolicyProvider>();
    var corsService = app.Services.GetRequiredService<ICorsService>();
    var policy = await policyProvider.GetPolicyAsync(ctx.Context, cors.PolicyName!);
    CorsResult corsResult = corsService.EvaluatePolicy(ctx.Context, policy);
    corsService.ApplyResult(corsResult, ctx.Context.Response);
  }
});

// app.UseStaticFiles(new StaticFileOptions
// {
//   FileProvider = new PhysicalFileProvider(
//     Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
//   RequestPath = "/static"
// });

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
