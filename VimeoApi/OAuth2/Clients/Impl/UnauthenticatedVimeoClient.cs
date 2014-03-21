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
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoApi.OAuth2.Clients.Impl
{
    /// <summary>
    /// Represents a class for making unauthenticated requests to vimeo.
    /// An unauthenticated access token is requested using ClientID and ClientSecret.
    /// </summary>
    public class UnauthenticatedVimeoClient : VimeoClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnauthenticatedVimeoClient"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="configuration">The configuration.</param>
        public UnauthenticatedVimeoClient(IRequestFactory factory, IClientConfiguration configuration)
            : base(factory, configuration)
        {
        }


        /// <summary>
        /// Unauthenticated requests follow the OAuth 2.0 Client Credentials Grant
        /// You must request an unauthenticated access token to make unauthenticated requests. You can not use your app credentials.
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        protected override Endpoint AccessTokenServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = base.BaseUrl,
                    Resource = "/oauth/authorize/client"
                };
            }
        }

        protected override void BeforeGetAccessToken(BeforeAfterRequestArgs args)
        {
            //Adding Auth data to header
            string basicAuthData = string.Format("{0}:{1}", Configuration.ClientId, Configuration.ClientSecret);
            string encodedBasicAuthData = Convert.ToBase64String(Encoding.ASCII.GetBytes(basicAuthData));
            args.Client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(encodedBasicAuthData, "basic");

            args.Request.AddObject(new
            {
                grant_type = "client_credentials",
                scope = Configuration.Scope,
            });
        }
      
    }
}
