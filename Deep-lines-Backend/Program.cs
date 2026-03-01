using Deep_lines_Backend.BLL.Mapping;
using Deep_lines_Backend.DAL.Context;
using Deep_lines_Backend.Extensions;
using Microsoft.EntityFrameworkCore;
using Deep_lines_Backend.BLL.DTOs.UserEntity;
using Deep_lines_Backend.BLL.Interfaces.IService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Allow requests from the Angular dev server (http://localhost:4200)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowFront",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefultConnection");

builder.Services.AddDbContext<AppDbContext>(options=> options.UseSqlServer(connection));

builder.Services.addServiceLifetimesExtention();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();

    app.UseCors("AllowFront");

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

// Seed default admin user
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var empService = services.GetRequiredService<IEmployeeService>();
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetService<ILoggerFactory>()?.CreateLogger("Seed");

        var adminEmail = configuration["DefaultAdmin:Email"] ?? "admin@local";
        var existing = empService.GetByEmail(adminEmail).Result;
        if (existing == null)
        {
            var adminDto = new AddUserDTO
            {
                Name = configuration["DefaultAdmin:Name"] ?? "Administrator",
                Email = adminEmail,
                Password = configuration["DefaultAdmin:Password"] ?? "Admin@123",
                Role = configuration["DefaultAdmin:Role"] ?? "Admin",
                Phone = configuration["DefaultAdmin:Phone"] ?? string.Empty,
                Address = configuration["DefaultAdmin:Address"] ?? string.Empty,
                status = configuration["DefaultAdmin:Status"] ?? "Active",
                department = configuration["DefaultAdmin:Department"] ?? string.Empty,
                jopTitle = configuration["DefaultAdmin:JobTitle"] ?? "Administrator",
                employmentType = configuration["DefaultAdmin:EmploymentType"] ?? string.Empty,
                joinedDate = DateTime.UtcNow
            };

            empService.AddUser(adminDto).GetAwaiter().GetResult();
            logger?.LogInformation("Default admin user created: {Email}", adminEmail);
        }
        else
        {
            logger?.LogInformation("Default admin user already exists: {Email}", adminEmail);
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetService<ILoggerFactory>()?.CreateLogger("Seed");
        logger?.LogError(ex, "An error occurred while seeding the default admin user.");
    }
}

app.Run();
