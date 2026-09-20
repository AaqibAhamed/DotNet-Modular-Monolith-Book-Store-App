using FastEndpoints;

namespace RiverBooks.Books;

internal class Delete(IBookService bookService) : Endpoint<DeleteBookRequest>
{
  private readonly IBookService _bookService = bookService;

  public override void Configure()
  {
    Delete("/books/{Id}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteBookRequest request,
    CancellationToken ct)
  {
    // TODO: Implement NotFound

    await _bookService.DeleteBookByIdAsync(request.Id);

    await Send.NoContentAsync(ct);
  }
}
