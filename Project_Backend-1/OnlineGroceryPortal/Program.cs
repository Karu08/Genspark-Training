using OnlineGroceryPortal.Contexts;
using OnlineGroceryPortal.Interfaces;
using OnlineGroceryPortal.Repositories;
using OnlineGroceryPortal.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using AspNetCoreRateLimit;
using System.Text;
using OnlineGroceryPortal.Services.Misc;
using System.Security.Claims;
using Serilog;



// Log.Logger = new LoggerConfiguration()
//     .WriteTo.Console()
//     .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
//     .Enrich.FromLogContext()
//     .MinimumLevel.Debug()
//     .CreateLogger();

// Log.Information("Starting up application...");


var builder = WebApplication.CreateBuilder(args);
//builder.Host.UseSerilog(); 

var configuration = builder.Configuration;

IdentityModelEventSource.ShowPII = true;

builder.Services.AddControllers();

builder.Services.AddDbContext<GroceryDbContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Online Grocery Portal API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.\n\nEnter 'Bearer' followed by your token.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),
            RoleClaimType = ClaimTypes.Role
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
    {
        var bearerToken = context.Request.Headers["Authorization"].ToString();
        var token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
        Console.WriteLine($"🔒 Incoming token (clean): {token}");
        return Task.CompletedTask;
    },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"JWT auth failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("JWT token validated");
                return Task.CompletedTask;
            }
        };
    });


builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSignalR();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:5500", "http://127.0.0.1:5500", "http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 429)
    {
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync("{\"success\": false, \"message\": \"Rate limit exceeded. Try again later.\"}");
    }
});

app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<OrderHub>("/orderHub");
});

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 401)
    {
        Console.WriteLine("Unauthorized - Token may be invalid or expired.");
    }
});

app.Run();
