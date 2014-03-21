using System;
using OAuth2.Infrastructure;
using VimeoApi.Tests.Setup;
using System.Web.Mvc;
using VimeoApi.OAuth2;
using VimeoApi.OAuth2.Clients;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subtext.TestLibrary;
using System.Collections.Specialized;
using VimeoApi.OAuth2.Clients.Impl;

namespace VimeoApi.Tests
{
    [TestClass]
    public class UnauthenticatedVimeoClientTests
    {
        private HttpSimulator _httpSimulator;
        private UnauthenticatedVimeoClient _client;

        [TestInitialize]
        public void Init()
        {
            _httpSimulator = AppStart.SetDependencyResolver();
            _client = AppStart.InitVimeoClient<UnauthenticatedVimeoClient>();
        }

        [TestMethod]
        public void TestUnauthenticatedVimeoClient()
        {
            var token = _client.GetToken(new NameValueCollection() { { "error", "" } });
            Assert.AreEqual(string.IsNullOrEmpty(token), false);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _httpSimulator.Dispose();
        }
    }
}
