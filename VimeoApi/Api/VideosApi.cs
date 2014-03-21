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

using VimeoApi.OAuth2.Clients;
using Newtonsoft.Json.Linq;
using OAuth2.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuth2.Infrastructure;
using VimeoApi.OAuth2.Clients.Impl;
using VimeoApi.Models;
using VimeoApi.OAuth2;
using System.Net;

namespace VimeoApi.Api
{
    public class VideosApi : VimeoApi
    {
        public VideosApi(VimeoClient client)
            : base(client)
        {
        }

        #region /videos

        protected virtual Endpoint VideosServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}"
                };
            }
        }

        /// <summary>
        /// Search for videos
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetVideos(DefaultParametersContext parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            if (parameters.query.IsEmpty())
            {
                throw new ArgumentException("You must provide a query.", "parameters.query");
            }

            return Execute(VideosServiceEndpoint,
                            new { clip_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Video>>();
        }

        /// <summary>
        /// Get info on an individual video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public Video GetVideo(string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }

            return Execute(VideosServiceEndpoint,
                            new { clip_id = clipId },
                            null,
                            Method.GET).ToObject<Video>();
        }

        /// <summary>
        /// Edit video metadata
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public void EditVideo(string clipId, VideoParametersContext parameters)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }

            Execute(VideosServiceEndpoint,
                            new { clip_id = clipId },
                            parameters,
                            Method.PATCH);
        }

        /// <summary>
        /// Delete a clip
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public void DeleteVideo(string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }

            Execute(VideosServiceEndpoint,
                            new { clip_id = clipId },
                            null,
                            Method.DELETE);
        }

        #endregion

        #region /videos/comments

        protected virtual Endpoint VideosCommentsServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}/comments/{comment_id}"
                };
            }
        }

        /// <summary>
        /// Get comments on this video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DefaultResultSet<Comment> GetVideoComments(string clipId, DefaultParametersContext parameters = null)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }

            return Execute(VideosCommentsServiceEndpoint,
                            new { clip_id = clipId, comment_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Comment>>();
        }

        /// <summary>
        /// Post a comment on the video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="text">The text for the new comment</param>
        public void PostCommentOnVideo(string clipId, string text)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }

            Execute(VideosCommentsServiceEndpoint,
                            new { clip_id = clipId, comment_id = string.Empty },
                            new { text = text },
                            Method.POST);
        }

        /// <summary>
        /// See if a comment exists on the clip
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="commentId">commentId</param>
        /// <returns></returns>
        public Comment GetCommentOnVideo(string clipId, string commentId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }
            if (string.IsNullOrEmpty(commentId))
            {
                throw new ArgumentException("Parameter is empty", "commentId");
            }
            return Execute(VideosCommentsServiceEndpoint,
                            new { clip_id = clipId, comment_id = commentId },
                            null,
                            Method.GET).ToObject<Comment>();
        }

        /// <summary>
        /// Edit an existing comment on a video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="commentId">commentId</param>
        /// <param name="text">The text for the new comment</param>
        /// <returns></returns>
        public void EditCommentOnVideo(string clipId, string commentId, string text)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }
            if (string.IsNullOrEmpty(commentId))
            {
                throw new ArgumentException("Parameter is empty", "commentId");
            }
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("text");
            }

            Execute(VideosCommentsServiceEndpoint,
                            new { clip_id = clipId, comment_id = commentId },
                            new { text = text },
                            Method.PATCH);
        }

        /// <summary>
        /// Delete a comment from a video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="commentId">commentId</param>
        /// <returns></returns>
        public void DeleteCommentFromVideo(string clipId, string commentId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }
            if (string.IsNullOrEmpty(commentId))
            {
                throw new ArgumentException("Parameter is empty", "commentId");
            }

            Execute(VideosCommentsServiceEndpoint,
                            new { clip_id = clipId, comment_id = commentId },
                            null,
                            Method.DELETE);
        }

        #endregion

        #region /videos/credits

        protected virtual Endpoint VideosCreditsServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}/credits/{credit_id}"
                };
            }
        }

        /// <summary>
        /// Get a list of users credited on a video.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Credit> GetVideoCredits(string clipId, DefaultParametersContext parameters)
        {
            return Execute(VideosCreditsServiceEndpoint,
                            new { clip_id = clipId, credit_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Credit>>();
        }

        /// <summary>
        /// Add a credit for the video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="role">The role of the person being credited</param>
        /// <param name="parameters">VideoCreditParameterContext</param>
        public void PostCreditForVideo(string clipId, VideoCreditParameterContext parameters)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }

            Execute(VideosCreditsServiceEndpoint,
                            new { clip_id = clipId, credit_id = string.Empty },
                            parameters,
                            Method.POST);
        }

        /// <summary>
        /// Retrieve individual credit information.
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="creditId">creditId</param>
        /// <returns></returns>
        public Credit GetVideoCredit(string clipId, string creditId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }
            if (string.IsNullOrEmpty(creditId))
            {
                throw new ArgumentException("Parameter is empty", "creditId");
            }

            return Execute(VideosCreditsServiceEndpoint,
                            new { clip_id = clipId, credit_id = creditId },
                            null,
                            Method.GET).ToObject<Credit>();
        }


        /// <summary>
        /// Allows the user who created the credit to modify credit information
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="creditId">creditId</param>
        /// <param name="role">The role of the person being credited</param>
        public void EditCreditOnVideo(string clipId, string creditId, string role)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }

            Execute(VideosCreditsServiceEndpoint,
                            new { clip_id = clipId, credit_id = creditId },
                            new { role = role },
                            Method.PATCH);
        }

        /// <summary>
        /// Allows a user to delete the credit
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="creditId">creditId</param>
        public void DeleteCreditFromVideo(string clipId, string creditId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("Parameter is empty", "clipId");
            }
            if (string.IsNullOrEmpty(creditId))
            {
                throw new ArgumentException("Parameter is empty", "creditId");
            }

            Execute(VideosCreditsServiceEndpoint,
                            new { clip_id = clipId, credit_id = creditId },
                            null,
                            Method.DELETE);
        }

        #endregion

        #region /videos/likes

        protected virtual Endpoint VideoLikesServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}/likes"
                };
            }
        }

        /// <summary>
        /// Get a list of the users who liked this video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetVideoLikes(string clipId, DefaultParametersContext parameters)
        {
            if (clipId == null)
            {
                throw new ArgumentNullException("clipId");
            }

            return Execute(VideoLikesServiceEndpoint,
                           new { clip_id = clipId },
                           parameters,
                           Method.GET).ToObject<DefaultResultSet<User>>();
        }

        #endregion

        #region /videos/presets

        protected virtual Endpoint VideoPresetsServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}/presets/{preset_id}"
                };
            }
        }


        /// <summary>
        /// Do the given embed settings exist on this clip
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="presetId">presetId</param>
        /// <returns></returns>
        public bool DoesPresetExistInVideo(string clipId, string presetId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(presetId))
            {
                throw new ArgumentException("You must provide a valid presetId", "presetId");
            }

            try
            {
                Execute(VideoPresetsServiceEndpoint,
                               new { clip_id = clipId, preset_id = presetId },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no presets found
                if (ex.Response != null && ex.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }

                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Apply the given embed settings to the clip.
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="presetId">presetId</param>
        public void ApplyPreset(string clipId, string presetId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(presetId))
            {
                throw new ArgumentException("You must provide a valid presetId", "presetId");
            }

            Execute(VideoPresetsServiceEndpoint,
                           new { clip_id = clipId, preset_id = presetId },
                           null,
                           Method.PUT);
        }

        /// <summary>
        /// Remove the given embed settings from the clip
        /// </summary>
        /// <param name="clipId"></param>
        /// <param name="presetId"></param>
        public void RemovePreset(string clipId, string presetId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(presetId))
            {
                throw new ArgumentException("You must provide a valid presetId", "presetId");
            }

            Execute(VideoPresetsServiceEndpoint,
                           new { clip_id = clipId, preset_id = presetId },
                           null,
                           Method.DELETE);
        }

        #endregion

        #region /videos/videos

        protected virtual Endpoint VideoVideosServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}/videos"
                };
            }
        }


        /// <summary>
        /// Get related videos
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetSimilarVideos(string clipId, DefaultParametersContext parameters)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            return Execute(VideoVideosServiceEndpoint,
                    new { clip_id = clipId },
                    parameters,
                    Method.GET).ToObject<DefaultResultSet<Video>>();
        }


        #endregion

        #region /videos/tags

        protected virtual Endpoint VideoTagsServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}/tags/{tag}"
                };
            }
        }

        /// <summary>
        /// List all of the tags on the video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public DefaultResultSet<VideoTag> GetTagsOnVideo(string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            return Execute(VideoTagsServiceEndpoint,
                            new { clip_id = clipId, tag = string.Empty },
                            null,
                            Method.GET).ToObject<DefaultResultSet<VideoTag>>();
        }

        /// <summary>
        /// Assign tags to the video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="tag">The name of the tag to apply</param>
        public void AssignTagToVideo(string clipId, string[] tag)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            if (tag == null || tag.Length == 0)
            {
                throw new ArgumentException("You must provide at least one tag", "tag");
            }

            //Content-Type for this request must be set to application/json
            //**
            //creating the request body
            List<object> body = new List<object>();
            foreach (string tagItem in tag)
            {
                body.Add(new { tag = tagItem });
            }
            //Execute the request
            Execute(VideoTagsServiceEndpoint,
                new { clip_id = clipId, tag = string.Empty },
                body,
                Method.PUT,
                DataFormat.Json);
        }

        /// <summary>
        /// Does the tag exist for this video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="tag">The name of the tag to apply</param>
        /// <returns></returns>
        public bool DoesTagExistForVideo(string clipId, string tag)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentException("You must provide a valid tag", "tag");
            }

            try
            {
                Execute(VideoTagsServiceEndpoint,
                               new { clip_id = clipId, tag = tag },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no presets found
                if (ex.Response != null && ex.Response.StatusCode == HttpStatusCode.NotFound)
                {
                    return false;
                }

                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Remove the tag from this video if it exists
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="tag">The name of the tag to apply</param>
        public void RemoveTagFromVideo(string clipId, string tag)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(tag))
            {
                throw new ArgumentException("You must provide a valid tag", "tag");
            }

            Execute(VideoTagsServiceEndpoint,
                            new { clip_id = clipId, tag = tag },
                            null,
                            Method.DELETE);
        }

        #endregion

        #region /videos/privacy/users

        protected virtual Endpoint VideoPrivacyUsersServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}/privacy/users/{user_id}"
                };
            }
        }

        /// <summary>
        /// Retrieve the users that are allowed to see this video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetAllowedUsers(string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            try
            {
                return Execute(VideoPrivacyUsersServiceEndpoint,
                                new { clip_id = clipId, user_id = string.Empty },
                                null,
                                Method.GET).ToObject<DefaultResultSet<User>>();
            }
            catch (UnexpectedResponseException ex)
            {
                if (ex.Response != null && ex.Response.StatusCode == HttpStatusCode.BadRequest)
                {
                    //If you are asking for a list where none applies.
                    return null;

                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Add a user to the allowed users list.
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="userId">userId</param>
        public void AllowedUser(string clipId, string userId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("You must provide a valid userId", "userId");
            }

            Execute(VideoPrivacyUsersServiceEndpoint,
                            new { clip_id = clipId, user_id = userId },
                            null,
                            Method.PUT);
        }

        /// <summary>
        /// Remove a user from the allowed users list
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="userId">userId</param>
        public void DisallowedUser(string clipId, string userId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("You must provide a valid userId", "userId");
            }

            Execute(VideoPrivacyUsersServiceEndpoint,
                            new { clip_id = clipId, user_id = userId },
                            null,
                            Method.DELETE);
        }

        #endregion

        #region /videos/privacy/domains

        protected virtual Endpoint VideoPrivacyDomainsServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}/privacy/domains/{domain}"
                };
            }
        }
        //TODO: catch exceptions that should not be thrown (ex. 400 if the domain does not exist on a video)
        /// <summary>
        /// Retrieve the domains that are allowed to embed this video
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public List<Domain> GetAllowedDomains(string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            try
            {
                return Execute(VideoPrivacyDomainsServiceEndpoint,
                                new { clip_id = clipId, domain = string.Empty },
                                null,
                                Method.GET).ToObject<List<Domain>>();
            }
            catch (UnexpectedResponseException ex)
            {
                if (ex.Response != null && ex.Response.StatusCode == HttpStatusCode.BadRequest)
                {
                    //If you are asking for a list where none applies.
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// If this clip has domain privacy enabled, this call will enable this video to be embedded on the provided domain.
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="domain">domain</param>
        public void EmbedVideoOnDomain(string clipId, string domain)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(domain))
            {
                throw new ArgumentException("You must provide a valid domain", "domain");
            }

            Execute(VideoPrivacyDomainsServiceEndpoint,
                            new { clip_id = clipId, domain = domain },
                            null,
                            Method.PUT);
        }

        /// <summary>
        /// Remove a user from the allowed domains list.
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <param name="domain">domain</param>
        public void RemoveUserFromAllowedDomains(string clipId, string domain)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(domain))
            {
                throw new ArgumentException("You must provide a valid domain", "domain");
            }

            Execute(VideoPrivacyDomainsServiceEndpoint,
                            new { clip_id = clipId, domain = domain },
                            null,
                            Method.DELETE);
        }


        #endregion

        #region /videos/related

        protected virtual Endpoint VideoRelatedServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/videos/{clip_id}/related"
                };
            }
        }

        /// <summary>
        /// Retrieve the videos related to this one
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetRelatedVideos(string clipId, string filter, string filterEmbeddable)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            return Execute(VideoRelatedServiceEndpoint,
                            new { clip_id = clipId },
                            new { filter = filter ?? string.Empty, filter_embeddable = filterEmbeddable ?? string.Empty },
                            Method.GET).ToObject<DefaultResultSet<Video>>();
        }

        #endregion
    }
}
