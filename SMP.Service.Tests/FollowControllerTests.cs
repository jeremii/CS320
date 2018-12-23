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
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string myId = await TestHelper.GetRouteSuccessful(route);

            firstName = "Johnny";
            lastName = "Basic";
            route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string userId2 = await TestHelper.GetRouteSuccessful(route);

            route = $"{RootAddress}/IsFollowing/{myId}/{userId2}";


            var response = await TestHelper.GetRouteSuccessful(route);
            bool result = Convert.ToBoolean(response);

            Assert.NotNull(result);
        }
        [Fact]
        public async void GetFollowersOfUser_ShouldGetAllFollowersAndFollowStatusOfUser()
        {
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string myId = await TestHelper.GetRouteSuccessful(route);

            //firstName = "Johnny";
            //lastName = "Basic";
            //route = $"api/User/FindIdByName/{firstName}/{lastName}";
            //string userId = await TestHelper.GetRouteSuccessful(route);

            route = $"{RootAddress}/Followers/{myId}/{myId}";


            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new UserFollowViewModel(), response);
            Assert.IsType(typeof(List<UserFollowViewModel>), result);
            Assert.NotNull(result);
        }
        [Fact]
        public async void GetFollowingsOfUser_ShouldGetAllFollowingsAndFollowStatusOfUser()
        {
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string myId = await TestHelper.GetRouteSuccessful(route);

            //firstName = "Johnny";
            //lastName = "Basic";
            //route = $"api/User/FindIdByName/{firstName}/{lastName}";
            //string userId = await TestHelper.GetRouteSuccessful(route);

            route = $"{RootAddress}/Following/{myId}/{myId}";


            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new UserFollowViewModel(), response);
            Assert.IsType(typeof(List<UserFollowViewModel>), result);
            Assert.NotNull(result);
        }
        [Fact]
        public async void CreateFollow_ShouldCreateAFollow()
        {
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string myId = await TestHelper.GetRouteSuccessful(route);

            firstName = "Johnny";
            lastName = "Basic";
            route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string userId = await TestHelper.GetRouteSuccessful(route);

            // If already following, delete follow first
            route = $"{RootAddress}/IsFollowing/{userId}/{myId}";
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
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string myId = await TestHelper.GetRouteSuccessful(route);

            firstName = "Johnny";
            lastName = "Basic";
            route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string userId = await TestHelper.GetRouteSuccessful(route);

            // Create follow first, if not following
            route = $"{RootAddress}/IsFollowing/{userId}/{myId}";
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
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string myId = await TestHelper.GetRouteSuccessful(route);

            firstName = "Johnny";
            lastName = "Basic";
            route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string userId = await TestHelper.GetRouteSuccessful(route);

            // If already following, delete follow first
            route = $"{RootAddress}/IsFollowing/{userId}/{myId}";
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