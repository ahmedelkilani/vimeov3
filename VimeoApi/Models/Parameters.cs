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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoApi.Models
{
    #region Common
    /// <summary>
    /// Represents the default parameters context object.
    /// </summary>
    public class DefaultParametersContext
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int? page { get; set; }
        /// <summary>
        /// Page size
        /// </summary>
        public int? per_page { get; set; }
        /// <summary>
        /// Search query
        /// </summary>
        public string query { get; set; }
        /// <summary>
        /// Field to sort on
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// Sort direction
        /// </summary>
        public string direction { get; set; }
        /// <summary>
        /// Technique used to filter the results
        /// </summary>
        public string filter { get; set; }
    }

    public class VideoFilterableParametersContext : DefaultParametersContext
    {
        /// <summary>
        /// Should the videos be openly embeddable?
        /// </summary>
        public string filter_embeddable { get; set; }
    }

    #endregion

    #region Videos

    public class VideoParametersContext
    {
        /// <summary>
        /// string	The new name for the video
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// string	The new description for the video
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// string	The new privacy setting for the video
        /// </summary>
        public string privacy { get; set; }

        /// <summary>
        /// string	Enable or disable the review page
        /// </summary>
        public string review_link { get; set; }
    }

    public class VideoCreditParameterContext
    {
        /// <summary>
        /// The role of the person being credited
        /// </summary>
        public string role { get; set; }

        /// <summary>
        /// The name of the person being credited
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// The email address of the person being credited
        /// </summary>
        public string email { get; set; }
    }

    #endregion

    #region Users

    public class AlbumParameterContext
    {
        /// <summary>
        /// The new title for the Album
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// The new description for the Album
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// The new privacy level for the Album
        /// </summary>
        public AlbumPrivacy privacy { get; set; }

        /// <summary>
        /// The new password for the Album (Required only if privacy is set to 'password')
        /// </summary>
        public string password { get; set; }

        /// <summary>
        /// he new default sort for the Album
        /// </summary>
        public AlbumSortType? sort { get; set; }
    }

    public enum AlbumPrivacy
    {
        anybody,
        password
    }

    public enum AlbumSortType
    {
        arranged,
        newest,
        oldest,
        plays,
        comments,
        likes,
        added_first,
        added_last,
        alphabetical
    }

    public class FeedParameterContext
    {
        /// <summary>
        /// Page number
        /// </summary>
        public int? page { get; set; }

        /// <summary>
        /// Page size
        /// </summary>
        public int? per_page { get; set; }

        /// <summary>
        /// The offset amount
        /// </summary>
        public int? offset { get; set; }
    }

    public class GenerateTicketParameterContext
    {
        /// <summary>
        /// Upload ticket type
        /// </summary>
        public UploadTicketType type { get; set; }

        /// <summary>
        /// Once the upload is complete, the user will be redirected back to this URL
        /// </summary>
        public string redirect_url { get; set; }
    }

    public enum UploadTicketType
    {
        post,
        streaming
    }

    #endregion
}
