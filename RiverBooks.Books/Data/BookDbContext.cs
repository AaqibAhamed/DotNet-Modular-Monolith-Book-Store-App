using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books.Data;

public class BookDbContext : DbContext
{
  internal DbSet<Book> Books { get; set; }

  public BookDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
  {

  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // base.OnModelCreating(modelBuilder);
    modelBuilder.HasDefaultSchema("Books");

    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
  }
}
