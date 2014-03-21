using VimeoApi.OAuth2;
using VimeoApi.OAuth2.Clients.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VimeoApi.Web.Helpers
{
    public static class HttpContextExtensions
    {
        private const string VIMEO_CLIENT_KEY = "vimeo_client_key";

        public static VimeoClient GetVimeoClient(this HttpContextBase context)
        {
            if (context.Session[VIMEO_CLIENT_KEY] == null)
            {
                var client = AuthorizationRoot.Clients.FirstOrDefault(p => p.Name == "VimeoClient") as VimeoClient;
                context.Session[VIMEO_CLIENT_KEY] = client;
            }
            return context.Session[VIMEO_CLIENT_KEY] as VimeoClient;
        }

        private static ExtendedAuthorizationRoot AuthorizationRoot
        {
            get
            {
                return DependencyResolver.Current.GetService<ExtendedAuthorizationRoot>();
            }
        }
    }
}