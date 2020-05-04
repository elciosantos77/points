using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Points.Api.Configurations;
using Points.Infra.CrossCutting.IoC;
using Swashbuckle.AspNetCore.Swagger;
using Points.Infra.CrossCutting.Identity;
using AspNetCore.Identity.MongoDbCore.Models;
using System;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Points.Api.Extensions;

namespace Points.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  var settingsKey = Configuration.GetSection(nameof(KeyPoints)).Get<KeyPoints>();
                  string key = settingsKey.Key;

                  var parameters = options.TokenValidationParameters;
                  parameters.ValidateIssuer = false;
                  parameters.ValidateAudience = false;
                  parameters.ValidateLifetime = true;
                  parameters.ValidateIssuerSigningKey = true;

                  parameters.IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(key));
                  options.Events = new JwtBearerEvents()
                  {
                      OnAuthenticationFailed = context =>
                      {
                          Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                          return Task.CompletedTask;
                      },
                      OnTokenValidated = context =>
                      {
                          Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                          return Task.CompletedTask;
                      }
                  };
              });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthorization(auth =>
            {
                var bearer = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();

                auth.DefaultPolicy = bearer;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "POINTS API", Version = "v1" });

                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Utilize o padrão de autenticação do JWT. Exemplo: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });

            var settings = Configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
            services.AddSingleton(settings);
            RegisterServices(services);

            services.AddIdentity<ApplicationUser, MongoIdentityRole>()
                    .AddMongoDbStores<ApplicationUser, MongoIdentityRole, Guid>(settings.ConnectionString, settings.DatabaseName)
                    .AddDefaultTokenProviders();

            services.AddScoped<RoleManager<IdentityRole>>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(option =>
                option.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "POINTS API V1");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddAutoMapperSetup();
            services.AddMediatR(typeof(Startup));
            BootStrapper.RegisterServices(services);
        }
    }
}
