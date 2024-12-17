using API.Data;
using API.Interfaces;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
           IConfiguration config)
        {
            services.AddScoped<IPostItemRepository,PostItemRepository>();
            services.AddScoped<IPlaceRepository, PlaceRepository>();
            services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });
            //Add Cors for alow permisson to API from Client App
            services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder => builder.WithOrigins("http://localhost:4200")
                                          .AllowAnyHeader()
                                          .AllowAnyMethod());
                });
            //Add maper to conver Dto object to Task Model object
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
