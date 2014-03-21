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
    public class AuthenticatedViaRedirectVimeoClientTests
    {
        private HttpSimulator _httpSimulator;
        private AuthenticatedViaRedirectVimeoClient _client;

        [TestInitialize]
        public void Init()
        {
            _httpSimulator = AppStart.SetDependencyResolver();
            _client = AppStart.InitVimeoClient<AuthenticatedViaRedirectVimeoClient>();
        }

        [TestMethod]
        public void TestAuthenticatedViaRedirectVimeoClient()
        {
        }

        [TestCleanup]
        public void Cleanup()
        {
            _httpSimulator.Dispose();
        }
    }
}
