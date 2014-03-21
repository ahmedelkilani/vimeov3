using VimeoApi.Api;
using VimeoApi.Models;
using VimeoApi.OAuth2;
using VimeoApi.OAuth2.Clients.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VimeoApi.Web.Filters;
using VimeoApi.Web.Models;

namespace VimeoApi.Web.Controllers
{
    [VimeoAuthorize]
    public class UploadController : BaseController
    {
        UsersApi _usersApi;
        
        public UploadController(ExtendedAuthorizationRoot authorizationRoot)
        {
        }

        //
        // GET: /Upload/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SimpleUpload()
        {
            _usersApi = new UsersApi(CurrentVimeoClient);
            var model = new UploadInfoModel();
            //Check Users Quota
            var user = _usersApi.GetUser();
            model.FreeSpace = user.upload_quota.space.free;
            model.MaxSpace = user.upload_quota.space.max;
            model.CanUploadHD = user.upload_quota.quota.hd;
            model.CanUploadSD = user.upload_quota.quota.sd;
            //Generate an upload ticket
            var ticket = _usersApi.GenerateUploadTicket(new GenerateTicketParameterContext()
            {
                type = UploadTicketType.post,
                redirect_url = GetFullyQualifiedUrl("UploadSuccess", "Upload")
            });
            model.UploadLinkSecure = ticket.upload_link_secure;

            return View(model);
        }

        public ActionResult UploadSuccess()
        {
            return View(new UploadSuccessModel());
        }

        public ActionResult StreamingUpload()
        {
            _usersApi = new UsersApi(CurrentVimeoClient);
            var model = new UploadInfoModel();
            //Check Users Quota
            var user = _usersApi.GetUser();
            model.FreeSpace = user.upload_quota.space.free;
            model.MaxSpace = user.upload_quota.space.max;
            model.CanUploadHD = user.upload_quota.quota.hd;
            model.CanUploadSD = user.upload_quota.quota.sd;
            //Generate an upload ticket
            var ticket = _usersApi.GenerateUploadTicket(new GenerateTicketParameterContext()
            {
                type = UploadTicketType.streaming
            });
            model.UploadLinkSecure = ticket.upload_link_secure;
            model.CompleteUri = ticket.complete_uri;

            return View(model);
        }

        [HttpGet]
        public JsonResult VerifyUpload(string uri)
        {
            _usersApi = new UsersApi(CurrentVimeoClient);

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = _usersApi.VerifyUpload(uri)
            };
        }

        [HttpGet]
        public JsonResult CompleteUpload(string uri)
        {
            _usersApi = new UsersApi(CurrentVimeoClient);

            return new JsonResult()
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = _usersApi.CompleteUpload(uri)
            };
        }


        #region Helpers

        private string GetFullyQualifiedUrl(string actionName, string controllerName)
        {
            UrlHelper urlHelper = new UrlHelper(ControllerContext.RequestContext);
            return urlHelper.Action(actionName, controllerName, null, this.Request.Url.Scheme);
        }

        #endregion
    }
}
