using System;
using System.Collections.Generic;
using System.Text;

namespace NCStudio.Utility.Security
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
