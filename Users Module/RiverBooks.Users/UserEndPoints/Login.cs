using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.AspNetCore.Identity;
using RiverBooks.Users.UserEndPoints;

namespace RiverBooks.Users.UserEndpoints;

internal class Login : Endpoint<UserLoginRequest>
{
  private readonly UserManager<ApplicationUser> _userManager;

  public Login(UserManager<ApplicationUser> userManager)
  {
    _userManager = userManager;
  }
  public override void Configure()
  {
    Post("/users/login");
    AllowAnonymous();
  }

  public override async Task HandleAsync(UserLoginRequest request, CancellationToken ct)
  {
    var user = await _userManager.FindByEmailAsync(request.Email!);

    if (user == null)
    {
      await Send.UnauthorizedAsync(ct);
      return;
    }

    var loginSuccessful = await _userManager.CheckPasswordAsync(user, request.Password);

    if (!loginSuccessful)
    {
      await Send.UnauthorizedAsync(ct);
      return;
    }

    var jwtSecret = Config["Auth:JwtSecret"]!;

    var token = JwtBearer.CreateToken(o =>
    {
      o.SigningKey = jwtSecret;
      o.ExpireAt = DateTime.UtcNow.AddDays(1);
      //o.User.Claims.Add(("EmailAddress", request.Email));
      o.User.Claims.Add(("EmailAddress", user.Email!));
    });
    await Send.OkAsync(token, ct);
  }

}
