using API.Data;
using API.Interfaces;
using API.Service;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(opt => 
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection")); //DefaultConnection defined in from appsettings.json file
            });

            services.AddCors();
            services.AddScoped<ITokenService,TokenService>(); //Scoped or Singleton is used vs Transient (too short lived) for http requests. Singleton is used for caching.
                                                          //use of interfaces makes testing easier, mock the implementation class
            return services;
        }

    }
}