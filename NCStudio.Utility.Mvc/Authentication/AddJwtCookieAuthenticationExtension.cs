using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NCStudio.Utility.Security;
using System;
using System.Threading.Tasks;

namespace NCStudio.Utility.Mvc.Authentication
{
    public static class AddJwtCookieAuthenticationExtension
    {
        public static void AddJwtCookieAuthentication(this IServiceCollection services,
            string secret_key, string issuer = "NC", string audience = "NCUser")
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SigningKey.GetSigningKey(secret_key),

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer =  issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = audience,

                // Validate the token expiry
                ValidateLifetime = true,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }
            ).AddCookie((options) => {
                options.Cookie.Name = "access_token";
                options.Cookie.HttpOnly = true;
                options.TicketDataFormat = new CustomJwtDataFormat(
                      SecurityAlgorithms.HmacSha256,
                      tokenValidationParameters);
                options.Events.OnRedirectToAccessDenied = (context) =>
                {
                    return Task.Run(() =>
                    {
                        context.Response.StatusCode = 401;
                    });
                };
                options.Events.OnRedirectToLogin = (context) => {
                    return Task.Run(() => {
                        context.Response.StatusCode = 401;
                    });
                };
            });
        }
    }
}
