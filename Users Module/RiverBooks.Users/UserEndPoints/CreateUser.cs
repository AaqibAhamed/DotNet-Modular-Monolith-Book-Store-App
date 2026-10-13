
using Ardalis.Result.AspNetCore;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.UseCases.User.Create;
using RiverBooks.Users.UserEndPoints;

namespace RiverBooks.Users.UserEndpoints;

internal class CreateUser : Endpoint<CreateUserRequest>
{
  // private readonly UserManager<ApplicationUser> _userManager;

  private readonly IMediator _mediator;

  public CreateUser(/*UserManager<ApplicationUser> userManager */ IMediator mediator)
  {
    // _userManager = userManager;
    _mediator = mediator;
  }

  public override void Configure()
  {
    Post("/users");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
  {

    var command = new CreateUserCommand(request.Email, request.Password);

    var result = await _mediator.Send(command);

    if (!result.IsSuccess)
    {
      await Send.ResultAsync(result.ToMinimalApiResult());
      return;
    }

    await Send.OkAsync();

    // var newUser = new ApplicationUser
    // {
    //   Email = request.Email,
    //   UserName = request.Email
    // };

    // await _userManager.CreateAsync(newUser, request.Password);

    // await Send.OkAsync(cancellationToken);

  }
}
