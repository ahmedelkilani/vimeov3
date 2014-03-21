using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Subtext.TestLibrary;
using VimeoApi.Api;
using VimeoApi.Tests.Setup;
using VimeoApi.OAuth2.Clients.Impl;
using VimeoApi.Models;

namespace VimeoApi.Tests.Api.Users
{
    [TestClass]
    public class UsersApiTests
    {
        private HttpSimulator _httpSimulator;
        private UsersApi _usersApi;

        [TestInitialize]
        public void Init()
        {
            _httpSimulator = AppStart.SetDependencyResolver();
            _usersApi = AppStart.InitVimeoClient<AuthenticatedViaRedirectVimeoClient>().Users();
        }
        
        #region Test Variables

        private const string USER_ID = "25760851";
        private const string ALBUM_ID = "2774289";
        private const string CHANNEL_ID = "699606";
        private const string GROUP_ID = "235799";
        private const string FOLLOWING_USER_ID = "938175";
        private const string LIKED_CLIP_ID = "87585644";
        private const string WATCHLATER_CLIP_ID = "88836644";

        #endregion

        #region /users

        [TestMethod]
        public void GetUsers()
        {
            var response = _usersApi.GetUsers(new DefaultParametersContext()
            {
                query = "Ahmed El Kilani"
            });

            Assert.AreNotEqual(null, response.data);
            Assert.AreNotEqual(0, response.data.Count);
        }

        [TestMethod]
        public void GetUser()
        {
            var response = _usersApi.GetUser(USER_ID);

            Assert.AreNotEqual(null, response);
        }

        #endregion

        #region /users/albums

        [TestMethod]
        public void GetAlbums()
        {
            var response = _usersApi.GetAlbums(USER_ID, new DefaultParametersContext()
            {
                query = "album"
            });

            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void GetAlbum()
        {
            var response = _usersApi.GetAlbum(ALBUM_ID);

            Assert.AreNotEqual(null, response);
        }

        [TestMethod]
        public void EditAlbum()
        {
            _usersApi.EditAlbum("2774289", new AlbumParameterContext()
            {
                description = "edited desc",
                privacy = AlbumPrivacy.password,
                password = "12345",
                sort = AlbumSortType.added_last,
                title = "edit_title"
            });

        }

        #endregion

        #region /users/appearances

        [TestMethod]
        public void GetUserAppearances()
        {
            var result = _usersApi.GetUserAppearances(new VideoFilterableParametersContext()
            {
                direction = "asc"
            });

            Assert.AreNotEqual(null, result);

        }

        #endregion

        #region /users/channels

        [TestMethod]
        public void GetUserChannels()
        {
            var result = _usersApi.GetUserChannels(new DefaultParametersContext()
            {
                direction = "asc"
            });

            Assert.AreNotEqual(null, result);

        }

        [TestMethod]
        public void IsUserSubscribedToChannel()
        {
            var result = _usersApi.IsUserSubscribedToChannel(USER_ID, CHANNEL_ID);

            Assert.AreEqual(true, result);

        }

        #endregion

        #region /users/groups

        [TestMethod]
        public void GetUserGroups()
        {
            var result = _usersApi.GetUserGroups(USER_ID, null);

            Assert.AreNotEqual(null, result);
            Assert.AreEqual("/groups/" + GROUP_ID, result.data[0].uri);

        }

        [TestMethod]
        public void IsUserJoinedGroup()
        {
            var result = _usersApi.IsUserJoinedGroup(USER_ID, GROUP_ID);

            Assert.AreEqual(true, result);

        }

        [TestMethod]
        public void RemoveUserFromGroup()
        {
            //_usersApi.RemoveUserFromGroup(USER_ID, GROUP_ID);

        }


        #endregion

        #region /users/feed

        [TestMethod]
        public void GetUserFeed()
        {
            var result = _usersApi.GetUserFeed(new FeedParameterContext()
            {
                offset = 5,
                per_page = 5,
            });

            Assert.AreNotEqual(null, result);
            Assert.AreEqual(5, result.data.Count);

        }

        #endregion

        #region /users/followers

        [TestMethod]
        public void GetUserFollowers()
        {
            var result = _usersApi.GetUserFollowers(USER_ID, null);

            Assert.AreNotEqual(null, result);
        }


        #endregion

        #region /users/following

        [TestMethod]
        public void GetUserFollowingList()
        {
            var result = _usersApi.GetUserFollowingList(null);

            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void IsUserFollowingAnotherUser()
        {
            var result = _usersApi.IsUserFollowingAnotherUser(USER_ID, FOLLOWING_USER_ID);

            Assert.AreEqual(true, result);

        }

        [TestMethod]
        public void UnfollowUser()
        {
            // _usersApi.UnfollowUser(USER_ID, FOLLOWING_USER_ID);

        }


        #endregion

        #region /users/likes

        [TestMethod]
        public void GetUserLikes()
        {
            var result = _usersApi.GetUserLikes(USER_ID, null);

            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.data.Count);
        }

        [TestMethod]
        public void DoesUserLikeVideo()
        {
            var result = _usersApi.DoesUserLikeVideo(USER_ID, LIKED_CLIP_ID);

            Assert.AreEqual(true, result);

        }

        #endregion

        #region /users/presets

        [TestMethod]
        public void GetUserPresets()
        {
            var result = _usersApi.GetUserPresets(USER_ID, null);

            Assert.AreNotEqual(null, result);
        }

        //TODO: determine how to add a preset
        [TestMethod]
        public void GetPreset()
        {
            //var result = _usersApi.GetPreset(USER_ID, "");
            //Assert.AreNotEqual(null, result);
        }

        #endregion

        #region /users/videos

        [TestMethod]
        public void DoesUserOwnVideo()
        {
            var result = _usersApi.DoesUserOwnVideo(USER_ID, LIKED_CLIP_ID);

            Assert.AreEqual(false, result);
        }

        #endregion

        #region /users/watchlater

        [TestMethod]
        public void GetUserWatchLaterlist()
        {
            var result = _usersApi.GetUserWatchLaterlist(USER_ID, null);

            Assert.AreNotEqual(null, result);
            Assert.AreNotEqual(0, result.data.Count);
        }

        [TestMethod]
        public void IsVideoInWatchlist()
        {
            var result = _usersApi.IsVideoInWatchlist(USER_ID, WATCHLATER_CLIP_ID);

            Assert.AreEqual(result, result);
        }


        #endregion
        
        #region /users/portfolios
        //TODO: NOT TESTED: Do not have money for PRO account
        #endregion

        #region /users/portfolios/videos
        //TODO: NOT TESTED: Do not have money for PRO account
        #endregion


        [TestCleanup]
        public void Cleanup()
        {
            _httpSimulator.Dispose();
        }
    }
}
