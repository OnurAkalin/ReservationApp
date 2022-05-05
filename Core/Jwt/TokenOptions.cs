namespace Core.Jwt;

public class TokenOptions
{
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int AccessTokenExpiration { get; set; }
    public string SecurityKey { get; set; }
    public string LoginProvider { get; set; }
    public string TokenName { get; set; }
}