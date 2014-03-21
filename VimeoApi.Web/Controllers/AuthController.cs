using VimeoApi.OAuth2.Clients.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VimeoApi.Web.Filters;
using VimeoApi.Web.Models;
using OAuth2.Infrastructure;

namespace VimeoApi.Web.Controllers
{
    public class AuthController : BaseController
    {
        #region Constants

        private const string AUTH_TOKEN_KEY = "auth_token_key";
        private const string AUTH_SCOPE_KEY = "auth_scope_key";
        private const string AUTH_TOKEN_TYPE_KEY = "auth_token_type_key";

        #endregion

        //
        // GET: /Auth/

        public ActionResult Index()
        {
            var returnUrl = Request.QueryString["return"];

            if (CurrentVimeoClient is AuthenticatedViaRedirectVimeoClient)
            {
                if (Request.QueryString["code"] != null)
                {
                    //Redirected from vimeo? initialize and set cookies

                    CurrentVimeoClient.GetUserInfo(Request.QueryString);
                    SetCookie(AUTH_TOKEN_KEY, CurrentVimeoClient.AccessToken);
                    SetCookie(AUTH_SCOPE_KEY, CurrentVimeoClient.Scope);
                    SetCookie(AUTH_TOKEN_TYPE_KEY, CurrentVimeoClient.TokenType);

                    returnUrl = CurrentVimeoClient.State;
                }
                else
                {
                    //Session expired? redirected from VimeoAuthAttribute
                    if (GetCookie(AUTH_TOKEN_KEY) == null)
                    {
                        // Do not have an auth cookie? go to vimeo
                        return Redirect(CurrentVimeoClient.GetLoginLinkUri(returnUrl));
                    }
                    else
                    {
                        // We have an access token, we can proceed
                        CurrentVimeoClient.SetAccessToken(
                            GetCookie(AUTH_TOKEN_KEY),
                            GetCookie(AUTH_TOKEN_TYPE_KEY),
                            GetCookie(AUTH_SCOPE_KEY));
                    }
                }

            }

            if (string.IsNullOrEmpty(returnUrl))
                return RedirectToAction("Index", "Home");
            else
                return Redirect(returnUrl);
        }

        public ActionResult Clear()
        {
            Session.Abandon();
            DeleteCookie(AUTH_TOKEN_KEY);
            DeleteCookie(AUTH_TOKEN_TYPE_KEY);
            DeleteCookie(AUTH_SCOPE_KEY);
            return RedirectToAction("Index", "Home");
        }

        [VimeoAuthorize]
        public ActionResult Display()
        {
            var model = new AuthDisplayModel()
            {
                AccessToken = CurrentVimeoClient.AccessToken,
                TokenType = CurrentVimeoClient.TokenType,
                Scope = CurrentVimeoClient.Scope
            };
            return View(model);
        }


        #region Helpers

        private void SetCookie(string name, string value)
        {
            Response.Cookies.Set(new HttpCookie(name)
            {
                Value = value,
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(1)
            });
        }

        private string GetCookie(string name)
        {
            var cookie = Request.Cookies.Get(name);
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                return cookie.Value;
            }
            return null;
        }

        private void DeleteCookie(string name)
        {
            Response.Cookies.Set(new HttpCookie(name)
            {
                Expires = DateTime.Now.AddDays(-1)
            });
        }

        #endregion

    }
}
