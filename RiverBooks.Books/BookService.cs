namespace RiverBooks.Books;

internal class BookService : IBookService
{
    public IEnumerable<BookDto> ListBooks()
    {
        return [
          new BookDto(Guid.NewGuid(), "The Great Gatsby", "F. Scott Fitzgerald", 1925),
          new BookDto(Guid.NewGuid(), "To Kill a Mockingbird", "Harper Lee", 1960),
          new BookDto(Guid.NewGuid(), "1984", "George Orwell", 1949)
        ];
    }
}




//

// public static void MapBookEndpoints(this IEndpointRouteBuilder routes)
// {
//     var group = routes.MapGroup("/api/Book").WithTags(nameof(Book));

//     group.MapGet("/", () => Results.Ok(BookRepository.GetAllBooks()))
//         .WithName("GetAllBooks")
//         .Produces<List<Book>>(StatusCodes.Status200OK);

//     group.MapGet("/{id}", (int id) =>
//     {
//         var book = BookRepository.GetBookById(id);
//         return book is not null ? Results.Ok(book) : Results.NotFound();
//     })
//     .WithName("GetBookById")
//     .Produces<Book>(StatusCodes.Status200OK)
//     .Produces(StatusCodes.Status404NotFound);

//     group.MapPost("/", (Book book) =>
//     {
//         BookRepository.AddBook(book);
//         return Results.Created($"/api/Book/{book.Id}", book);
//     })
//     .WithName("CreateBook")
//     .Produces<Book>(StatusCodes.Status201Created);

//     group.MapPut("/{id}", (int id, Book updatedBook) =>
//     {
//         var existingBook = BookRepository.GetBookById(id);
//         if (existingBook is null)
//         {
//             return Results.NotFound();
//         }

//         BookRepository.UpdateBook(id, updatedBook);
//         return Results.NoContent();
//     })
//     .WithName("UpdateBook")
//     .Produces(StatusCodes.Status204NoContent)
//     .Produces(StatusCodes.Status404NotFound);

//     group.MapDelete("/{id}", (int id) =>
//     {
//         var existingBook = BookRepository.GetBookById(id);
//         if (existingBook is null)
//         {
//             return Results.NotFound();
//         }

//         BookRepository.DeleteBook(id);
//         return Results.NoContent();
//     })
//     .WithName("DeleteBook")
//     .Produces(StatusCodes.Status204NoContent)
//     .Produces(StatusCodes.Status404NotFound);
// }