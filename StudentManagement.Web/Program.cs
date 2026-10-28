using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Serilog;
using Student_Management_System.Middleware;
using StudentManagement.Application;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;
using StudentManagement.Infrastructure.BackgroundServices;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext());

var jwtKey = builder.Configuration["Jwt:Key"];

if (string.IsNullOrWhiteSpace(jwtKey) || jwtKey.Length < 32)
{
    throw new InvalidOperationException(
        "Jwt:Key is missing or shorter than 32 characters. " +
        "Set it with user-secrets (development) or the Jwt__Key environment variable.");
}

if (string.IsNullOrWhiteSpace(builder.Configuration.GetConnectionString("DefaultConnection")))
{
    throw new InvalidOperationException(
        "ConnectionStrings:DefaultConnection is missing. " +
        "Set it with user-secrets (development) or the ConnectionStrings__DefaultConnection environment variable.");
}

builder.Services.AddDbContext<StudentManagementDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IApplicationDbContext>(
    provider => provider.GetRequiredService<StudentManagementDbContext>());

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter());
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddValidatorsFromAssembly(
    typeof(StudentManagement.Application.AssemblyMarker).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(
        typeof(StudentManagement.Application.AssemblyMarker).Assembly);

    cfg.AddOpenBehavior(typeof(StudentManagement.Application.Behaviors.ValidationBehavior<,>));
});

builder.Services.AddExceptionHandler<Student_Management_System.Middleware.ValidationExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

    options.AddSecurityRequirement(document =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference("Bearer", document)]
                = []
        });
});


builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();

builder.Services.AddScoped<PasswordService>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"] ?? ""
                )
            )
        };
    });

builder.Services.AddHostedService<RefreshTokenCleanupService>();

builder.Services.AddHostedService<AccessLogCleanupService>();  

var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? [];

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;

    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddHealthChecks();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<StudentManagementDbContext>();

    var passwordService = scope.ServiceProvider
        .GetRequiredService<PasswordService>();

    await dbContext.Database.MigrateAsync();

    var adminExists = await dbContext.Users
        .AnyAsync(x => x.UserRole == UserRole.Admin);

    if (!adminExists)
    {
        var adminPassword = builder.Configuration["Seed:AdminPassword"];

        if (string.IsNullOrWhiteSpace(adminPassword))
        {
            throw new InvalidOperationException(
                "No admin exists and Seed:AdminPassword is not set. " +
                "Set it with user-secrets (development) or the Seed__AdminPassword environment variable.");
        }

        var admin = new User
        {
            Username = builder.Configuration["Seed:AdminUsername"] ?? "admin",
            Email = builder.Configuration["Seed:AdminEmail"] ?? "admin@school.com",
            PasswordHash = passwordService.HashPassword(adminPassword),
            UserRole = UserRole.Admin,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dbContext.Users.Add(admin);

        await dbContext.SaveChangesAsync();
    }

    app.UseForwardedHeaders();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<AccessLogMiddleware>();

    app.UseSerilogRequestLogging(options =>
    {
        options.GetLevel = (httpContext, elapsed, ex) =>
            httpContext.Request.Path.StartsWithSegments("/health")
                ? LogEventLevel.Verbose
                : ex != null || httpContext.Response.StatusCode >= 500
                    ? LogEventLevel.Error
                    : LogEventLevel.Information;
    });

    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    app.UseCors("Frontend");

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.MapHealthChecks("/health");

    app.Run();
}
    