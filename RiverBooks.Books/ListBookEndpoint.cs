using FastEndpoints;
using Microsoft.AspNetCore.Builder;

namespace RiverBooks.Books;

internal class ListBookEndpoint(IBookService bookService) : EndpointWithoutRequest<ListBooksResponse>
{
    private readonly IBookService _bookService = bookService;

    public override void Configure()
    {
        Get("/books");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "List all books";
            s.Description = "Returns a list of all books in the system.";
            s.Response<ListBooksResponse>(200, "List of books");
        });
    }

    // public override Task<ListBooksResponse> ExecuteAsync(CancellationToken cancellationToken = default)
    // {
    //     var books = _bookService.ListBooks();

    //     return Task.FromResult(new ListBooksResponse
    //     {
    //         Books = books
    //     });
    // }

    public override async Task HandleAsync(CancellationToken cancellationToken = default)
    {
        var books = _bookService.ListBooks();

        await Send.OkAsync(new ListBooksResponse()
        {
            Books = books
        }, cancellation: cancellationToken);
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