using System.IO;
using System.Text.Json.Serialization;
using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace BugTracker.Api
{
    /// <summary>
    /// Настройка приложения
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Конструктор настройки
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Конфиуграция
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor();
            services.AddCors(c =>
            {
                c.AddPolicy("default", policy =>
                {
                    // сюда можно добавить адреса которые к нам смогут обращаться.
                });
            });

            services.AddDbContext<BugTrackerDbContext>(opt =>
            opt.UseInMemoryDatabase("BugTrackerDb"));
            services.AddControllers()

            .AddNewtonsoftJson(c
             =>
            {
                c.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
             .AddJsonOptions(
                c => c.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                );

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme).AddIdentityServerAuthentication("Bearer",
                c =>
                {
                    c.Authority = "http://localhost:4000";
                    c.RequireHttpsMetadata = false;
                    c.ApiName = "bugtracker.api";
                    c.EnableCaching = true;
                    c.SaveToken = true;
                }
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "BugTrackerApi",
                        Description = "апи для багтрекера с использованием ASP .NET Core 3.1. Для аутентификации используется IdenityServer. авторизация не реализована.",
                        Version = "v1"
                    });
                var filePathCore = Path.Combine(System.AppContext.BaseDirectory, "BugTracker.Api.Core.xml");
                c.IncludeXmlComments(filePathCore);
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "BugTracker.Api.xml");
                c.IncludeXmlComments(filePath);
            });


            AddDomainServices(services);
        }

        private static void AddDomainServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(IssueProfile).Assembly);
            services.AddTransient<IssueService>();
            services.AddTransient<MilestoneService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BugTrackerApi v1");
            }
            );


            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("default");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
