namespace RiverBooks.Books;

internal interface IBookRepository : IReadOnlyBookRepository
{
    Task AddBookAsync(Book book);
    Task DeleteBookAsync(Book book);
    Task SaveChangesAsync();
}
