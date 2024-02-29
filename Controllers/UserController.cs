using Microsoft.AspNetCore.Mvc;
using UserApi_Identity.Data.Dtos;
using UserApi_Identity.Services;

namespace UserApi_Identity.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("cadastro")]
    public async Task<IActionResult> PostUser(CreateUserDto dto)
    {
        await _userService.Register(dto);
        return Ok("Usuario cadastrado!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto)
    {
        var token = await _userService.Login(dto);
        return Ok(token);
    }
}