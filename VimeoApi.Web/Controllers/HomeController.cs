using VimeoApi.OAuth2;
using VimeoApi.OAuth2.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OAuth2.Infrastructure;
using VimeoApi.OAuth2.Clients.Impl;
using VimeoApi.Api;
using VimeoApi.Models;

namespace VimeoApi.Web.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
            ViewBag.Message = "VimeoApi Library for .NET";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
