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
    public class UserControllerTests : BaseTests
    {

        public UserControllerTests()
        {
            RootAddress = "api/User";
        }

        [Fact]
        public async void GetAllUsers_ShouldGetAllUsers()
        {
            string route = $"{RootAddress}/All";
            var response = await TestHelper.GetRouteSuccessful(route);
            var users = TestHelper.DeserializeResponseList(new User(), response);
            Assert.Equal(35, users.Count);
        }
        [Fact]
        public async void GetAllUsers2_ShouldGetAllUsersPlusFollowStatus()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);

            string route = $"{RootAddress}/All/{myId}";
            var response = await TestHelper.GetRouteSuccessful(route);

            var users = TestHelper.DeserializeResponseList(new UserFollowViewModel(), response);
            Assert.Equal(35, users.Count);
        }
        [Fact]
        public async void GetUser_ShouldGetUserOverview()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);

            string route = $"{RootAddress}/{myId}";
            string content = await TestHelper.GetRouteSuccessful(route);

            var result = TestHelper.DeserializeResponse(new UserOverviewViewModel(), content);
            Assert.Equal(firstName+" "+lastName, result.FullName);
        }
        [Fact]
        public async void SearchUser_ShouldFindUsers()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);

            // Any name with the vowel "a" in it
            string searchTerm = "a";

            string route = $"{RootAddress}/Search/{myId}/{searchTerm}";
            string content = await TestHelper.GetRouteSuccessful(route);
        }

    }
}
