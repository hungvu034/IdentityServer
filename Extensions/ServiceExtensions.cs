using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Identity;
using IsApi.Configurations;
using IsApi.Persistence;
using IsApi.Repository;
using IsApi.Repository.Interfaces;
using IsApi.Service.Identity;
using IsApi.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace IsApi.Extensions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddCongigurationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddJwtKey(configuration);
            services.AddTransient<ITokenService, TokenService>();
            services.ConfigureDatabase(configuration);
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IdentityDbSeed>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IRoleRepository,RoleRepository>();
            services.AddJwtAuthentication(configuration);
            return services;
        }
         public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtKeySettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            Console.WriteLine("Authen Key + " + jwtKeySettings.Key);
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKeySettings.Key));
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = false
            };
            services.AddAuthentication(
                o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = tokenValidationParameters;
            });
        }
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("ConnectionString").Value;
            services.AddDbContext<IdentityDbContext>(
             config => config.UseSqlServer(connectionString)
            );
            Console.WriteLine("ConnectionString: " +connectionString);
        }

        public static void AddJwtKey(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtKeySettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            Console.WriteLine("Jwt key + " + jwtKeySettings.Key);
            services.AddSingleton(jwtKeySettings);
        }
    }
}