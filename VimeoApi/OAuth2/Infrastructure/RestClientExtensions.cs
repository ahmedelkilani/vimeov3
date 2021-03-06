﻿#region License
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

using OAuth2.Client;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Infrastructure
{
    public static class RestClientExtensions
    {
        public static IRestResponse ExecuteAndCheck(this IRestClient client, IRestRequest request)
        {
            var response = client.Execute(request);
            if (response.StatusCode != HttpStatusCode.Continue &&
                response.StatusCode != HttpStatusCode.OK &&
                response.StatusCode != HttpStatusCode.Created &&
                response.StatusCode != HttpStatusCode.Accepted &&
                response.StatusCode != HttpStatusCode.Found &&
                response.StatusCode != HttpStatusCode.NoContent)
            {
                //TODO: wrap error messages
                throw new UnexpectedResponseException(response);
            }
            return response;
        }
    }
}
