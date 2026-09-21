using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StudentManagement.Application;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;
using StudentManagement.Infrastructure.Data;
using StudentManagement.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StudentManagementDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IApplicationDbContext>(
    provider => provider.GetRequiredService<StudentManagementDbContext>());

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(StudentManagement.Application.AssemblyMarker).Assembly));

builder.Services.AddSwaggerGen();

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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<StudentManagementDbContext>();

    var passwordService = scope.ServiceProvider
        .GetRequiredService<PasswordService>();
    
    var adminExists = await dbContext.Users
        .AnyAsync(x => x.UserRole == UserRole.Admin);

    if (!adminExists)
    {
        var admin = new User
        {
            Username = "admin",
            Email = "admin@school.com",
            PasswordHash = passwordService.HashPassword("Admin@123"),
            UserRole = UserRole.Admin,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dbContext.Users.Add(admin);

        await dbContext.SaveChangesAsync();
    }

    var teacherUser = await dbContext.Users
        .FirstOrDefaultAsync(x => x.Username == "teacher01");

    if (teacherUser == null)
    {
        teacherUser = new User
        {
            Username = "teacher01",
            Email = "teacher01@school.com",
            PasswordHash = passwordService.HashPassword("Teacher@123"),
            UserRole = UserRole.Teacher,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        dbContext.Users.Add(teacherUser);

        await dbContext.SaveChangesAsync();
    }
    
    var teacher = await dbContext.Teachers
        .FirstOrDefaultAsync(x => x.UserId == teacherUser.Id);

    if (teacher == null)
    {
        teacher = new Teacher
        {
            UserId = teacherUser.Id,
            EmployeeNumber = "EMP-00001",
            FirstName = "John",
            LastName = "Teacher",
            Email = "teacher01@school.com",
            PhoneNumber = "9800000000",
            Gender = Gender.Male,
            IsActive = true
        };

        dbContext.Teachers.Add(teacher);

        await dbContext.SaveChangesAsync();
    }
}

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