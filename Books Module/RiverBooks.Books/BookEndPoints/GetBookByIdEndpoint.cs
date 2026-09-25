using FastEndpoints;

namespace RiverBooks.Books.BookEndPoints;

internal class GetBookByIdEndpoint : Endpoint<GetBookByIdRequest, BookDto>
{
  private readonly IBookService _bookService;

  public GetBookByIdEndpoint(IBookService bookService)
  {
    _bookService = bookService;
  }

  public override void Configure()
  {
    Get("/books/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(GetBookByIdRequest req, CancellationToken ct)
  {
    var book = await _bookService.GetBookByIdAsync(req.Id);

    if (book is null)
    {
      await Send.NotFoundAsync(ct);
      return;
    }

    await Send.OkAsync(book, ct);
  }
}
