using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static RiverBooks.Users.ApplicationUser;

namespace RiverBooks.Users.Infrastructure.Data;

public class UserStreetAddressConfiguration : IEntityTypeConfiguration<UserStreetAddress>
{
  public void Configure(EntityTypeBuilder<UserStreetAddress> builder)
  {
    builder.ToTable(nameof(UserStreetAddress));
    builder
      .Property(x => x.Id)
      .ValueGeneratedNever();

    builder.ComplexProperty(usa => usa.StreetAddress);
  }
}

