using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NCStudio.Utility.Testing
{
    public class ResponseResult
    {
        public string Content { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
