using Backend.DbContexts;
using Backend.Implementations;
using Backend.Interfaces;
using Backend.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// ✅ Serilog Logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

// ✅ Add Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Swagger Config
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Catalog API",
        Version = "v1",
        Description = "A B2B API to help salesmen showcase products to business owners.",
        Contact = new OpenApiContact
        {
            Name = "Ammar Arab",
            Email = "oammar@outlook.sa",
            Url = new Uri("https://0ammar.github.io/Personal-site/")
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by space and your token"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
        c.IncludeXmlComments(xmlPath);
});

// ✅ JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidAudience = config["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Jwt:Key"]!)
            )
        };
    });

builder.Services.AddAuthorization();

// ✅ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// ✅ Data Protection and Helper
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "CatalogKeys")))
    .SetApplicationName("Catalog-App");

builder.Services.AddSingleton<PasswordHelper>();

// ✅ Decrypt DB password if needed
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var passwordHelper = scope.ServiceProvider.GetRequiredService<PasswordHelper>();

    var rawConnectionString = config.GetConnectionString("CatalogConnection")!;
    string finalConnectionString = rawConnectionString;

    if (rawConnectionString.Contains("Password=ENC_"))
    {
        var start = rawConnectionString.IndexOf("Password=ENC_") + "Password=ENC_".Length;
        var end = rawConnectionString.IndexOf(';', start);
        var encrypted = rawConnectionString.Substring(start, end - start);

        var decrypted = passwordHelper.Decrypt(encrypted);
        finalConnectionString = rawConnectionString.Replace("Password=ENC_" + encrypted, $"Password={decrypted}");
    }

    builder.Services.AddDbContext<CatalogContext>(options =>
        options.UseSqlServer(finalConnectionString));
}

// ✅ Application Services
builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();
builder.Services.AddScoped<IItemServices, ItemServices>();
builder.Services.AddScoped<IUserServices, UserServices>();

// ✅ Build App
var app = builder.Build();

// ✅ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ✅ Run
try
{
    Log.Information("App started successfully.");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start.");
}
finally
{
    Log.CloseAndFlush();
}
