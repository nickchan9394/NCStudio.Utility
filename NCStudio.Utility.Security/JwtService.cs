using NCStudio.Utility.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace NCStudio.Utility.Security
{
    public class JwtService : IJwtService
    {
        public JsonWebToken GenerateJsonWebToken(string username,string[] roles,TokenProviderOptions options,params KeyValuePair<string,string>[] otherClaims)
        {
            var now = DateTime.UtcNow;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.DateTimeToUnixTimestamp().ToString(), ClaimValueTypes.Integer64),
            };

            claims.AddRange(otherClaims.Select(c => new Claim(c.Key, c.Value)));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(1)),
                signingCredentials: options.SigningCredentials
                );

            var result = new JsonWebToken
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt),
                ExpiresIn = (int)options.Expiration.TotalSeconds
            };

            return result;
        }
    }
}
