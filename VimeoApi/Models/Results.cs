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
    #region Result Sets

    public class DefaultResultSet<T>
    {
        public int? total { get; set; }
        public int? page { get; set; }
        public int? per_page { get; set; }
        public Paging paging { get; set; }
        public List<T> data { get; set; }
    }

    #endregion

    #region Result Data Models

    public class Privacy
    {
        public string view { get; set; }
        public string embed { get; set; }
        public bool download { get; set; }
        public bool add { get; set; }
        public string join { get; set; }
        public string videos { get; set; }
        public string comment { get; set; }
        public string forums { get; set; }
        public string invite { get; set; }
    }

    public class Picture
    {
        public string type { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string link { get; set; }
    }

    public class Stat
    {
        public int? plays { get; set; }
        public int likes { get; set; }
        public int comments { get; set; }
        public int videos { get; set; }
        public int users { get; set; }
        public int moderators { get; set; }
        public int topics { get; set; }
        public int albums { get; set; }
        public int channels { get; set; }
        public int groups { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public string canonical { get; set; }
    }

    public class VideoTag
    {
        public string uri { get; set; }
        public string tag { get; set; }
    }

    public class Connection
    {
        public string credits { get; set; }
        public string activities { get; set; }
        public string albums { get; set; }
        public string channels { get; set; }
        public string feed { get; set; }
        public string followers { get; set; }
        public string following { get; set; }
        public string groups { get; set; }
        public string likes { get; set; }
        public string portfolios { get; set; }
        public string videos { get; set; }
        public string watchlater { get; set; }
        public string shared { get; set; }
        public string users { get; set; }
    }

    public class Metadata
    {
        public Connection connections { get; set; }
        public Interaction interactions { get; set; }
    }

    public class Follow
    {
        public bool added { get; set; }
        public DateTime? added_time { get; set; }
        public string type { get; set; }
        public string uri { get; set; }
    }

    public class Logos
    {
        public bool vimeo { get; set; }
        public bool custom { get; set; }
        public bool sticky_custom { get; set; }
    }

    public class Join
    {
        public bool added { get; set; }
        public DateTime? added_time { get; set; }
        public string status { get; set; }
        public string title { get; set; }
        public string uri { get; set; }
    }

    public class Watchlater
    {
        public bool added { get; set; }
        public DateTime? added_time { get; set; }
        public string uri { get; set; }
    }

    public class Like
    {
        public bool added { get; set; }
        public DateTime? added_time { get; set; }
        public string uri { get; set; }
    }

    public class Interaction
    {
        public Follow follow { get; set; }
        public Join join { get; set; }
        public Watchlater watchlater { get; set; }
        public Like like { get; set; }
    }

    public class Button
    {
        public bool like { get; set; }
        public bool watchlater { get; set; }
        public bool share { get; set; }
        public bool embed { get; set; }
        public bool vote { get; set; }
        public bool hd { get; set; }
    }

    public class Logo
    {
        public bool vimeo { get; set; }
        public bool custom { get; set; }
        public bool sticky_custom { get; set; }
    }

    public class Setting
    {
        public Button buttons { get; set; }
        public Logo logos { get; set; }
        public string outro { get; set; }
        public string portrait { get; set; }
        public string title { get; set; }
        public string byline { get; set; }
        public bool badge { get; set; }
        public bool byline_badge { get; set; }
        public bool playbar { get; set; }
        public bool volume { get; set; }
        public bool fullscreen_button { get; set; }
        public bool scaling_button { get; set; }
        public bool autoplay { get; set; }
        public bool autopause { get; set; }
        public bool loop { get; set; }
        public string color { get; set; }
        public bool link { get; set; }
        public object color_original { get; set; }
        public string custom_logo_url { get; set; }
        public string custom_logo_link_url { get; set; }
        public bool? custom_logo_use_link { get; set; }
        public int? logo_width { get; set; }
        public int? logo_height { get; set; }
    }

    public class EmbedPresets
    {
        public string uri { get; set; }
        public string name { get; set; }
        public Setting settings { get; set; }
    }

    public class Resource
    {
        public string uri { get; set; }
        public string name { get; set; }
    }

    public class Context
    {
        public string action { get; set; }
        public string resource_type { get; set; }
        public Resource resource { get; set; }
    }

    public class User
    {
        public string uri { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string location { get; set; }
        public string bio { get; set; }
        public DateTime created_time { get; set; }
        public string account { get; set; }
        public string[] content_filter { get; set; }
        public List<Picture> pictures { get; set; }
        public List<Website> websites { get; set; }
        public Stat stats { get; set; }
        public Metadata metadata { get; set; }
        public UploadQuota upload_quota { get; set; }
    }

    public class UploadTicket
    {
        /// <summary>
        /// The API endpoint for your upload ticket. You can query this to learn more about the upload
        /// </summary>
        public string uri { get; set; }
        /// <summary>
        /// Your unique ticket id.
        /// </summary>
        public string ticket_id { get; set; }
        /// <summary>
        /// The video you upload to this upload ticket will be associated with this user
        /// </summary>
        public User user { get; set; }

        /// <summary>
        /// An upload URL
        /// </summary>
        public string upload_link { get; set; }
        /// <summary>
        /// A secure upload URL
        /// </summary>
        public string upload_link_secure { get; set; }

        /// <summary>
        /// The URI you use when finishing the upload. You will make an HTTP DELETE request to this endpoint
        /// </summary>
        public string complete_uri { get; set; }

        /// <summary>
        /// An HTML upload form
        /// </summary>
        public string form { get; set; }
    }

    public class Space
    {
        /// <summary>
        /// The amount of bytes left in your quota for the current period
        /// </summary>
        public int free { get; set; }
        /// <summary>
        /// Your maximum quota for the current period
        /// </summary>
        public int max { get; set; }
        /// <summary>
        /// The amount of bytes you have uploaded this period
        /// </summary>
        public int used { get; set; }

    }

    public class Quota
    {
        /// <summary>
        /// Boolean indicating whether this user can upload an HD video
        /// </summary>
        public bool hd { get; set; }

        /// <summary>
        /// Boolean indicating whether this user can upload an SD video
        /// </summary>
        public bool sd { get; set; }
    }

    public class UploadQuota
    {
        public Space space { get; set; }
        public Quota quota { get; set; }
    }

    public class Website
    {
        public string name { get; set; }
        public string link { get; set; }
        public string description { get; set; }
    }

    public class Video
    {
        public string uri { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public int duration { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public DateTime created_time { get; set; }
        public DateTime modified_time { get; set; }
        public string[] content_rating { get; set; }
        public string license { get; set; }
        public Privacy privacy { get; set; }
        public List<Picture> pictures { get; set; }
        public List<Tag> tags { get; set; }
        public Stat stats { get; set; }
        public Metadata metadata { get; set; }
        public EmbedPresets embed_presets { get; set; }
        public User user { get; set; }
        public string status { get; set; }
    }

    public class Paging
    {
        public string next { get; set; }
        public string previous { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Comment
    {
        public string uri { get; set; }
        public string type { get; set; }
        public string text { get; set; }
        public DateTime created_on { get; set; }
        public User user { get; set; }
    }

    public class Credit
    {
        public string uri { get; set; }
        public Video video { get; set; }
        public string role { get; set; }
        public string name { get; set; }
        public User user { get; set; }
    }

    public class Album
    {
        public string uri { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public int duration { get; set; }
        public string created_time { get; set; }
        public string modified_time { get; set; }
        public User user { get; set; }
        public Privacy privacy { get; set; }
        public List<Picture> pictures { get; set; }
        public Stat stats { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Channel
    {
        public string uri { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public DateTime created_time { get; set; }
        public DateTime modified_time { get; set; }
        public User user { get; set; }
        public List<Picture> pictures { get; set; }
        public Privacy privacy { get; set; }
        public Stat stats { get; set; }
        public Metadata metadata { get; set; }
    }

    public class Group
    {
        public string uri { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public DateTime created_time { get; set; }
        public DateTime modified_time { get; set; }
        public Privacy privacy { get; set; }
        public List<Picture> pictures { get; set; }
        public Stat stats { get; set; }
        public Metadata metadata { get; set; }
        public User user { get; set; }
    }

    public enum AddingVideoToGroupResult
    {
        /// <summary>
        /// The video was successfully added to the Group
        /// </summary>
        Added,
        /// <summary>
        /// The video is pending addition to the Group
        /// </summary>
        Pending,
        /// <summary>
        /// The video is already in the Group
        /// </summary>
        AleadyExists
    }

    public class Feed
    {
        public string uri { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string link { get; set; }
        public int duration { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public DateTime created_time { get; set; }
        public DateTime modified_time { get; set; }
        public string content_rating { get; set; }
        public string license { get; set; }
        public Privacy privacy { get; set; }
        public List<Picture> pictures { get; set; }
        public List<Tag> tags { get; set; }
        public Stat stats { get; set; }
        public Metadata metadata { get; set; }
        public EmbedPresets embed_presets { get; set; }
        public User user { get; set; }
        public Context context { get; set; }
        public string status { get; set; }
        public string review_link { get; set; }
    }

    public class Preset
    {
        public string uri { get; set; }
        public string name { get; set; }
        public Setting settings { get; set; }
        public User user { get; set; }
    }

    public class Domain
    {
        public string domain { get; set; }
        public string uri { get; set; }
    }

    public class Category
    {
        public string uri { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public bool top_level { get; set; }
        public List<Picture> pictures { get; set; }
        public Stat stats { get; set; }
        public List<Category> subcategories { get; set; }
        public Metadata metadata { get; set; }
    }

    #endregion
}
