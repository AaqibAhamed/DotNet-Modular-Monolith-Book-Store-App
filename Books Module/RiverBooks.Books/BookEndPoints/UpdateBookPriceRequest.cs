namespace RiverBooks.Books.BookEndPoints;

public record UpdateBookPriceRequest(Guid Id, decimal NewPrice);
