using AutoMapper;
using Championship.API.Config;
using Championship.Application.Mappers;
using Championship.Application.Services;
using Championship.Application.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace Championship.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), AppDomain.CurrentDomain.GetAssemblies());

            services
                .AddCustomApiVersioning()
                .AddCustomSwagger()
                .AddCustomFluentValidation()
                .AddCustomApplicationServices();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Championship V1");
                c.RoutePrefix = string.Empty;
            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    static class CustomExtensionsMethods
    {
        public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.Conventions.Add(new VersionByNamespaceConvention());
            });
            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Championship API",
                    Version = "v1"
                });
                c.OperationFilter<RemoveVersionParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
            });
            return services;
        }
        public static IServiceCollection AddCustomFluentValidation(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidationFilter());
            })
            .AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<CreateTournamentViewModelValidator>();
            });
            return services;
        }

        public static void AddCustomApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient<IMovieService, MovieService>();
            services.AddSingleton<ITournamentService, TournamentService>();
        }

    }
}
