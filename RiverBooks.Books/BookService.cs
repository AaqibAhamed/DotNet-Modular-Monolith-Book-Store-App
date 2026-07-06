namespace RiverBooks.Books;

internal class BookService : IBookService
{
    public List<BookDto> ListBooks()
    {
        return [
          new BookDto(Guid.NewGuid(), "The Great Gatsby", "F. Scott Fitzgerald", 1925),
          new BookDto(Guid.NewGuid(), "To Kill a Mockingbird", "Harper Lee", 1960),
          new BookDto(Guid.NewGuid(), "1984", "George Orwell", 1949)
        ];
    }
}


