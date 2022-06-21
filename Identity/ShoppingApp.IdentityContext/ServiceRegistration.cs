
namespace ShoppingApp.IdentityContext
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddIdentityContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<IdentityDataContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
