using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public TokenService
    (
        IConfiguration configuration,
        UserManager<User> userManager
    )
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task<TokenResponseDto> GenerateAsync(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var userClaims = new List<Claim>();

        userClaims.AddRoles(userRoles.ToList());
        userClaims.AddMobilePhone(user.PhoneNumber);
        userClaims.AddEmail(user.Email);
        userClaims.AddName(user.GetFullName());
        userClaims.AddNameIdentifier(user.Id.ToString());

        var tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptions>();

        var securityKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey);
        var credentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var expireDate = DateTime.Now.AddDays(tokenOptions.AccessTokenExpiration);

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
            UserId = user.Id,
            UserFullName = user.GetFullName(),
            SiteId = user.SiteId,
            UserRoles = userRoles.ToList()
        };
    }
}