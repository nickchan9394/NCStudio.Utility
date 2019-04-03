using System;
using System.Collections.Generic;
using System.Text;

namespace NCStudio.Utility.Security
{
    public interface IJwtService
    {
        JsonWebToken GenerateJsonWebToken(string username,string[] roles,TokenProviderOptions options,
            params KeyValuePair<string,string>[] otherClaims);
    }
}
