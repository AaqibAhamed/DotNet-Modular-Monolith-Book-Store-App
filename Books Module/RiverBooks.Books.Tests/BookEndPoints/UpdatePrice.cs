namespace RiverBooks.Books.Tests.BookEndPoints;

public class BookTests
{
  [Fact]
  public void UpdatePrice_ChangesPrice()
  {
    var book = new Book(
        Guid.NewGuid(),
        "The Fellowship of the Ring",
        "J.R.R. Tolkien",
        10m);

    book.UpdatePrice(15m);

    Assert.Equal(15m, book.Price);
  }
}
