using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NSwag.Generation.AspNetCore;
using NSwag.Generation.Processors.Security;
using NSwag;
using PruebaTecnica.Services;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using PruebaTecnica.Services.Productos;
using PruebaTecnica.Services.ComboBox;

namespace PruebaTecnica.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddTransientServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IExecuteService, ExecuteService>();
            services.AddTransient<IProductoService, ProductoService>();
            services.AddTransient<IComboBoxService, ComboBoxService>();
        }

        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddOpenApiDocument(document =>
            {               

                document.DocumentName = "General";
                document.Description = "API de prueba Técnica";
                document.ApiGroupNames = new[] { "General" };
            });

            services.AddOpenApiDocument(document =>
            {

                document.DocumentName = "Productos";
                document.Description = "Sección de productos";
                document.ApiGroupNames = new[] { "Productos" };
            });

            services.AddOpenApiDocument(document =>
            {

                document.DocumentName = "ComboBox";
                document.Description = "Selectores";
                document.ApiGroupNames = new[] { "ComboBox" };
            });

            //services.AddOpenApiDocument(document => {                

            //    document.DocumentName = "Permisos";
            //    document.Description = "APIS exclusivas de Permisos";
            //    document.ApiGroupNames = new[] { "Permisos" };
            //});

            return services;
        }

        public static void AddConfigOptions<TOptions>(this IServiceCollection services, IConfiguration configuration, string section) where TOptions : class, new()
        {
            IConfigurationSection sec = configuration.GetSection(section);
            services.Configure<TOptions>(sec);
        }

        public static IServiceCollection AddSofCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                     builder => builder.WithOrigins()
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials());
            });

            return services;
        }

    }
}
