using VimeoApi.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VimeoApi.Web.Helpers;
using OAuth2.Infrastructure;
using VimeoApi.OAuth2.Clients.Impl;
using System.Globalization;

namespace VimeoApi.Web.Filters
{
    /// <summary>
    ///  Represents an attribute that is used on a controller or action that requires authorization by vimeo
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class VimeoAuthorizeAttribute : ActionFilterAttribute, IActionFilter, IResultFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.HttpContext;
            var authClient = context.GetVimeoClient();

            if (authClient is AuthenticatedViaRedirectVimeoClient)
            {
                if (authClient.AccessToken.IsEmpty())
                {
                    // Build query string
                    var query = HttpUtility.ParseQueryString(context.Request.QueryString.ToString());
                    query["return"] = context.Request.RawUrl;

                    context.Response.Redirect("/auth?" + query, true);
                }
            }
            else if (authClient is UnauthenticatedVimeoClient)
            {
                if (authClient.AccessToken.IsEmpty())
                {
                    authClient.GetToken(new System.Collections.Specialized.NameValueCollection() { { "error", "" } });
                }
            }
        }

    }
}