using Deep_lines_Backend.BLL.Mapping;
using Deep_lines_Backend.DAL.Context;
using Deep_lines_Backend.Extensions;
using Microsoft.EntityFrameworkCore;

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

app.Run();
