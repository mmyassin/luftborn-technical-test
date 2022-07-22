using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Luftborn.TechnicalTest.Configuration;
using System;
using System.Linq;
using Luftborn.TechnicalTest.Extensions;
using Luftborn.TechnicalTest.EntityFrameworkCore.Repositories;
using Luftborn.TechnicalTest.Domain.Repositories;
using Luftborn.TechnicalTest.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Luftborn.TechnicalTest.Domain.Entities;
using Luftborn.TechnicalTest.Authorization.Users;
using Luftborn.TechnicalTest.Authorization;
using Microsoft.AspNetCore.Identity;
using Luftborn.TechnicalTest.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Luftborn.TechnicalTest.Host
{
    public class Startup
    {
        private const string _defaultCorsPolicyName = "localhost";

        private readonly IConfigurationRoot _appConfiguration;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IWebHostEnvironment env)
        {
            _hostingEnvironment = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            ConfigureAuth(services, _appConfiguration);
            services.AddDbContext<TechnicalTestDbContext>(options =>
            {
                options.UseSqlServer(_appConfiguration.GetConnectionString("Default"));
            });

            
            services.AddTransient(typeof(IDbContextProvider<>), typeof(DbContextProvider<>));
            services.AddTransient(typeof(AuthManager));
            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepositoryBase<,>));
            services.AddScoped(typeof(IRepository<,>), typeof(EfCoreRepositoryBase<,>));
            
            ConfigureTokenAuth(services);
            //Configure CORS for angular2 UI
            var corsOrigins = _appConfiguration["CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray();
            services.AddCors(
                options => options.AddPolicy(
                    _defaultCorsPolicyName,
                    builder => builder
                        .WithOrigins(corsOrigins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Luftborn Technical Test API", Version = "v1" });
                c.DocInclusionPredicate((docName, description) => true);
                // Define the BearerAuth scheme that's in use
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Luftborn TechnicalTest API v1"));
            }

            app.UseCors(_defaultCorsPolicyName); // Enable CORS!

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


        private void ConfigureTokenAuth(IServiceCollection services)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            services.AddTransient(sp => new TokenAuthConfiguration()
            {
                SecurityKey = securityKey,
                Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"],
                Audience = _appConfiguration["Authentication:JwtBearer:Audience"],
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                Expiration = TimeSpan.FromDays(1)
            });
        }

        private void ConfigureAuth(IServiceCollection services, IConfiguration configuration)
        {
            if (bool.Parse(configuration["Authentication:JwtBearer:IsEnabled"]))
            {
                services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                }).AddJwtBearer("JwtBearer", options =>
                {
                    options.Audience = configuration["Authentication:JwtBearer:Audience"];

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The signing key must match!
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                        // Validate the JWT Issuer (iss) claim
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                        // Validate the JWT Audience (aud) claim
                        ValidateAudience = true,
                        ValidAudience = configuration["Authentication:JwtBearer:Audience"],

                        // Validate the token expiry
                        ValidateLifetime = true,

                        // If you want to allow a certain amount of clock drift, set that here
                        ClockSkew = TimeSpan.Zero
                    };
                });
            }
        }
    }
}
