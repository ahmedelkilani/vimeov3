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

namespace RestSharp
{
    public static class IRestResponseExtensions
    {
        //TODO: refactor
        /// <summary>
        /// Read Range from Response header.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="from">First byte position</param>
        /// <param name="to">Last byte position</param>
        public static void ReadHeaderRange(this IRestResponse response, out long from, out long to)
        {
            //Setting default values
            from = to = 0;

            var contentRange = GetHeaderValue(response, "Range");
            if (contentRange != null)
            {
                var bytesData = contentRange.Split('=');
                if (bytesData.Length > 1)
                {
                    var bytesRange = bytesData[1].Split('-');
                    long.TryParse(bytesRange[0], out from);
                    if (bytesRange.Length > 1)
                        long.TryParse(bytesRange[1], out to);
                }
            }
        }

        /// <summary>
        /// Gets the value of a response header.
        /// </summary>
        /// <param name="response"></param>
        /// <param name="headerName">Header name</param>
        /// <returns></returns>
        public static string GetHeaderValue(this IRestResponse response, string headerName)
        {
            var header = response.Headers.FirstOrDefault(p => p.Name.Equals(headerName, StringComparison.OrdinalIgnoreCase));
            if (header != null)
                return header.Value as string;

            return null;
        }
    }
}
