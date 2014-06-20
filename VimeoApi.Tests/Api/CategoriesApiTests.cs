using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subtext.TestLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimeoApi.Api;
using VimeoApi.Models;
using VimeoApi.OAuth2.Clients.Impl;
using VimeoApi.Tests.Setup;

namespace VimeoApi.Tests.Api
{
    [TestClass]
    public class CategoriesApiTests
    {
        private HttpSimulator _httpSimulator;
        private CategoriesApi _categoriesApi;

        [TestInitialize]
        public void Init()
        {
            _httpSimulator = AppStart.SetDependencyResolver();
            _categoriesApi = AppStart.InitVimeoClient<AuthenticatedViaRedirectVimeoClient>().Categories();
        }

        #region Test Variables

        private const string CATEGORY = "activism";

        #endregion

        #region /channels

        [TestMethod]
        public void GetCategories()
        {
            var response = _categoriesApi.GetCategories(1, 4);

            Assert.AreNotEqual(null, response.data);
            Assert.AreEqual(4, response.data.Count);
        }

        [TestMethod]
        public void GetCategory()
        {
            var response = _categoriesApi.GetCategory(CATEGORY);

            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void GetChannel()
        {
        }

        [TestMethod]
        public void EditChannel()
        {

        }

        #endregion

        #region /channels/categories

        [TestMethod]
        public void GetCategoryChannels()
        {
            var response = _categoriesApi.GetCategoryChannels(CATEGORY, null);

            Assert.AreNotEqual(null, response.data);
        }

        #endregion

        #region /channels/groups

        [TestMethod]
        public void GetCategoryGroups()
        {
            var response = _categoriesApi.GetCategoryGroups(CATEGORY, null);

            Assert.AreNotEqual(null, response.data);
        }

        #endregion

        #region /channels/users

        [TestMethod]
        public void GetCategoryUsers()
        {
            var response = _categoriesApi.GetCategoryUsers(CATEGORY, null);

            Assert.AreNotEqual(null, response.data);
        }

        #endregion

        #region /channels/videos

        [TestMethod]
        public void GetCategoryVideos()
        {
            var response = _categoriesApi.GetCategoryVideos(CATEGORY, null);

            Assert.AreNotEqual(null, response.data);
        }

        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            _httpSimulator.Dispose();
        }
    }
}
