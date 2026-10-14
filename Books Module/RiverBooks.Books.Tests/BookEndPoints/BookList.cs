using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using RiverBooks.Books.BookEndPoints;

namespace RiverBooks.Books.Tests.BookEndPoints;

public class BookList(Fixture fixture, ITestOutputHelper outputHelper) : TestBase<Fixture>
{
  [Fact]
  public async Task VerifyBooksCountAsync()
  {
    var testResult = await fixture.Client.GETAsync<ListBookEndpoint, ListBooksResponse>();

    testResult.Response.EnsureSuccessStatusCode();
    testResult.Result.Books.Count.Should().Be(3); //BookConfiguration has 3 books hardcoded
    outputHelper.WriteLine($"Received {testResult.Result.Books.Count} books.");
  }
}

