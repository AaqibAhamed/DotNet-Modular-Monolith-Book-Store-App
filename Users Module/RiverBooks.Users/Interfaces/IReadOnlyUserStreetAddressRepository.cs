using RiverBooks.Users.Domain;
using static RiverBooks.Users.ApplicationUser;

namespace RiverBooks.Users.Interfaces;

public interface IReadOnlyUserStreetAddressRepository
{
  Task<UserStreetAddress?> GetById(Guid userStreetAddressId);
}

