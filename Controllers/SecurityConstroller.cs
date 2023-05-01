using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BasicNet6Template.DTOs.Security;
using BasicNet6Template.Security;
using BasicNet6Template.Services.User;

namespace BasicNet6Template.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SecurityConstroller : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly ILogger<SecurityConstroller> _logger;

    public SecurityConstroller(ILogger<SecurityConstroller> logger, IConfiguration configuration, IUserService userService, IJwtService jwtService)
    {
        _logger = logger;
        _configuration = configuration;
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    [ApiKeyAuthorize]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        if (await _userService.ValidateCredentialsAsync(loginDTO.Usuario, loginDTO.Pass))
        {
            var jwt = _jwtService.GenerateJwt(loginDTO.Usuario);
            return Ok(new { Token = jwt });
        }
        return Unauthorized("Credenciales inválidas.");
    }

    [HttpGet("ApiKeyProtected")]
    [ApiKeyAuthorize]
    public IActionResult ApiKeyProtected()
    {
        return Ok("Esta acción está protegida por una API key.");
    }

    [HttpGet("JwtProtected")]
    [Authorize]
    public IActionResult JwtProtected()
    {
        return Ok("Esta acción está protegida por JWT.");
    }
}
