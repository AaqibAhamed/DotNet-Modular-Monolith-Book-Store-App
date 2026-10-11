namespace RiverBooks.Users.Interfaces;

public interface IApplicationUserRepository
{
  Task<ApplicationUser> GetUserWithAddressesByEmailAsync(string email);
  Task<ApplicationUser> GetUserWithCartByEmailAsync(string email);
  Task SaveChangesAsync();
}
