#region License
/*
Author: Ahmed El-Kilani
 
The MIT License

Copyright (c) 2010-2014 Google, Inc. <a href="http://angularjs.org">http://angularjs.org</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
#endregion

using Newtonsoft.Json.Linq;
using OAuth2.Client;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using OAuth2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoApi.OAuth2.Clients.Impl
{
    /// <summary>
    /// Represents a class for making authenticated requests to vimeo. An authentication token is required for each user.
    /// Access tokens are generated via redirect.
    /// </summary>
    public class AuthenticatedViaRedirectVimeoClient : VimeoClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticatedViaRedirectVimeoClient"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="configuration">The configuration.</param>
        public AuthenticatedViaRedirectVimeoClient(IRequestFactory factory, IClientConfiguration configuration)
            : base(factory, configuration)
        {
        }
        
        protected override UserInfo ParseUserInfo(string content)
        {
            var response = JObject.Parse(content);
            var avatarUri_Small = response["pictures"][0]["link"].Value<string>();
            var avatarUri_Normal = response["pictures"][2]["link"].Value<string>();
            var avatarUri_Large = response["pictures"][3]["link"].Value<string>();
            return new UserInfo
            {
                Id = response["uri"].Value<string>(),
                FirstName = response["name"].Value<string>(),
                AvatarUri =
                {
                    Small = avatarUri_Small,
                    Normal = avatarUri_Normal,
                    Large = avatarUri_Large
                }
            };
        }

    }
}
