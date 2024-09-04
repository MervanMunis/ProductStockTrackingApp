using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EFCore;
using Services.Concrete;
using Services.Contracts;

namespace WebAPI.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, 
            IConfiguration configuration) => services.AddDbContext<RepositoryContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                    sqlOptions => sqlOptions.MigrationsAssembly("WebAPI")));

        public static void ConfigureIdentity(this IServiceCollection services) =>
            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequiredLength = 5;

                opts.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();

        public static void ConfigureRepositoryManager(this IServiceCollection services) => 
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        
    }
}
