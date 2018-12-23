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
    public class PostControllerTests : BaseTests
    {
        public PostControllerTests()
        {
            RootAddress = "api/Post";
        }
        [Fact]
        public async void GetAllPostsOfUser_ShouldGetAllPostsOfUser()
        {
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string content = await TestHelper.GetRouteSuccessful(route);

            string myId = content;

            route = $"{RootAddress}/{myId}";
            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new Post(), response);
            Assert.IsType(typeof(List<Post>), result);
        }
        [Fact]
        public async void GetFollowingPosts_ShouldGetAllPostsUserIsFollowing()
        {
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string content = await TestHelper.GetRouteSuccessful(route);

            string myId = content;

            route = $"{RootAddress}/Following/{myId}";
            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new UserPostViewModel(), response);
            Assert.Equal(96, result.Count);

        }
        [Fact]
        public async void GetAllPosts_ShouldGetAllPosts()
        {
            string route = $"{RootAddress}";
            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new Post(), response);
            Assert.IsType(typeof(List<Post>), result);
        }
        [Fact]
        public async void CreatePost_ShouldCreateAPost()
        {
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"api/User/FindIdByName/{firstName}/{lastName}";
            string myId = await TestHelper.GetRouteSuccessful(route);

            Post obj = new Post()
            {
                Text = "test",
                UserId = myId,
                Time = DateTime.Now
            };

            route = $"{RootAddress}/Create";

            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await TestHelper.PostRouteSuccessful(route, content);
        }
    }
}
