using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VimeoApi.Tests
{
    /// <summary>
    /// General (garbage) Test Class
    /// </summary>
    [TestClass]
    public class Console
    {
        /// <summary>
        /// Test anything you want..
        /// </summary>
        [TestMethod]
        public void Run()
        {
            string[] tags = new string[] { "user", "tag" };
            object[] x = { new { tag = "tag" } };
            //var o = Newtonsoft.Json.Linq.JObject.FromObject(x);

            string val = JsonConvert.SerializeObject(x);
           // var val = "";
            var arr = val.Split('-');
        }
    }
}
