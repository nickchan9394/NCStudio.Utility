using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using NCStudio.Utility.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NCStudio.Utility.Testing
{
    public static class HttpTesting
    {

        public async static Task<ResponseResult> GetAsync(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(uri);

                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> GetAsync(string uri,
            string username,string[] roles,
            string cookieDomain = "",
            KeyValuePair<string,string>[] clamins=null,
            string secretKey = "1234567890123456",
            string audience="NCUser",string issuer="NC")
        {
            using (var httpClient = new HttpClient())
            {
                addTokenInCookie(httpClient, username, roles, cookieDomain, clamins, secretKey, audience, issuer);

                var response = await httpClient.GetAsync(uri);

                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> PostAsync(string uri,string content,Encoding encoding=null,string mediaType="application/json")
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .PostAsync(uri,new StringContent(content, encoding??Encoding.UTF8, mediaType));

                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> PostAsync(
            string uri, 
            string content, 
            string username,
            string[] roles,
            string cookieDomain = "",
            KeyValuePair<string, string>[] clamins = null,
            Encoding encoding = null, 
            string mediaType = "application/json",
            string secretKey = "1234567890123456",
            string audience = "NCUser", string issuer = "NC")
        {
            using (var httpClient = new HttpClient())
            {
                addTokenInCookie(httpClient, username, roles, cookieDomain, clamins, secretKey, audience, issuer);

                var response = await httpClient
                    .PostAsync(uri, new StringContent(content, encoding ?? Encoding.UTF8, mediaType));

                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> PutAsync(string uri, string content, Encoding encoding = null, string mediaType = "application/json")
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient
                    .PutAsync(uri, new StringContent(content, encoding ?? Encoding.UTF8, mediaType));

                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> PutAsync(string uri, string content,
            string username,
            string[] roles,
            string cookieDomain = "",
            KeyValuePair<string, string>[] clamins = null,
            Encoding encoding = null,
            string mediaType = "application/json",
            string secretKey = "1234567890123456",
            string audience = "NCUser", string issuer = "NC")
        {
            using (var httpClient = new HttpClient())
            {
                addTokenInCookie(httpClient, username, roles, cookieDomain, clamins, secretKey, audience, issuer);

                var response = await httpClient
                    .PutAsync(uri, new StringContent(content, encoding ?? Encoding.UTF8, mediaType));

                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> DeleteAsync(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.DeleteAsync(uri);

                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        public async static Task<ResponseResult> DeleteAsync(string uri,
            string username, string[] roles,
            string cookieDomain = "",
            KeyValuePair<string, string>[] clamins = null,
            string secretKey = "1234567890123456",
            string audience = "NCUser", string issuer = "NC")
        {
            using (var httpClient = new HttpClient())
            {
                addTokenInCookie(httpClient, username, roles, cookieDomain, clamins, secretKey, audience, issuer);

                var response = await httpClient.DeleteAsync(uri);

                var result = await response.Content.ReadAsStringAsync();
                return new ResponseResult
                {
                    Content = result,
                    StatusCode = response.StatusCode
                };
            }
        }

        private static void addTokenInCookie(HttpClient httpClient, string username, string[] roles,
                string cookieDomain,
                IList<KeyValuePair<string, string>> claims,
                string secretKey,
                string audience, string issuer)
        {
            httpClient.DefaultRequestHeaders.Remove("Cookie");
            var signingKey = SigningKey.GetSigningKey(secretKey);
            var options = new TokenProviderOptions
            {
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
            };
            var jwtService = new JwtService();

            JsonWebToken token;
            if (claims != null)
            {
                claims.Add(new KeyValuePair<string, string>("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
                token = jwtService.GenerateJsonWebToken(username, roles, options, claims.ToArray());
            }
            else
            {
                token = jwtService.GenerateJsonWebToken(username, roles, options, new KeyValuePair<string, string>("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
            }


            var cookieOptions = new CookieOptions
            {
                Domain = cookieDomain,
                Expires = DateTimeOffset.UtcNow.AddHours(8).AddDays(1).AddMinutes(-5)
            };

            var cookies = new List<string> {
                $"access_token={token.AccessToken}",
                $"username={username}",
                $"expires_in={token.ExpiresIn}"
            };
            httpClient.DefaultRequestHeaders.Add("Cookie", string.Join(";", cookies));
        }
    }
}
