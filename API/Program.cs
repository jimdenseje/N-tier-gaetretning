
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            // Add services to the container.
            builder.Services.AddControllers();

            // Add Authentication
            var key = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                            ValidAudience = builder.Configuration["JwtSettings:Audience"],

                            IssuerSigningKey = new SymmetricSecurityKey(key)
                        };
                    });

            builder.Services.AddAuthorization();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Register DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("ConnectionString"),
                    sql => sql.MigrationsAssembly("DataAccessLayer")
                )
            );

            // Register DAL
            //builder.Services.AddScoped<IAgeGroupRepository, AgeGroupRepository>();

            // Register BLL
            //builder.Services.AddScoped<IAgeGroupService, AgeGroupService>();

            // Register DAL and BLL services automatically using reflection
            foreach (var name in new[] { "AgeGroup", "DailyChallenge", "Score", "User" })
            {
                var repoInterface = Type.GetType($"DataAccessLayer.Interfaces.I{name}Repository, DataAccessLayer");
                var repoClass = Type.GetType($"DataAccessLayer.Repositories.{name}Repository, DataAccessLayer");

                var serviceInterface = Type.GetType($"BusinessLogicLayer.Interfaces.I{name}Service, BusinessLogicLayer");
                var serviceClass = Type.GetType($"BusinessLogicLayer.Services.{name}Service, BusinessLogicLayer");

                if (repoInterface == null)
                    throw new Exception($"Could not find repository interface for {name}");
                if (repoClass == null)
                    throw new Exception($"Could not find repository class for {name}");
                if (serviceInterface == null)
                    throw new Exception($"Could not find service interface for {name}");
                if (serviceClass == null)
                    throw new Exception($"Could not find service class for {name}");

                builder.Services.AddScoped(repoInterface, repoClass);
                builder.Services.AddScoped(serviceInterface, serviceClass);
            }

            // Register AuthService separately since it doesn't have a corresponding repository
            builder.Services.AddScoped<IAuthService, AuthService>();

            // Build the app
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // Enable HTTPS redirection
            app.UseHttpsRedirection();

            // Enable authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Enable CORS
            app.UseCors("AllowAll");

            // Map controllers
            app.MapControllers();

            // Run the app
            app.Run();
        }
    }
}
