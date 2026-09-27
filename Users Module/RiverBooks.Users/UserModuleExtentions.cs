using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;


namespace RiverBooks.Users;

public static class UserModuleExtentions
{
  public static IServiceCollection AddUserModuleServices(this IServiceCollection services, ConfigurationManager config, ILogger logger)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");

    services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));

    services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDbContext>();

    // Add User Services
    // services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();

    logger.Information("{Module} module services registered", "Users");

    return services;
  }

}
