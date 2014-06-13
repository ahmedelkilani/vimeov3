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
using RestSharp;
using OAuth2.Infrastructure;
using System.Net;

namespace VimeoApi.Api
{
    public class ChannelsApi : VimeoApi
    {
        public ChannelsApi(VimeoClient client)
            : base(client)
        {
        }

        #region /channels

        protected virtual Endpoint ChannelsServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/channels/{channel_id}"
                };
            }
        }

        /// <summary>
        /// Get a list of all Channels.
        /// </summary>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Channel> GetChannels(DefaultParametersContext parameters)
        {
            return Execute(ChannelsServiceEndpoint,
                            new { channel_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Channel>>();
        }

        /// <summary>
        /// Get an individual Channel.
        /// </summary>
        /// <param name="channelId">channelId</param>
        /// <returns></returns>
        public Channel GetChannel(string channelId)
        {
            if (channelId.IsEmpty())
            {
                throw new ArgumentException("You must provide channelId", "channelId");
            }

            return Execute(ChannelsServiceEndpoint,
                            new { channel_id = channelId },
                            null,
                            Method.GET).ToObject<Channel>();
        }

        /// <summary>
        /// Edit a Channel's information
        /// </summary>
        /// <param name="channelId">channelId</param>
        /// <param name="name">The Channel's new name</param>
        /// <param name="description">The Channel's new description</param>
        /// <param name="privacy">The Channel's new privacy level</param>
        public void EditChannel(string channelId, string name, string description, ChannelPrivacy privacy)
        {
            if (channelId.IsEmpty())
            {
                throw new ArgumentException("You must provide channelId", "channelId");
            }

            Execute(ChannelsServiceEndpoint,
                            new { channel_id = channelId },
                            new { name = name, description = description, privacy = privacy },
                            Method.PATCH);
        }

        /// <summary>
        /// Delete a Channel
        /// </summary>
        /// <param name="channelId">channelId</param>
        public void DeleteChannel(string channelId)
        {
            if (channelId.IsEmpty())
            {
                throw new ArgumentException("You must provide channelId", "channelId");
            }

            Execute(ChannelsServiceEndpoint,
                            new { channel_id = channelId },
                            null,
                            Method.DELETE);
        }

        #endregion

        #region /channels/videos

        protected virtual Endpoint ChannelsVideosServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/channels/{channel_id}/videos/{clip_id}"
                };
            }
        }

        /// <summary>
        /// Get a list of videos in a Channel.
        /// </summary>
        /// <param name="channelId">channelId</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetVideosInChannel(string channelId, VideoFilterableParametersContext parameters)
        {
            if (channelId.IsEmpty())
            {
                throw new ArgumentException("You must provide channelId", "channelId");
            }

            return Execute(ChannelsVideosServiceEndpoint,
                            new { channel_id = channelId, clip_id = string.Empty },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Video>>();

        }

        /// <summary>
        /// Get a clip from this Channel
        /// </summary>
        /// <param name="channelId">channelId</param>
        /// <param name="clipId">clipId</param>
        /// <returns></returns>
        public Video GetVideoFromChannel(string channelId, string clipId)
        {
            if (channelId.IsEmpty())
            {
                throw new ArgumentException("You must provide channelId", "channelId");
            }
            if (clipId.IsEmpty())
            {
                throw new ArgumentException("You must provide clipId", "clipId");
            }

            return Execute(ChannelsVideosServiceEndpoint,
                            new { channel_id = channelId, clip_id = clipId },
                            null,
                            Method.GET).ToObject<Video>();

        }

        /// <summary>
        /// Add a video to a Channel.
        /// </summary>
        /// <param name="channelId">channelId</param>
        /// <param name="clipId">clipId</param>
        public void AddVideoToChannel(string channelId, string clipId)
        {
            if (channelId.IsEmpty())
            {
                throw new ArgumentException("You must provide channelId", "channelId");
            }
            if (clipId.IsEmpty())
            {
                throw new ArgumentException("You must provide clipId", "clipId");
            }

            Execute(ChannelsVideosServiceEndpoint,
                            new { channel_id = channelId, clip_id = clipId },
                            null,
                            Method.PUT);

        }

        /// <summary>
        /// Remove a video from a Channel.
        /// </summary>
        /// <param name="channelId">channelId</param>
        /// <param name="clipId">clipId</param>
        public void RemoveVideoFromChannel(string channelId, string clipId)
        {
            if (channelId.IsEmpty())
            {
                throw new ArgumentException("You must provide channelId", "channelId");
            }
            if (clipId.IsEmpty())
            {
                throw new ArgumentException("You must provide clipId", "clipId");
            }

            Execute(ChannelsVideosServiceEndpoint,
                            new { channel_id = channelId, clip_id = clipId },
                            null,
                            Method.DELETE);

        }

        #endregion

        #region /channels/users

        protected virtual Endpoint ChannelsUsersServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/channels/{channel_id}/users"
                };
            }
        }

        /// <summary>
        /// Get a list of users that follow a Channel.
        /// </summary>
        /// <param name="channelId">channelId</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetUsersFollowingChannel(string channelId)
        {
            if (channelId.IsEmpty())
            {
                throw new ArgumentException("You must provide channelId", "channelId");
            }

            return Execute(ChannelsUsersServiceEndpoint,
                            new { channel_id = channelId },
                            null,
                            Method.GET).ToObject<DefaultResultSet<User>>();
            
        }

        #endregion

    }
}
