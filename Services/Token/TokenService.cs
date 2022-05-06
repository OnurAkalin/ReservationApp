﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public TokenService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<TokenResponseDto> GenerateTokenAsync(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var userClaims = new List<Claim>();

        userClaims.AddRoles(userRoles.ToList());
        userClaims.AddMobilePhone(user.PhoneNumber);
        userClaims.AddName(user.GetFullName());

        var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();

        var securityKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey);
        var credentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var expireDate = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);

        var tokenTemplate = new JwtSecurityToken
        (
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            claims: userClaims,
            expires: expireDate,
            signingCredentials: credentials
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenTemplate);
        
        await _userManager.SetAuthenticationTokenAsync(user, tokenOptions.LoginProvider, tokenOptions.TokenName, token);

        return new TokenResponseDto
        {
            Token = token,
            Expiration = expireDate,
            UserRoles = userRoles.ToList()
        };
    }
}