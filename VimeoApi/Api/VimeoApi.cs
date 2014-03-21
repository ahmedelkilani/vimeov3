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
using RestSharp;
using VimeoApi.OAuth2.Clients.Impl;

namespace VimeoApi.Api
{
    public abstract class VimeoApi
    {
        protected VimeoClient Client;

        public VimeoApi(VimeoClient client)
        {
            Client = client;
        }

        protected virtual JObject Execute(Endpoint endpoint, object urlSegments, object parameters, Method method, DataFormat? requestFormat)
        {
            var client = Client.CreateClient(endpoint);
            var request = Client.CreateRequest(endpoint, method);

            var response = ExecuteRaw(client, request, urlSegments, parameters, method, requestFormat);

            var responseContent = string.IsNullOrWhiteSpace(response.Content) ? "{}" : response.Content;

            return JObject.Parse(responseContent);
        }

        protected virtual JObject Execute(Endpoint endpoint, object urlSegments, object parameters, Method method)
        {
            return Execute(endpoint, urlSegments, parameters, method, null);
        }

        protected JObject Execute(Endpoint endpoint, object urlSegments, object parameters = null)
        {
            return Execute(endpoint, urlSegments, parameters, Method.GET);
        }

        protected JObject Execute(Endpoint endpoint, Method method)
        {
            return Execute(endpoint, null, null, method);
        }

        protected JObject Execute(Endpoint endpoint)
        {
            return Execute(endpoint, null, null, Method.GET);
        }


        protected virtual IRestResponse ExecuteRaw(IRestClient client, IRestRequest request, object urlSegments, object parameters, Method method, DataFormat? requestFormat = null)
        {
            if (urlSegments != null)
                request.AddObject(urlSegments, ParameterType.UrlSegment);

            // If requestFormat is specified, then we should serialize the parameters as RequestBody
            if (requestFormat.HasValue)
            {
                request.RequestFormat = requestFormat.Value;

                if (parameters != null)
                    request.AddBody(parameters);
            }
            else
            {
                if (parameters != null)
                    request.AddObject(parameters);
            }
            return client.ExecuteAndCheck(request);
        }
    }
}
