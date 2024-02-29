using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UserApi_Identity.Data.Dtos;
using UserApi_Identity.Models;

namespace UserApi_Identity.Services;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager; // possui metodos que ajudam na autenticação dos usuarios
    private readonly SignInManager<User> _signInManager;
    private readonly TokenService _tokenService;

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Register(CreateUserDto dto)
    {
        User user = _mapper.Map<User>(dto);
        
        // a senha é guadada como um hash, o que é fundamental para a segurança
        var res = await _userManager.CreateAsync(user, dto.Password);

        if (!res.Succeeded) 
            throw new ApplicationException("Falha no cadastro de usuario");
    }

    public async Task<string> Login(LoginUserDto dto)
    {
        // ele pede o username e senha separados pq é o que é passado no login
        var res = await _signInManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

        if (!res.Succeeded)
        {
            throw new ApplicationException("Usuario nao autenticado!");
        }
        
        // busco o meu usuario inteiro para ser passado no token
        var user = _signInManager.UserManager.Users.FirstOrDefault(user =>
            user.NormalizedUserName == dto.UserName.ToUpper());
        
        var token = _tokenService.GenerateToken(user);
        
        return token;
    }
}