using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RiverBooks.Users;

public static class UserModuleExtentions
{
  public static IServiceCollection AddUserModuleServices(this IServiceCollection services, ConfigurationManager config)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");

    services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));

    services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDbContext>();

    // Add User Services
    // services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();

    return services;
  }

}
