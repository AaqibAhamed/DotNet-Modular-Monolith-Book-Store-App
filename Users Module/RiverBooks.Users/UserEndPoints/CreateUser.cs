
using FastEndpoints;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.UserEndPoints;

namespace RiverBooks.Users.UserEndpoints;

internal class CreateUser : Endpoint<CreateUserRequest>
{
  private readonly UserManager<ApplicationUser> _userManager;

  public CreateUser(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }

  public override void Configure()
  {
    Post("/users");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
  {
    var newUser = new ApplicationUser
    {
      Email = request.Email,
      UserName = request.Email
    };

    await _userManager.CreateAsync(newUser, request.Password);

    await Send.OkAsync(cancellationToken);
  }
}
