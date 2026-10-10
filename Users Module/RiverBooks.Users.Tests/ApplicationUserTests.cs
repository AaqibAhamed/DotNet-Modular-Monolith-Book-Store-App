namespace RiverBooks.Users.Tests;

public class ApplicationUserTests
{
  [Fact]
  public void AddItemToCart_WhenBookAlreadyExists_UpdatesExistingItem()
  {
    var bookId = Guid.NewGuid();
    var user = new ApplicationUser();
    var existingItem = new CartItem(bookId, "Original description", 2, 10m);
    var addedItem = new CartItem(bookId, "Updated description", 3, 12m);

    user.AddItemToCart(existingItem);
    user.AddItemToCart(addedItem);

    var cartItem = Assert.Single(user.CartItems);
    Assert.Equal(5, cartItem.Quantity);
    Assert.Equal("Updated description", cartItem.Description);
    Assert.Equal(12m, cartItem.UnitPrice);
  }
}
