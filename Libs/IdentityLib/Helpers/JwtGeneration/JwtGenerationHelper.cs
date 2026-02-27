using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace IdentityLib.Helpers.JwtGeneration;

public static class JwtGenerationHelper
{
    /// <summary>
    /// Генерирует Access Token
    /// </summary>
    public static string GenerateAccessToken(
        Guid userId,
        string? scope,
        string issuer,
        string audience,
        int lifeTimeSeconds,
        RsaSecurityKey signingKey,
        bool isRestoringPassword = false)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64)
        };

        if (!string.IsNullOrWhiteSpace(scope))
        {
            claims.Add(new Claim("scope", scope));
        }

        // При восстановлении пароля выдаем временный 5-минутный токен
        var expires = isRestoringPassword
            ? DateTime.UtcNow.AddMinutes(5)
            : DateTime.UtcNow.AddSeconds(lifeTimeSeconds);

        return CreateToken(claims, issuer, audience, expires, signingKey);
    }

    /// <summary>
    /// Генерирует ID Token (OpenID Connect)
    /// </summary>
    public static string GenerateIdToken(
        Guid userId,
        string name,
        string email,
        string? lastName,
        IEnumerable<Claim>? customClaims,
        string issuer,
        string audience,
        int lifeTimeSeconds,
        RsaSecurityKey signingKey)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.Name, name),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.GivenName, name),
            new(JwtRegisteredClaimNames.FamilyName, lastName ?? ""),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        if (customClaims != null)
        {
            claims.AddRange(customClaims);
        }

        var expires = DateTime.UtcNow.AddSeconds(lifeTimeSeconds);
        return CreateToken(claims, issuer, audience, expires, signingKey);
    }

    /// <summary>
    /// Генерирует криптостойкий refresh token
    /// </summary>
    /// <param name="lengthInBytes">Длина в байтах (по умолчанию 32 = 256 бит)</param>
    public static string GenerateRefreshToken(int lengthInBytes = 32)
    {
        var buffer = new byte[lengthInBytes];
        RandomNumberGenerator.Fill(buffer);
        return Convert.ToBase64String(buffer);
    }

    /// <summary>
    /// Извлекает UserId из токена без проверки времени жизни
    /// </summary>
    public static Guid GetUserIdFromToken(string token, RsaSecurityKey signingKey, string issuer, string audience)
    {
        var principal = ValidateToken(token, signingKey, issuer, audience, validateLifetime: false);
        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier) ??
                          principal.FindFirst(JwtRegisteredClaimNames.Sub);

        return userIdClaim == null ? throw new InvalidOperationException("Token does not contain user id") : Guid.Parse(userIdClaim.Value);
    }

    /// <summary>
    /// Проверяет валидность токена
    /// </summary>
    public static bool IsTokenValid(string token, RsaSecurityKey signingKey, string issuer, string audience, bool validateLifetime = true)
    {
        try
        {
            ValidateToken(token, signingKey, issuer, audience, validateLifetime);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Создание токена
    /// </summary>
    private static string CreateToken(
        IEnumerable<Claim> claims,
        string issuer,
        string audience,
        DateTime expires,
        RsaSecurityKey signingKey)
    {
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.RsaSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expires,
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = credentials,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Валидация токена
    /// </summary>
    private static ClaimsPrincipal ValidateToken(
        string token,
        RsaSecurityKey signingKey,
        string issuer,
        string audience,
        bool validateLifetime)
    {
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = validateLifetime,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = signingKey,
            ClockSkew = TimeSpan.Zero
        };

        var handler = new JwtSecurityTokenHandler();
        return handler.ValidateToken(token, parameters, out _);
    }
}