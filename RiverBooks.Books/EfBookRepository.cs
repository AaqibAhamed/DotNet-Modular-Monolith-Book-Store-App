
using Microsoft.EntityFrameworkCore;

namespace RiverBooks.Books;

internal class EfBookRepository : IBookRepository
{
  private readonly BookDbContext _dbContext;

  public EfBookRepository(BookDbContext dbContext)
  {
    _dbContext = dbContext;
  }
  public Task AddBookAsync(Book book)
  {
    _dbContext.Add(book);
    return Task.CompletedTask;
  }

  public Task DeleteBookAsync(Book book)
  {
    _dbContext.Remove(book);
    return Task.CompletedTask;
  }

  public async Task<Book?> GetBookByIdAsync(Guid id)
  {
    return await _dbContext!.Books.FindAsync(id);
  }

  public async Task<List<Book>> GetBooksListAsync()
  {
    return await _dbContext.Books.ToListAsync();
  }

  public async Task SaveChangesAsync()
  {
    await _dbContext.SaveChangesAsync();
  }
}
