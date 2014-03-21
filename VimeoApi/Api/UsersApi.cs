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

using VimeoApi.Models;
using VimeoApi.OAuth2.Clients;
using VimeoApi.OAuth2.Clients.Impl;
using OAuth2.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuth2.Infrastructure;
using RestSharp;
using System.Net;
using Newtonsoft.Json.Linq;

namespace VimeoApi.Api
{
    public class UsersApi : VimeoApi
    {
        public UsersApi(VimeoClient client)
            : base(client)
        {
        }

        #region /users

        /// <summary>
        /// Gets Users/Current User Endpoint
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected virtual Endpoint UsersServiceEndpoint(bool currentUser)
        {
            return new Endpoint
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me" : "/users/{user_id}"
            };
        }

        /// <summary>
        /// Search for users
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetUsers(DefaultParametersContext parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }
            if (parameters.query.IsEmpty())
            {
                throw new ArgumentException("You must provide a query.", "parameters.query");
            }

            return Execute(UsersServiceEndpoint(false),
                            new { user_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<User>>();
        }

        /// <summary>
        /// Get an individual user
        /// </summary>
        /// <returns></returns>
        public User GetUser(string userId)
        {
            return Execute(UsersServiceEndpoint(userId == null),
                            new { user_id = userId },
                            null,
                            Method.GET).ToObject<User>();
        }

        /// <summary>
        /// Get the current user
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            return GetUser(null);
        }

        #endregion

        #region /users/albums

        /// <summary>
        /// Gets Users/Current User's Albums Endpoint
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected virtual Endpoint UsersAlbumsServiceEndpoint(bool currentUser)
        {
            return new Endpoint
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/albums" : "/users/{user_id}/albums"
            };
        }

        /// <summary>
        /// Gets Albums Endpoint
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        protected virtual Endpoint AlbumsServiceEndpoint()
        {
            return new Endpoint
            {
                BaseUri = Client.BaseUrl,
                Resource = "/albums/{album_id}"
            };
        }

        /// <summary>
        /// Get a list of a user's Albums
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Album> GetAlbums(string userId, DefaultParametersContext parameters)
        {
            return Execute(UsersAlbumsServiceEndpoint(userId == null),
                            new { user_id = userId },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Album>>();
        }

        /// <summary>
        /// Get a list of a current user's Albums
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Album> GetAlbums(DefaultParametersContext parameters)
        {
            return GetAlbums(null, parameters);
        }

        /// <summary>
        /// Get info on an Album
        /// </summary>
        /// <param name="albumId">albumId</param>
        /// <returns></returns>
        public Album GetAlbum(string albumId)
        {
            if (string.IsNullOrEmpty(albumId))
            {
                throw new ArgumentException("You must provide a valid albumId", "albumId");
            }

            return Execute(AlbumsServiceEndpoint(),
                            new { album_id = albumId },
                            null,
                            Method.GET).ToObject<Album>();
        }

        /// <summary>
        /// Edit an Album
        /// </summary>
        /// <param name="albumId">albumId</param>
        /// <returns></returns>
        public void EditAlbum(string albumId, AlbumParameterContext parameters)
        {
            if (string.IsNullOrEmpty(albumId))
            {
                throw new ArgumentException("You must provide a valid albumId", "albumId");
            }
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            Execute(AlbumsServiceEndpoint(),
                            new { album_id = albumId },
                            parameters,
                            Method.PATCH);
        }


        #endregion

        #region /users/appearances

        /// <summary>
        /// Gets Users/Current User's Appearances Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint AppearancesServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/appearances" : "/users/{user_id}/appearances"
            };
        }

        /// <summary>
        /// Get a list of videos that a user appears in.
        /// <param name="userId">userId</param>
        /// <param name="parameters">parameters</param>
        /// </summary>
        public DefaultResultSet<Video> GetUserAppearances(string userId, VideoFilterableParametersContext parameters)
        {
            return Execute(AppearancesServiceEndpoint(userId == null),
                           new { user_id = userId },
                           parameters,
                           Method.GET).ToObject<DefaultResultSet<Video>>();
        }

        /// <summary>
        /// Get a list of videos that the current user appears in.
        /// <param name="parameters">parameters</param>
        /// </summary>
        public DefaultResultSet<Video> GetUserAppearances(VideoFilterableParametersContext parameters)
        {
            return GetUserAppearances(null, parameters);
        }



        #endregion

        #region /users/channels

        /// <summary>
        /// Gets Users/Current User's Channels Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint UserChannelsServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/channels/{channel_id}" : "/users/{user_id}/channels/{channel_id}"
            };
        }

        /// <summary>
        /// Get a list of the Channels a user follows.
        /// <param name="parameters">parameters</param>
        /// </summary>
        public DefaultResultSet<Channel> GetUserChannels(string userId, DefaultParametersContext parameters)
        {
            return Execute(UserChannelsServiceEndpoint(userId == null),
                           new { user_id = userId, channel_id = string.Empty },
                           parameters,
                           Method.GET).ToObject<DefaultResultSet<Channel>>();
        }

        /// <summary>
        /// Get a list of the Channels the current user follows.
        /// <param name="parameters">parameters</param>
        /// </summary>
        public DefaultResultSet<Channel> GetUserChannels(DefaultParametersContext parameters)
        {
            return GetUserChannels(null, parameters);
        }

        /// <summary>
        /// Does the authenticated user subscribe to this Channel?
        /// <param name="userId">userId</param>
        /// <param name="channelId">channelId</param>
        /// </summary>
        public bool IsUserSubscribedToChannel(string userId, string channelId)
        {
            if (string.IsNullOrEmpty(channelId))
            {
                throw new ArgumentException("You must provide a valid channelId", "channelId");
            }

            try
            {
                Execute(UserChannelsServiceEndpoint(userId == null),
                               new { user_id = userId, channel_id = channelId },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no subscriptions found
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
        /// Does the current authenticated user subscribe to this Channel?
        /// </summary>
        /// <param name="channelId">channelId</param>
        /// <returns></returns>
        public bool IsUserSubscribedToChannel(string channelId)
        {
            return IsUserSubscribedToChannel(null, channelId);
        }


        #endregion

        #region /users/groups

        /// <summary>
        /// Gets Users/Current User's Groups Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint UserGroupsServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/groups/{group_id}" : "/users/{user_id}/groups/{group_id}"
            };
        }


        /// <summary>
        /// Get a list of the Groups a user has joined
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="parameters">parameters</param>
        /// /// <returns></returns>
        public DefaultResultSet<Group> GetUserGroups(string userId, DefaultParametersContext parameters)
        {
            return Execute(UserGroupsServiceEndpoint(userId == null),
                           new { user_id = userId, group_id = string.Empty },
                           parameters,
                           Method.GET).ToObject<DefaultResultSet<Group>>();
        }

        /// <summary>
        /// Get a list of the Groups the current user has joined
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// /// <returns></returns>
        public DefaultResultSet<Group> GetUserGroups(DefaultParametersContext parameters)
        {
            return GetUserGroups(null, parameters);
        }

        /// <summary>
        /// Does the authenticated user belong to this Group?
        /// <param name="userId">userId</param>
        /// <param name="groupId">groupId</param>
        /// </summary>
        public bool IsUserJoinedGroup(string userId, string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new ArgumentException("You must provide a valid groupId", "groupId");
            }

            try
            {
                Execute(UserGroupsServiceEndpoint(userId == null),
                               new { user_id = userId, group_id = groupId },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no users found
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
        /// Does the current authenticated user belong to this Group?
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <returns></returns>
        public bool IsUserJoinedGroup(string groupId)
        {
            return IsUserJoinedGroup(null, groupId);
        }

        /// <summary>
        /// Remove user from the Group
        /// <param name="userId">userId</param>
        /// <param name="groupId">groupId</param>
        /// </summary>
        public void RemoveUserFromGroup(string userId, string groupId)
        {
            if (string.IsNullOrEmpty(groupId))
            {
                throw new ArgumentException("You must provide a valid groupId", "groupId");
            }

            Execute(UserGroupsServiceEndpoint(userId == null),
                               new { user_id = userId, group_id = groupId },
                               null,
                               Method.DELETE);
        }

        /// <summary>
        /// Remove the current user from the Group
        /// <param name="userId">userId</param>
        /// <param name="groupId">groupId</param>
        /// </summary>
        public void RemoveUserFromGroup(string groupId)
        {
            RemoveUserFromGroup(null, groupId);
        }


        #endregion

        #region /users/feed

        /// <summary>
        /// Gets Current User's Feed Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint UserFeedsServiceEndpoint()
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = "/me/feed"
            };
        }

        /// <summary>
        /// Get a list of the videos in your feed.
        /// </summary>
        /// <returns></returns>
        public DefaultResultSet<Feed> GetUserFeed(FeedParameterContext parameters)
        {
            return Execute(UserFeedsServiceEndpoint(),
                         null,
                         parameters,
                         Method.GET).ToObject<DefaultResultSet<Feed>>();
        }

        #endregion

        #region /users/followers

        /// <summary>
        /// Gets User/Current User's followers Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint UserFollowersServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/followers" : "/users/{user_id}/followers"
            };
        }

        /// <summary>
        /// Get a list of the users following a user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetUserFollowers(string userId, DefaultParametersContext parameters)
        {
            return Execute(UserFollowersServiceEndpoint(userId == null),
                         new { user_id = userId },
                         parameters,
                         Method.GET).ToObject<DefaultResultSet<User>>();
        }

        /// <summary>
        /// Get a list of the users following the current user
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetUserFollowers(DefaultParametersContext parameters)
        {
            return GetUserFollowers(null, parameters);
        }

        #endregion

        #region /users/following

        /// <summary>
        /// Gets User/Current User's following Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint UserFollowingServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/following/{follow_user_id}" : "/users/{user_id}/following/{follow_user_id}"
            };
        }

        /// <summary>
        /// Get a list of the users that a user is following
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetUserFollowingList(string userId, DefaultParametersContext parameters)
        {
            return Execute(UserFollowingServiceEndpoint(userId == null),
                         new { user_id = userId, follow_user_id = string.Empty },
                         parameters,
                         Method.GET).ToObject<DefaultResultSet<User>>();
        }

        /// <summary>
        /// Get a list of the users that the current user is following
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetUserFollowingList(DefaultParametersContext parameters)
        {
            return GetUserFollowingList(null, parameters);
        }


        /// <summary>
        /// Does this user follow another user?
        /// <param name="userId">userId</param>
        /// <param name="groupId">groupId</param>
        /// </summary>
        public bool IsUserFollowingAnotherUser(string userId, string followUserId)
        {
            if (string.IsNullOrEmpty(followUserId))
            {
                throw new ArgumentException("You must provide a valid followUserId", "followUserId");
            }

            try
            {
                Execute(UserFollowingServiceEndpoint(userId == null),
                               new { user_id = userId, follow_user_id = followUserId },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no users found
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
        /// Does this user follow another user?
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <returns></returns>
        public bool IsUserFollowingAnotherUser(string followUserId)
        {
            return IsUserFollowingAnotherUser(null, followUserId);
        }

        /// <summary>
        /// Unfollow another user
        /// <param name="userId">userId</param>
        /// <param name="followUserId">followUserId</param>
        /// </summary>
        public void UnfollowUser(string userId, string followUserId)
        {
            if (string.IsNullOrEmpty(followUserId))
            {
                throw new ArgumentException("You must provide a valid followUserId", "followUserId");
            }

            Execute(UserFollowingServiceEndpoint(userId == null),
                               new { user_id = userId, follow_user_id = followUserId },
                               null,
                               Method.DELETE);
        }

        /// <summary>
        /// Unfollow another user
        /// <param name="followUserId">followUserId</param>
        /// </summary>
        public void UnfollowUser(string followUserId)
        {
            UnfollowUser(null, followUserId);
        }

        #endregion

        #region /users/likes

        /// <summary>
        /// Gets User/Current User's likes Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint UserLikesServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/likes/{clip_id}" : "/users/{user_id}/likes/{clip_id}"
            };
        }

        /// <summary>
        /// Get a list of videos that a user likes
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetUserLikes(string userId, VideoFilterableParametersContext parameters)
        {
            return Execute(UserLikesServiceEndpoint(userId == null),
                            new { user_id = userId, clip_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Video>>();
        }

        /// <summary>
        /// Get a list of videos that the current user likes
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetUserLikes(VideoFilterableParametersContext parameters)
        {
            return GetUserLikes(null, parameters);
        }

        /// <summary>
        /// Does user like video
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public bool DoesUserLikeVideo(string userId, string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            try
            {
                Execute(UserLikesServiceEndpoint(userId == null),
                               new { user_id = userId, clip_id = clipId },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no users found
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
        /// Does the current user like video
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public bool DoesUserLikeVideo(string clipId)
        {
            return DoesUserLikeVideo(null, clipId);
        }

        #endregion

        #region /users/presets

        /// <summary>
        /// Gets User/Current User's presets Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint UserPresetsServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/presets/{preset_id}" : "/users/{user_id}/presets/{preset_id}"
            };
        }

        /// <summary>
        /// Get all presets created by the authenticated user
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Preset> GetUserPresets(string userId, DefaultParametersContext parameters)
        {
            return Execute(UserPresetsServiceEndpoint(userId == null),
                            new { user_id = userId, preset_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Preset>>();
        }

        /// <summary>
        /// Get all presets created by the current authenticated user
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Preset> GetUserPresets(DefaultParametersContext parameters)
        {
            return GetUserPresets(null, parameters);
        }

        /// <summary>
        /// Get a specific preset
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="presetId">presetId</param>
        /// <returns></returns>
        public Preset GetPreset(string userId, string presetId)
        {
            if (string.IsNullOrEmpty(presetId))
            {
                throw new ArgumentException("You must provide parameter presetId", "presetId");
            }

            return Execute(UserPresetsServiceEndpoint(userId == null),
                           new { user_id = userId, preset_id = presetId },
                           null,
                           Method.GET).ToObject<Preset>();

        }

        /// <summary>
        /// Get a specific preset
        /// </summary>
        /// <param name="presetId">presetId</param>
        /// <returns></returns>
        public Preset GetPreset(string presetId)
        {
            return GetPreset(null, presetId);
        }

        #endregion

        #region /users/videos

        /// <summary>
        /// Gets User/Current User's videos Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint UserVideosServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/videos/{clip_id}" : "/users/{user_id}/videos/{clip_id}"
            };
        }

        /// <summary>
        /// Does the authenticated user own the clip?
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public bool DoesUserOwnVideo(string userId, string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            try
            {
                Execute(UserVideosServiceEndpoint(userId == null),
                               new { user_id = userId, clip_id = clipId },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no video found
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
        /// Does the current authenticated user own the clip?
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public bool DoesUserOwnVideo(string clipId)
        {
            return DoesUserOwnVideo(null, clipId);
        }

        //TODO: NOT TESTED: error":"An unknown error has occured. Please let us know!"
        /// <summary>
        /// Get an upload ticket to replace this video file
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public UploadTicket GetUploadTicket(string userId, string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide parameter clipId", "clipId");
            }

            return Execute(UserVideosServiceEndpoint(userId == null),
                           new { user_id = userId, clip_id = clipId },
                           null,
                           Method.POST).ToObject<UploadTicket>();
        }

        /// <summary>
        /// Get an upload ticket to replace this video file
        /// </summary>
        /// <param name="userId">userId/param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public UploadTicket GetUploadTicket(string clipId)
        {
            return GetUploadTicket(null, clipId);
        }

        /// <summary>
        /// Generate an upload ticket
        /// </summary>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public UploadTicket GenerateUploadTicket(string userId, GenerateTicketParameterContext parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException("parameters");
            }

            return Execute(UserVideosServiceEndpoint(userId == null),
                           new { user_id = userId, clip_id = string.Empty },
                           parameters,
                           Method.POST).ToObject<UploadTicket>();
        }

        /// <summary>
        /// Generate an upload ticket
        /// </summary>
        /// <returns></returns>
        public UploadTicket GenerateUploadTicket(GenerateTicketParameterContext parameters)
        {
            return GenerateUploadTicket(null, parameters);
        }

        /// <summary>
        /// Check how much of a file has transferred
        /// </summary>
        /// <param name="uri">Upload Link</param>
        /// <returns>Number of bytes on the server</returns>
        public long VerifyUpload(string uri)
        {
            var endpoint = new Endpoint() { BaseUri = uri };
            var client = Client.CreateClient(endpoint);
            var request = Client.CreateRequest(endpoint, Method.PUT);
            request.AddHeader("Content-Range", "bytes */*");

            var response = client.Execute(request);

            if ((int)response.StatusCode == 308)
            {
                long firstByte = 0, length = 0;
                response.ReadHeaderRange(out firstByte, out length);
                return length;
            }

            throw new UnexpectedResponseException(response);
        }

        /// <summary>
        /// Completes the upload
        /// </summary>
        /// <param name="completeUrl">Complete Upload Url</param>
        /// <returns>Clip Id</returns>
        public string CompleteUpload(string completeUrl)
        {
            var endpoint = new Endpoint() { BaseUri = Client.BaseUrl, Resource = completeUrl };
            var client = Client.CreateClient(endpoint);
            var request = Client.CreateRequest(endpoint, Method.DELETE);

            var response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                var location = response.GetHeaderValue("Location");
                if (location != null)
                {
                    return location;
                }
            }

            throw new UnexpectedResponseException(response);
        }

        #endregion

        #region /users/watchlater

        /// <summary>
        /// Gets User/Current User's watchlater Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint UserWatchLaterServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/watchlater/{clip_id}" : "/users/{user_id}/watchlater/{clip_id}"
            };
        }

        /// <summary>
        /// Get the authenticated user's Watch Later list
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetUserWatchLaterlist(string userId, VideoFilterableParametersContext parameters)
        {
            return Execute(UserWatchLaterServiceEndpoint(userId == null),
                            new { user_id = userId, clip_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Video>>();
        }

        /// <summary>
        /// Get the current authenticated user's Watch Later list
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetUserWatchLaterlist(VideoFilterableParametersContext parameters)
        {
            return GetUserWatchLaterlist(null, parameters);
        }

        /// <summary>
        /// Is this video in the authenticated user's Watch Later?
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public bool IsVideoInWatchlist(string userId, string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }

            try
            {
                Execute(UserWatchLaterServiceEndpoint(userId == null),
                               new { user_id = userId, clip_id = clipId },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no video found
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
        /// Is this video in the current authenticated user's Watch Later?
        /// </summary>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public bool IsVideoInWatchlist(string clipId)
        {
            return IsVideoInWatchlist(null, clipId);
        }

        #endregion

        #region /users/tickets

        /// <summary>
        /// Gets the tickets Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint TicketsServiceEndpoint()
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = "/users/{user_id}/tickets/{ticket}"
            };
        }

        /// <summary>
        /// Does the upload ticket exist?
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="ticket">Upload ticket</param>
        /// <returns></returns>
        public bool DoesUploadTicketExist(string userId, string ticket)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("You must provide a valid userId", "userId");
            }
            if (string.IsNullOrEmpty(ticket))
            {
                throw new ArgumentException("You must provide a valid ticket", "ticket");
            }

            try
            {
                Execute(TicketsServiceEndpoint(),
                               new { user_id = userId, ticket = ticket },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no ticket found
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

        #endregion

        //TODO: NOT TESTED: Do not have money for PRO account
        #region /users/portfolios

        /// <summary>
        /// Gets User/Current User's portfolios Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint PortfoliosServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/portfolios/{portfolio_id}" : "/users/{user_id}/portfolios/{portfolio_id}"
            };
        }

        /// <summary>
        /// Get a list of Portfolios created by a user
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JObject GetPortfolios(string userId, DefaultParametersContext parameters)
        {
            return Execute(PortfoliosServiceEndpoint(userId == null),
                            new { user_id = userId, portfolio_id = string.Empty },
                            parameters,
                            Method.GET);
        }

        /// <summary>
        /// Get a list of Portfolios created by the current user
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public JObject GetPortfolios(DefaultParametersContext parameters)
        {
            return GetPortfolios(null, parameters);
        }


        /// <summary>
        /// Get a Portfolio.
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="portfolioId">portfolioId</param>
        /// <returns></returns>
        public JObject GetPortfolio(string userId, string portfolioId)
        {
            return Execute(PortfoliosServiceEndpoint(userId == null),
                            new { user_id = userId, portfolio_id = portfolioId },
                            null,
                            Method.GET);
        }

        /// <summary>
        /// Get a Portfolio.
        /// </summary>
        /// <param name="portfolioId">portfolioId</param>
        /// <returns></returns>
        public JObject GetPortfolio(string portfolioId)
        {
            return GetPortfolio(null, portfolioId);
        }

        #endregion

        //TODO: NOT TESTED: Do not have money for PRO account
        #region /users/portfolios/videos

        /// <summary>
        /// Gets User/Current User's portfolio videos Endpoint
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        protected virtual Endpoint PortfolioVideosServiceEndpoint(bool currentUser)
        {
            return new Endpoint()
            {
                BaseUri = Client.BaseUrl,
                Resource = currentUser ? "/me/portfolios/{portfolio_id}/videos/{clip_id}" : "users/{user_id}/portfolios/{portfolio_id}/videos/{clip_id}"
            };
        }

        /// <summary>
        /// Get the videos in this Portfolio
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="portfolioId">portfolioId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetVieosInPortfolio(string userId, string portfolioId, DefaultParametersContext parameters)
        {
            if (string.IsNullOrEmpty(portfolioId))
            {
                throw new ArgumentException("You must provide parameter portfolioId", "portfolioId");
            }

            return Execute(PortfolioVideosServiceEndpoint(userId == null),
                            new { user_id = userId, portfolio_id = portfolioId, clip_id = string.Empty },
                            null,
                            Method.GET).ToObject<DefaultResultSet<Video>>();
        }

        /// <summary>
        /// Get the videos in this Portfolio
        /// </summary>
        /// <param name="portfolioId">portfolioId</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetVieosInPortfolio(string portfolioId, DefaultParametersContext parameters)
        {
            return GetVieosInPortfolio(null, portfolioId, parameters);
        }

        /// <summary>
        /// Get a clip from a Portfolio
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="portfolioId">portfolioId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public bool IsVideoInPortfolio(string userId, string portfolioId, string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(portfolioId))
            {
                throw new ArgumentException("You must provide a valid portfolioId", "portfolioId");
            }

            try
            {
                Execute(PortfolioVideosServiceEndpoint(userId == null),
                               new { user_id = userId, portfolio_id = portfolioId, clip_id = clipId },
                               null,
                               Method.GET);
                return true;
            }
            catch (UnexpectedResponseException ex)
            {
                // If 404? there is no video found
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
        /// Get a clip from a Portfolio
        /// </summary>
        /// <param name="portfolioId">portfolioId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public bool IsVideoInPortfolio(string portfolioId, string clipId)
        {
            return IsVideoInPortfolio(null, portfolioId, clipId);
        }

        /// <summary>
        /// Add a video to the Portfolio
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="portfolioId">portfolioId</param>
        /// <param name="clipId">clipId</param>
        public void AddVideoToPortfolio(string userId, string portfolioId, string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(portfolioId))
            {
                throw new ArgumentException("You must provide a valid portfolioId", "portfolioId");
            }

            Execute(PortfolioVideosServiceEndpoint(userId == null),
                               new { user_id = userId, portfolio_id = portfolioId, clip_id = clipId },
                               null,
                               Method.PUT);
        }

        /// <summary>
        /// Add a video to the Portfolio
        /// </summary>
        /// <param name="portfolioId">portfolioId</param>
        /// <param name="clipId">clipId</param>
        public void AddVideoToPortfolio(string portfolioId, string clipId)
        {
            AddVideoToPortfolio(null, portfolioId, clipId);
        }

        /// <summary>
        /// Remove a video from the Portfolio
        /// </summary>
        /// <param name="userId">userId</param>
        /// <param name="portfolioId">portfolioId</param>
        /// <param name="clipId">clipId</param>
        public void RemoveVideoFromPortfolio(string userId, string portfolioId, string clipId)
        {
            if (string.IsNullOrEmpty(clipId))
            {
                throw new ArgumentException("You must provide a valid clipId", "clipId");
            }
            if (string.IsNullOrEmpty(portfolioId))
            {
                throw new ArgumentException("You must provide a valid portfolioId", "portfolioId");
            }

            Execute(PortfolioVideosServiceEndpoint(userId == null),
                               new { user_id = userId, portfolio_id = portfolioId, clip_id = clipId },
                               null,
                               Method.DELETE);
        }

        /// <summary>
        /// Remove a video from the Portfolio
        /// </summary>
        /// <param name="portfolioId">portfolioId</param>
        /// <param name="clipId">clipId</param>
        public void RemoveVideoFromPortfolio(string portfolioId, string clipId)
        {
            RemoveVideoFromPortfolio(null, portfolioId, clipId);
        }

        #endregion

    }
}
