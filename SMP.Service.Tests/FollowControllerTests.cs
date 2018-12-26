using System;
using Xunit;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using SMP.Service.Tests.Base;
using System.Threading.Tasks;
using SMP.DAL.Initializers;
using System.Text;
using System.Net.Http.Headers;

namespace SMP.Service.Tests
{
    [Collection("Service Testing")]
    public class FollowControllerTests : BaseTests
    {
        public FollowControllerTests()
        {
            RootAddress = "api/Follow";
        }
        [Fact]
        public async void GetAllFollowRecords_ShouldGetAllFollows()
        {

            string route = $"{RootAddress}";

            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new Follow(), response);

            Assert.NotNull(result);
        }
        [Fact]
        public async void GetIsFollowing_ShouldGetFollowingStatus()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);
            string userId = await TestHelper.GetRouteSuccessful(FindIdRoute2);

            string route = $"{RootAddress}/IsFollowing/{myId}/{userId}";
            var response = await TestHelper.GetRouteSuccessful(route);
            bool result = Convert.ToBoolean(response);

            Assert.NotNull(result);
        }
        [Fact]
        public async void GetFollowersOfUser_ShouldGetAllFollowersAndFollowStatusOfUser()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);

            //firstName = "Johnny";
            //lastName = "Basic";
            //route = $"api/User/FindIdByName/{firstName2}/{lastName2}";
            //string userId = await TestHelper.GetRouteSuccessful(route);

            string route = $"{RootAddress}/Followers/{myId}/{myId}";

            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new UserFollowViewModel(), response);
            Assert.IsType(typeof(List<UserFollowViewModel>), result);
            Assert.NotNull(result);
        }
        [Fact]
        public async void GetFollowingsOfUser_ShouldGetAllFollowingsAndFollowStatusOfUser()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);
            //string userId = await TestHelper.GetRouteSuccessful(FindIdRoute2);

            string route = $"{RootAddress}/Following/{myId}/{myId}";
            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new UserFollowViewModel(), response);
            Assert.IsType(typeof(List<UserFollowViewModel>), result);
            Assert.NotNull(result);
        }
        [Fact]
        public async void CreateFollow_ShouldCreateAFollow()
        {
        
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);
            string userId = await TestHelper.GetRouteSuccessful(FindIdRoute2);

            // If already following, delete follow first
            string route = $"{RootAddress}/IsFollowing/{userId}/{myId}";
            var response = await TestHelper.GetRouteSuccessful(route);
            bool result = Convert.ToBoolean(response);

            if( result )
            {
                route = $"{RootAddress}/Delete/{userId}/{myId}";
                await TestHelper.DeleteRouteSuccessful(route);
            }

            // Now, Create follow
            route = $"{RootAddress}/Create/{userId}/{myId}";
            Follow obj = new Follow()
            {
                UserId = userId,
                FollowerId = myId
            };
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await TestHelper.PostRouteSuccessful(route, content);
        }
        [Fact]
        public async void DeleteFollow_ShouldDeleteAFollow()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);
            string userId = await TestHelper.GetRouteSuccessful(FindIdRoute2);

            // Create follow first, if not following
            string route = $"{RootAddress}/IsFollowing/{userId}/{myId}";
            var response = await TestHelper.GetRouteSuccessful(route);
            bool result = Convert.ToBoolean(response);

            if (!result)
            {
                route = $"{RootAddress}/Create/{userId}/{myId}";
                Follow obj = new Follow()
                {
                    UserId = userId,
                    FollowerId = myId
                };
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var content = new ByteArrayContent(buffer);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                await TestHelper.PostRouteSuccessful(route, content);
            }

            // Now, Delete
            route = $"{RootAddress}/Delete/{userId}/{myId}";
            await TestHelper.DeleteRouteSuccessful(route);
        }

        [Fact]
        public async void CreateFollowWithoutIds_ShouldCreateAFollow()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);
            string userId = await TestHelper.GetRouteSuccessful(FindIdRoute2);

            // If already following, delete follow first
            string route = $"{RootAddress}/IsFollowing/{userId}/{myId}";
            var response = await TestHelper.GetRouteSuccessful(route);
            bool result = Convert.ToBoolean(response);

            if (result)
            {
                route = $"{RootAddress}/Delete/{userId}/{myId}";
                await TestHelper.DeleteRouteSuccessful(route);
            }

            // Now, Create follow
            route = $"{RootAddress}/Create";
            Follow obj = new Follow()
            {
                UserId = userId,
                FollowerId = myId
            };
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await TestHelper.PostRouteSuccessful(route, content);
        }
    }
}