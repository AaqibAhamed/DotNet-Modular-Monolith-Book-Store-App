using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.SharedKernel;
using RiverBooks.Users.Data;
using RiverBooks.Users.Infrastructure.Data;
using RiverBooks.Users.Interfaces;
using Serilog;


namespace RiverBooks.Users;

public static class UsersModuleServiceExtensions
{
  public static IServiceCollection AddUserModuleServices(this IServiceCollection services, ConfigurationManager config, ILogger logger, List<System.Reflection.Assembly> mediatRAssemblies)
  {
    string? connectionString = config.GetConnectionString("UsersConnectionString");

    services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(connectionString));

    services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<UsersDbContext>();

    // Add User Services
    services.AddScoped<IApplicationUserRepository, EfApplicationUserRepository>();
    services.AddScoped<IReadOnlyUserStreetAddressRepository, EfUserStreetAddressRepository>();


    // Add MediatR Domain Event Dispatcher
    services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

    // if using MediatR in this module, add any assemblies that contain handlers to the list
    mediatRAssemblies.Add(typeof(UsersModuleServiceExtensions).Assembly);

    logger.Information("{Module} module services registered", "Users");

    return services;
  }

}
