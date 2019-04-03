using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCStudio.Utility.Security
{
    public static class SigningKey
    {
        private static SecurityKey _signingKey;
        public static SecurityKey GetSigningKey(string secretKey)
        {
            if (_signingKey == null)
            {
                _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            }

            return _signingKey;
        }
    }
}
