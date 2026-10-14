namespace RiverBooks.Books;

internal interface IReadOnlyBookRepository
{
  Task<Book?> GetBookByIdAsync(Guid guid);
  Task<List<Book>> GetBooksListAsync();
}
