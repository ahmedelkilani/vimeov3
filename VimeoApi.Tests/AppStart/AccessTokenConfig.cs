using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoApi.Tests.Setup
{
    public class AccessTokenConfig
    {
        public static string Token
        {
            get { return ""; }
        }

        public static string TokenType
        {
            get { return "bearer"; }
        }

        public static string Scop
        {
            get { return "private interact create edit upload delete public"; }
        }
    }
}