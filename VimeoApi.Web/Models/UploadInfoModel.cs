using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace VimeoApi.Web.Models
{
    public class UploadInfoModel
    {
        [DisplayName("Free Space")]
        public int FreeSpace { get; set; }

        [DisplayName("Max Space")]
        public int MaxSpace { get; set; }

        [DisplayName("Can you upload HD videos?")]
        public bool CanUploadHD { get; set; }

        [DisplayName("Can you upload SD videos?")]
        public bool CanUploadSD { get; set; }

        public string UploadLinkSecure { get; set; }

        public string CompleteUri { get; set; }
    }
}