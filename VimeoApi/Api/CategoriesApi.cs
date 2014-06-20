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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OAuth2.Infrastructure;
using VimeoApi.OAuth2.Clients.Impl;
using VimeoApi.Models;
using RestSharp;

namespace VimeoApi.Api
{
    public class CategoriesApi : VimeoApi
    {
        public CategoriesApi(VimeoClient client)
            : base(client)
        {
        }


        #region /categories

        protected virtual Endpoint CategoriesServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/categories/{category}"
                };
            }
        }

        /// <summary>
        /// Get a list of the top level categories.
        /// </summary>
        /// <returns></returns>
        public DefaultResultSet<Category> GetCategories()
        {
            return GetCategories(null, null);
        }

        /// <summary>
        /// Get a list of the top level categories.
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="perPage">Page size</param>
        /// <returns></returns>
        public DefaultResultSet<Category> GetCategories(int? page, int? perPage)
        {
            return Execute(CategoriesServiceEndpoint,
                            new { category = string.Empty },
                            new { page = page, per_page = perPage },
                            Method.GET).ToObject<DefaultResultSet<Category>>();
        }

        /// <summary>
        /// Get an individual Channel.
        /// </summary>
        /// <param name="category">category</param>
        /// <returns></returns>
        public Category GetCategory(string category)
        {
            if (category.IsEmpty())
            {
                throw new ArgumentException("You must provide categoryId", "categoryId");
            }

            return Execute(CategoriesServiceEndpoint,
                            new { category = category },
                            null,
                            Method.GET).ToObject<Category>();
        }

        #endregion

        #region /categories/channels

        protected virtual Endpoint CategoryChannelsServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/categories/{category}/channels"
                };
            }
        }

        /// <summary>
        /// Get a list of Channels related to a category.
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Channel> GetCategoryChannels(string category, DefaultParametersContext parameters)
        {
            return Execute(CategoryChannelsServiceEndpoint,
                            new { category = category },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Channel>>();
        }

        #endregion

        #region /categories/groups

        protected virtual Endpoint CategoryGroupsServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/categories/{category}/groups"
                };
            }
        }

        /// <summary>
        /// Get a list of groups related to a category.
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Group> GetCategoryGroups(string category, DefaultParametersContext parameters)
        {
            return Execute(CategoryGroupsServiceEndpoint,
                            new { category = category },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Group>>();
        }

        #endregion

        #region /categories/users

        protected virtual Endpoint CategoryUsersServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/categories/{category}/users"
                };
            }
        }

        /// <summary>
        ///Get a list of users related to a category.
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<User> GetCategoryUsers(string category, DefaultParametersContext parameters)
        {
            return Execute(CategoryUsersServiceEndpoint,
                            new { category = category },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<User>>();
        }

        #endregion

        #region /categories/videos

        protected virtual Endpoint CategoryVideosServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = Client.BaseUrl,
                    Resource = "/categories/{category}/videos"
                };
            }
        }

        /// <summary>
        /// Get a list of videos related to a category.
        /// </summary>
        /// <param name="category">category</param>
        /// <param name="parameters">parameters</param>
        /// <returns></returns>
        public DefaultResultSet<Video> GetCategoryVideos(string category, VideoFilterableParametersContext parameters)
        {
            return Execute(CategoryVideosServiceEndpoint,
                            new { category = category },
                            parameters,
                            Method.GET).ToObject<DefaultResultSet<Video>>();
        }

        #endregion
    }
}
