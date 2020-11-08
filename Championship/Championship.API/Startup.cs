using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Championship.API.Config;
using Championship.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
            services
                .AddCustomApiVersioning()
                .AddCustomSwagger()
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

        public static void AddCustomApplicationServices(this IServiceCollection services)
        {
            services.AddHttpClient<IMovieService, MovieService>();
            //services.AddSingleton<IBookService, BookService>();
        }
    }
}
