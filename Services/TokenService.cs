﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserApi_Identity.Models;

namespace UserApi_Identity.Services;

public class TokenService
{
    private IConfiguration _configuration;
    
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    // AUTENTICAÇÃO
    public string GenerateToken(User user)
    {
        /* o que vai ser reivindicado no token?
         *   - username
         *   - id
         *   - birthDate
         */
        Claim[] claims = new Claim[]
        {
            new Claim("username", user.UserName!),
            new Claim("id", user.Id),
            new Claim(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
            new Claim(ClaimTypes.Role, user.Profile)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]!));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken
        (
            expires: DateTime.UtcNow.AddMinutes(10),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}