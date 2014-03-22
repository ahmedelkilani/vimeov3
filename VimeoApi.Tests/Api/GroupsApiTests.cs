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
    public class GroupsApiTests
    {
        private HttpSimulator _httpSimulator;
        private GroupsApi _groupsApi;

        [TestInitialize]
        public void Init()
        {
            _httpSimulator = AppStart.SetDependencyResolver();
            _groupsApi = AppStart.InitVimeoClient<AuthenticatedViaRedirectVimeoClient>().Groups();
        }

        #region Test Variables

        private const string GROUP_ID = "1";
        private const string CLIPINGROUP_ID = "76275361";
        private const string USER_CLIP_ID = "88709023";
        private const string POPULAR_CLIP_ID = "58910669";

        #endregion

        #region /groups

        [TestMethod]
        public void GetGroups()
        {
            var response = _groupsApi.GetGroups(new DefaultParametersContext()
            {
                //query = "test"
            });

            Assert.AreNotEqual(null, response.data);
            Assert.AreNotEqual(0, response.data.Count);
        }

        //TODO: {"error":"The user is not allowed to perform that action. [The authenticated user does not have permission to create a group]."}
        [TestMethod]
        public void CreateGroup()
        {
             _groupsApi.CreateGroup("TestGroup","Test Group");

        }

        [TestMethod]
        public void GetGroup()
        {
            var response = _groupsApi.GetGroup(GROUP_ID);

            Assert.AreEqual("Nerds Are Awesome", response.name);
        }

        [TestMethod]
        public void EditGroup()
        {
            _groupsApi.EditGroup(GROUP_ID, "", "EditedTestGroup", "Edited Test Group");

        }


        #endregion

        #region /groups/videos
        
        [TestMethod]
        public void GetVideosInGroup()
        {
            var response = _groupsApi.GetVideosInGroup(GROUP_ID, null);

            Assert.AreNotEqual(0, response.data.Count);
        }

        [TestMethod]
        public void GetVideoFromGroup()
        {
            var response = _groupsApi.GetVideoFromGroup(GROUP_ID, CLIPINGROUP_ID);

            Assert.AreEqual("For Nerds", response.name);
        }

        [TestMethod]
        public void AddRemoveVideoFromGroup()
        {            
            _groupsApi.RemoveVideoFromGroup(GROUP_ID, USER_CLIP_ID);
            // Add a non-existing video
            var response = _groupsApi.AddVideoToGroup(GROUP_ID, USER_CLIP_ID);
            Assert.AreEqual(AddingVideoToGroupResult.Added, response);
            // Add an existing video
            response = _groupsApi.AddVideoToGroup(GROUP_ID, USER_CLIP_ID);
            Assert.AreEqual(AddingVideoToGroupResult.AleadyExists, response);
        }

        #endregion

        #region /groups/users

        [TestMethod]
        public void GetUsersInGroup()
        {
            var response = _groupsApi.GetUsersInGroup(GROUP_ID, null);

            Assert.AreNotEqual(0, response.data.Count);
        }

        #endregion


        [TestCleanup]
        public void Cleanup()
        {
            _httpSimulator.Dispose();
        }
    }
}
