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
using VimeoApi.OAuth2.Clients.Impl;
using OAuth2.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimeoApi.Models;
using OAuth2.Infrastructure;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;

namespace VimeoApi.Api
{
    public class GroupsApi : VimeoApi
    {
        public GroupsApi(VimeoClient client)
            : base(client)
        {
        }

        #region /groups

        protected virtual Endpoint GroupsServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/groups/{group_id}"
                };
            }
        }

        /// <summary>
        /// Get a list of all Groups
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Group> GetGroups(DefaultParametersContext parameters)
        {
            return Execute(GroupsServiceEndpoint,
                            new { group_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Group>>();
        }

        /// <summary>
        /// Create a new Group
        /// </summary>
        /// <param name="name">The name of the new Group</param>
        /// <param name="description">The description of the new Group</param>
        /// <returns></returns>
        public void CreateGroup(string name, string description)
        {
            if (name.IsEmpty())
            {
                throw new ArgumentException("You must provide parameter name", "name");
            }

            Execute(GroupsServiceEndpoint,
                            new { group_id = string.Empty },
                            new { name = name, description = description },
                            Method.POST);
        }

        /// <summary>
        /// Get an individual Group
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <returns></returns>
        public Group GetGroup(string groupId)
        {
            if (groupId.IsEmpty())
            {
                throw new ArgumentException("You must provide groupId", "groupId");
            }

            return Execute(GroupsServiceEndpoint,
                            new { group_id = groupId },
                            null,
                            Method.GET).ToObject<Group>();
        }

        /// <summary>
        /// Edit an individual Group
        /// </summary>
        /// <param name="groupId">Id of the group to edit</param>
        /// <param name="creatorUri">URI of the new creator</param>
        /// <param name="name">New name for the Group</param>
        /// <param name="description">New description for the Group</param>
        public void EditGroup(string groupId, string creatorUri, string name, string description)
        {
            if (groupId.IsEmpty())
            {
                throw new ArgumentException("You must provide groupId", "groupId");
            }
            if (name.IsEmpty())
            {
                throw new ArgumentException("You must provide parameter name", "name");
            }

            Execute(GroupsServiceEndpoint,
                            new { group_id = groupId },
                            new { creator_uri = creatorUri, name = name, description = description },
                            Method.PATCH);
        }

        #endregion

        #region /groups/videos

        protected virtual Endpoint GroupVideosServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/groups/{group_id}/videos/{clip_id}"
                };
            }
        }

        /// <summary>
        /// Get a list of videos in a Group
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetVideosInGroup(string groupId, VideoFilterableParametersContext parameters)
        {
            if (groupId.IsEmpty())
            {
                throw new ArgumentException("You must provide groupId", "groupId");
            }

            return Execute(GroupVideosServiceEndpoint,
                            new { group_id = groupId, clip_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Video>>();

        }

        /// <summary>
        /// Get a video in a group
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public Video GetVideoFromGroup(string groupId, string clipId)
        {
            if (groupId.IsEmpty())
            {
                throw new ArgumentException("You must provide groupId", "groupId");
            }
            if (clipId.IsEmpty())
            {
                throw new ArgumentException("You must provide clipId", "clipId");
            }

            return Execute(GroupVideosServiceEndpoint,
                    new { group_id = groupId, clip_id = clipId },
                    null,
                    Method.GET).ToObject<Video>();

        }

        /// <summary>
        /// Add a video to a Group
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public AddingVideoToGroupResult AddVideoToGroup(string groupId, string clipId)
        {
            if (groupId.IsEmpty())
            {
                throw new ArgumentException("You must provide groupId", "groupId");
            }
            if (clipId.IsEmpty())
            {
                throw new ArgumentException("You must provide clipId", "clipId");
            }

            var client = Client.CreateClient(GroupVideosServiceEndpoint);
            var request = Client.CreateRequest(GroupVideosServiceEndpoint, Method.PUT);

            request.AddObject(new { group_id = groupId, clip_id = clipId }, ParameterType.UrlSegment);

            try
            {
                var response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.Accepted)
                {
                    return AddingVideoToGroupResult.Pending;
                }
                else if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return AddingVideoToGroupResult.Added;
                }

                throw new UnexpectedResponseException(response);
            }
            catch (UnexpectedResponseException ex)
            {
                if (ex.Response != null && ex.Response.StatusCode == HttpStatusCode.Forbidden)
                {
                    return AddingVideoToGroupResult.AleadyExists;
                }

                throw;
            }

        }

        /// <summary>
        /// Remove a video from a Group
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="clipId">clipId</param>
        public void RemoveVideoFromGroup(string groupId, string clipId)
        {
            if (groupId.IsEmpty())
            {
                throw new ArgumentException("You must provide groupId", "groupId");
            }
            if (clipId.IsEmpty())
            {
                throw new ArgumentException("You must provide clipId", "clipId");
            }

            Execute(GroupVideosServiceEndpoint,
                    new { group_id = groupId, clip_id = clipId },
                    null,
                    Method.DELETE);
        }

        #endregion

        #region /groups/users

        protected virtual Endpoint GroupUsersServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/groups/{group_id}/users"
                };
            }
        }

        /// <summary>
        /// Get a list of users that joined a Group
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetUsersInGroup(string groupId, DefaultParametersContext parameters)
        {
            if (groupId.IsEmpty())
            {
                throw new ArgumentException("You must provide groupId", "groupId");
            }

            return Execute(GroupUsersServiceEndpoint,
                            new { group_id = groupId },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<User>>();
        }

        #endregion
    }
}
