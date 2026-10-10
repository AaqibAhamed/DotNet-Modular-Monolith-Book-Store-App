namespace RiverBooks.Books;

internal class BookService(IBookRepository bookRepository) : IBookService
{
  private readonly IBookRepository _bookRepository = bookRepository;

  public async Task CreateBookAsync(BookDto newBook)
  {
    var book = new Book(newBook.Id, newBook.Title, newBook.Author, newBook.Price);

    await _bookRepository.AddBookAsync(book);

    await _bookRepository.SaveChangesAsync();
  }

  public async Task DeleteBookByIdAsync(Guid bookId)
  {
    var bookToDelete = await _bookRepository.GetBookByIdAsync(bookId);

    if (bookToDelete is not null)
    {
      await _bookRepository.DeleteBookAsync(bookToDelete);
      await _bookRepository.SaveChangesAsync();
    }
  }

  public async Task<BookDto> GetBookByIdAsync(Guid bookId)
  {
    var book = await _bookRepository.GetBookByIdAsync(bookId);

    // TODO : Will handle 404 later
    return new BookDto(book!.Id, book.Title, book.Author, book.Price);
  }

  public async Task<List<BookDto>> ListBooksAsync()
  {
    var books = (await _bookRepository.GetBooksListAsync())
              .Select(book => new BookDto(book.Id, book.Title, book.Author, book.Price))
              .ToList();

    return books;

  }

  public async Task UpdateBookPriceAsync(Guid bookId, decimal newPrice)
  {
    // TODO -validate the price first

    var book = await _bookRepository.GetBookByIdAsync(bookId);

    // TODO - Will handle 404 later

    book!.UpdatePrice(newPrice);

    await _bookRepository.SaveChangesAsync();
  }


}


