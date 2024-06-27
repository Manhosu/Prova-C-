using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] usuario user)
    {
        if (_userService.ValidateCredentials(user.Email, user.Senha))
        {
            var token = _userService.GenerateToken(user.Email);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }
}
