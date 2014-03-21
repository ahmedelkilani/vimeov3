using VimeoApi.OAuth2;
using VimeoApi.OAuth2.Clients.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VimeoApi.Web.Filters;
using VimeoApi.Web.Helpers;

namespace VimeoApi.Web.Controllers
{
    public class BaseController : Controller
    {
        protected VimeoClient CurrentVimeoClient
        {
            get
            {
                return ControllerContext.HttpContext.GetVimeoClient();
            }
        }
                

    }
}
