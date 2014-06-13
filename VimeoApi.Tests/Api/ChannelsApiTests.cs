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
    public class ChannelsApiTests
    {
        private HttpSimulator _httpSimulator;
        private ChannelsApi _channelsApi;

        [TestInitialize]
        public void Init()
        {
            _httpSimulator = AppStart.SetDependencyResolver();
            _channelsApi = AppStart.InitVimeoClient<AuthenticatedViaRedirectVimeoClient>().Channels();
        }

        #region Test Variables

        private const string CHANNEL_ID = "699606";
        private const string CLIPINCHANNEL_ID = "88709023";

        #endregion

        #region /channels

        [TestMethod]
        public void GetChannels()
        {
            var response = _channelsApi.GetChannels(new DefaultParametersContext()
            {
                //query = "test"
            });

            Assert.AreNotEqual(null, response.data);
            Assert.AreNotEqual(0, response.data.Count);
        }

        [TestMethod]
        public void GetChannel()
        {
            var response = _channelsApi.GetChannel(CHANNEL_ID);

            Assert.AreEqual("Test Channel", response.name);
        }

        [TestMethod]
        public void EditChannel()
        {
            _channelsApi.EditChannel(CHANNEL_ID, null, "No Desc", ChannelPrivacy.anybody);

        }

        #endregion

        #region /channels/videos

        [TestMethod]
        public void GetVideosInChannel()
        {
            var response = _channelsApi.GetVideosInChannel(CHANNEL_ID, null);

            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void GetVideoFromChannel()
        {
            var response = _channelsApi.GetVideoFromChannel(CHANNEL_ID, CLIPINCHANNEL_ID);

            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void AddVideoToChannel()
        {
            _channelsApi.AddVideoToChannel(CHANNEL_ID, CLIPINCHANNEL_ID);
        }

        #endregion

        #region /channels/users

        [TestMethod]
        public void GetUsersFollowingChannel()
        {
            var response = _channelsApi.GetUsersFollowingChannel(CHANNEL_ID);
            
            Assert.AreNotEqual(null, response);
        }

        #endregion


        [TestCleanup]
        public void Cleanup()
        {
            _httpSimulator.Dispose();
        }
    }
}
