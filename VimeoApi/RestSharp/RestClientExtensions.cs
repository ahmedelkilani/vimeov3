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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp
{
    public static class RestClientExtensions
    {
        /// <summary>
        /// Calls AddParameter() for all public, readable properties of obj
        /// </summary>
        /// <param name="restRequest"></param>
        /// <param name="obj">The object with properties to add as parameters</param>
        /// <param name="parametersTypes">The types of parameters to add</param>
        public static IRestRequest AddObject(this IRestRequest restRequest, object obj, ParameterType parametersTypes)
        {
            var type = obj.GetType();
            var props = type.GetProperties();

            foreach (var prop in props)
            {
                var propType = prop.PropertyType;
                var val = prop.GetValue(obj, null);

                if (val != null)
                {
                    if (propType.IsArray)
                    {
                        var elementType = propType.GetElementType();

                        if (((Array)val).Length > 0 && (elementType.IsPrimitive || elementType.IsValueType || elementType == typeof(string)))
                        {
                            // convert the array to an array of strings
                            var values = (from object item in ((Array)val) select item.ToString()).ToArray<string>();
                            val = string.Join(",", values);
                        }
                        else
                        {
                            // try to cast it
                            val = string.Join(",", (string[])val);
                        }
                    }

                    restRequest.AddParameter(prop.Name, val, parametersTypes);
                }
            }

            return restRequest;
        }
    }
}
