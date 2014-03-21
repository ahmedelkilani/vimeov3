using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VimeoApi.Web.Models
{
    public class AuthDisplayModel
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public string Scope { get; set; }
    }
}