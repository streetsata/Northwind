using Contracts;
using LoggerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Entites;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Northwind
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration configuration)
        {
            var conStr = configuration.GetConnectionString("localDB");
            if (conStr != null)
                services.AddDbContext<NorthwindContext>(o => o.UseSqlServer(conStr));
            else
                throw new System.Exception("ConnectionString is null");
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Northwind API", Version = "v1" });
            });
        }
    }
}
