using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VimeoApi.Tests.Setup;
using System.Web.Mvc;
using VimeoApi.OAuth2.Clients;
using Subtext.TestLibrary;
using VimeoApi.Api;
using VimeoApi.OAuth2.Clients.Impl;
using VimeoApi.Models;
using System.Linq;

namespace VimeoApi.Tests.Api.Videos
{
    [TestClass]
    public class VideosApiTests
    {
        private HttpSimulator _httpSimulator;
        private VideosApi _videosApi;

        [TestInitialize]
        public void Init()
        {
            _httpSimulator = AppStart.SetDependencyResolver();
            _videosApi = AppStart.InitVimeoClient<AuthenticatedViaRedirectVimeoClient>().Videos();
        }

        // Failed methods
        //****************
        //DeleteVideo
        //

        #region Test Variables

        private const string USER_CLIP_ID = "88709023";
        private const string POPULAR_CLIP_ID = "58910669";
        private const string COMMENT_ID = "11243449";
        private const string CREDIT_ID = "90678393";
        private const string PRESET_ID = "";
        private const string PRIVACY_TEST_CLIP_ID = "89054388";
        private const string USER_ID = "25760851";

        #endregion

        #region /videos

        [TestMethod]
        public void GetVideos()
        {
            var response = _videosApi.GetVideos(new DefaultParametersContext()
            {
                query = "Carrie Underwood"
            });

            Assert.AreNotEqual(null, response.data);
            Assert.AreNotEqual(0, response.data.Count);
        }

        [TestMethod]
        public void GetVideoById()
        {
            var response = _videosApi.GetVideo(USER_CLIP_ID);

            Assert.AreNotEqual(null, response);
            Assert.AreNotEqual("", response.name);
        }

        [TestMethod]
        public void EditVideo()
        {
            _videosApi.EditVideo(USER_CLIP_ID, new VideoParametersContext()
            {
                name = "Testing EditVideo" + DateTime.Now.ToString()
            });

        }

        [TestMethod]
        public void DeleteVideo()
        {
            _videosApi.DeleteVideo(USER_CLIP_ID);
        }
        #endregion

        #region /videos/comments

        [TestMethod]
        public void GetVideoComments()
        {
            var response = _videosApi.GetVideoComments(USER_CLIP_ID);

            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void PostCommentOnVideo()
        {
            var commentText = "Testing PostCommentOnVideo " + DateTime.Now.ToString();
            _videosApi.PostCommentOnVideo(USER_CLIP_ID, commentText);
        }

        [TestMethod]
        public void GetCommentOnVideo()
        {
            var response = _videosApi.GetCommentOnVideo(USER_CLIP_ID, COMMENT_ID);

            Assert.AreNotEqual(null, response);
            Assert.AreNotEqual("", response.text);
        }

        [TestMethod]
        public void EditCommentOnVideo()
        {
            var commentText = "Testing EditCommentOnVideo " + DateTime.Now.ToString();
            _videosApi.EditCommentOnVideo(USER_CLIP_ID, COMMENT_ID, commentText);
        }

        [TestMethod]
        public void DeleteCommentFromVideo()
        {
            _videosApi.DeleteCommentFromVideo(USER_CLIP_ID, COMMENT_ID);
        }

        #endregion

        #region /videos/credits

        [TestMethod]
        public void TestGetVideoCredits()
        {
            var response = _videosApi.GetVideoCredits(USER_CLIP_ID, null);

            Assert.AreNotEqual(null, response.data);
        }

        [TestMethod]
        public void TestGetVideoCredit()
        {
            var response = _videosApi.GetVideoCredit(USER_CLIP_ID, CREDIT_ID);

            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void TestPostCreditForVideo()
        {
            _videosApi.PostCreditForVideo(USER_CLIP_ID, new VideoCreditParameterContext()
            {
                name = "Me",
                email = "test@ahmedkilani.com"
            });
        }

        [TestMethod]
        public void TestEditVideoCredit()
        {
            _videosApi.EditCreditOnVideo(USER_CLIP_ID, CREDIT_ID, "edit role");
        }

        [TestMethod]
        public void TestDeleteCreditFromVideo()
        {
            _videosApi.DeleteCreditFromVideo(USER_CLIP_ID, CREDIT_ID);
        }

        #endregion

        #region /videos/likes

        [TestMethod]
        public void GetVideoLikes()
        {
            var result = _videosApi.GetVideoLikes(POPULAR_CLIP_ID, null);

            Assert.AreNotEqual(0, result.data.Count);
        }

        #endregion

        //TODO: find out how to create presets
        #region /videos/presets

        [TestMethod]
        public void ApplyPreset()
        {
            _videosApi.ApplyPreset(USER_CLIP_ID, PRESET_ID);
            //
            var result = _videosApi.DoesPresetExistInVideo(USER_CLIP_ID, PRESET_ID);
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void RemovePreset()
        {
            _videosApi.RemovePreset(USER_CLIP_ID, PRESET_ID);
            //
            var result = _videosApi.DoesPresetExistInVideo(USER_CLIP_ID, PRESET_ID);
            Assert.AreEqual(false, result);
        }

        #endregion

        #region /videos/videos

        //TODO: {"error":"An unknown error has occured. Please let us know!"}
        [TestMethod]
        public void GetSimilarVideos()
        {
            var result = _videosApi.GetSimilarVideos(USER_CLIP_ID, new DefaultParametersContext()
            {
                filter="test"
            });
            Assert.AreNotEqual(0, result.data.Count);
        }

        #endregion

        #region /videos/tags

        //TODO: {"error":"An unknown error has occured. Please let us know!"}
        [TestMethod]
        public void TagsOnVideo()
        {
            string testTag = "test";
            //Add tags
            _videosApi.AssignTagToVideo(USER_CLIP_ID, new string[] { testTag, "clip" });
            //Get tags
            var result = _videosApi.GetTagsOnVideo(USER_CLIP_ID);
            //Verify tag is retrieved
            Assert.AreEqual(true, result.data.Any(t => t.tag.Equals(testTag)));
            //Remove tag
            _videosApi.RemoveTagFromVideo(USER_CLIP_ID, testTag);
            //verify tag does not exist
            var exists = _videosApi.DoesTagExistForVideo(POPULAR_CLIP_ID, testTag);
            Assert.AreEqual(false, exists);

        }

        #endregion

        #region /videos/privacy/users

        [TestMethod]
        public void VideoPrivacyUsers()
        {
            //Allow user
            _videosApi.AllowedUser(PRIVACY_TEST_CLIP_ID, USER_ID);
            var result = _videosApi.GetAllowedUsers(PRIVACY_TEST_CLIP_ID);
            Assert.AreEqual(true, result.data.Any(u => u.uri.Contains(USER_ID)));
            //Disallow user
            _videosApi.DisallowedUser(PRIVACY_TEST_CLIP_ID, USER_ID);
            result = _videosApi.GetAllowedUsers(PRIVACY_TEST_CLIP_ID);
            Assert.AreEqual(false, result.data.Any(u => u.uri.Contains(USER_ID)));
            // Query on a video that has not setup its privacy to users // return null
            result = _videosApi.GetAllowedUsers(USER_CLIP_ID);
            Assert.AreEqual(null, result);
        }
                
        #endregion

        #region /videos/privacy/domains

        //TODO: NOT TESTED, requires Pro Account

        #endregion

        #region /videos/related

        [TestMethod]
        public void GetRelatedVideos()
        {
            var result = _videosApi.GetRelatedVideos(POPULAR_CLIP_ID, null, null);

        }

        #endregion

        [TestCleanup]
        public void Cleanup()
        {
            _httpSimulator.Dispose();
        }
    }
}
