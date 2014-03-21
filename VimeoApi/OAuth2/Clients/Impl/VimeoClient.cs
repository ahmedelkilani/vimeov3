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
    /// Vimeo OAuth2 Client.
    /// </summary>
    public abstract class VimeoClient : OAuth2Client
    {
        protected IRequestFactory _factory;
        private string _baseUrl = "https://api.vimeo.com";
        private string _apiVersion = "application/vnd.vimeo.*+json;version=3.0";
        protected const string TokenTypeKey = "token_type";
        protected const string ScopeKey = "scope";

        /// <summary>
        /// Initializes a new instance of the <see cref="VimeoClient"/> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="configuration">The configuration.</param>
        public VimeoClient(IRequestFactory factory, IClientConfiguration configuration)
            : base(factory, configuration)
        {
            _factory = factory;
        }

        #region Properties Overrides

        /// <summary>
        /// Friendly name of provider (OAuth2 service).
        /// </summary>
        public override string Name
        {
            get { return "VimeoClient"; }
        }

        /// <summary>
        /// Defines URI of service which issues access code.
        /// </summary>
        protected override Endpoint AccessCodeServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = _baseUrl,
                    Resource = "/oauth/authorize"
                };
            }
        }

        /// <summary>
        /// Defines URI of service which issues access token.
        /// </summary>
        protected override Endpoint AccessTokenServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = _baseUrl,
                    Resource = "/oauth/access_token"
                };
            }
        }

        /// <summary>
        /// Defines URI of service which allows to obtain information about user which is currently logged in.
        /// </summary>
        protected override Endpoint UserInfoServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = _baseUrl,
                    Resource = "/me"
                };
            }
        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// OAuth 2 Access Token Type.
        /// </summary>
        public virtual string TokenType { get; protected set; }

        /// <summary>
        /// Vimeo Api Supported Scope.
        /// </summary>
        public string Scope { get; protected set; }

        /// <summary>
        /// Vimeo Base Url.
        /// </summary>
        public string BaseUrl { get { return _baseUrl; } }

        /// <summary>
        /// Viemo Api Version Header
        /// </summary>
        public string ApiVersion
        {
            get { return _apiVersion; }
            set { _apiVersion = value; }
        }

        #endregion

        #region Access Token

        protected override void AfterGetAccessToken(BeforeAfterRequestArgs args)
        {
            base.AfterGetAccessToken(args);

            this.TokenType = (string)JObject.Parse(args.Response.Content).SelectToken(TokenTypeKey);
            if (this.TokenType.IsEmpty())
            {
                throw new UnexpectedResponseException(TokenTypeKey);
            }
            this.Scope = (string)JObject.Parse(args.Response.Content).SelectToken(ScopeKey);
            if (this.Scope.IsEmpty())
            {
                throw new UnexpectedResponseException(ScopeKey);
            }
        }

        #endregion

        #region User Info

        protected override void BeforeGetUserInfo(BeforeAfterRequestArgs args)
        {
            base.BeforeGetUserInfo(args);
            args.Client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(AccessToken, TokenType);
        }

        protected override UserInfo ParseUserInfo(string content)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// If you have stored the access token somewhere. Use this method to set it back to the client.
        /// </summary>
        /// <param name="token">OAuth2 Access Token</param>
        /// <param name="tokenType">OAuth2 Token Type</param>
        /// <param name="scope">Permission Scope</param>
        public void SetAccessToken(string token, string tokenType, string scope)
        {
            this.AccessToken = token;
            this.TokenType = tokenType;
            this.Scope = scope;
        }

        public virtual IRestClient CreateClient(Endpoint endpoint)
        {
            if (AccessToken.IsEmpty())
            {
                GetToken(new NameValueCollection() { { "error", "" } });
            }
            var client = _factory.CreateClient(endpoint);
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(AccessToken, TokenType);
            client.RemoveDefaultParameter("Accept");
            client.AddDefaultParameter("Accept", ApiVersion, ParameterType.HttpHeader);
            return client;
        }

        public virtual IRestRequest CreateRequest(Endpoint endpoint)
        {
            return CreateRequest(endpoint, Method.GET);
        }

        public virtual IRestRequest CreateRequest(Endpoint endpoint, Method method)
        {
            return _factory.CreateRequest(endpoint, method);
        }

        #endregion

    }
}
