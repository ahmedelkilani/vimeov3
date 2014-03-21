using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VimeoApi.Web.Models
{
    public class UploadSuccessModel
    {
        public string VideoUrl
        {
            get
            {
                return "https://vimeo.com" + GetVideoId();
            }
        }

        private string GetVideoId()
        {
            var videlUrl = HttpContext.Current.Request.QueryString["video_uri"];
            if (string.IsNullOrWhiteSpace(videlUrl))
                return string.Empty;
            
            return videlUrl.Substring(videlUrl.LastIndexOf('/'));
        }
    }
}