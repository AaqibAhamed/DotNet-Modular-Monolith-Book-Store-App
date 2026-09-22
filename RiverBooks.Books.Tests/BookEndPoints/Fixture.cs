using FastEndpoints.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RiverBooks.Books.Data;

namespace RiverBooks.Books.Tests.BookEndPoints;


public sealed class Fixture : AppFixture<Program>
{
  protected override async ValueTask SetupAsync()
  {
    await Services.GetRequiredService<BookDbContext>().Database.MigrateAsync();
  }
}
