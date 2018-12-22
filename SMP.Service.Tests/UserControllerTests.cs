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

        TestHelper helper = new TestHelper();

        [Fact]
        public async void GetAllUsers_ShouldGetAllUsers()
        {
            string route = "/All";
            var response = await TestHelper.GetRouteSuccessful(route);
            var users = TestHelper.DeserializeResponseList(new User(), response);
            Assert.Equal(35, users.Count);
        }
        [Fact]
        public async void GetAllUsers2_ShouldGetAllUsersPlusFollowStatus()
        {
            string route = "/FindIdByName/Joe/Schmoe";
            string content = await TestHelper.GetRouteSuccessful(route);
            string myId = content;

            route = "/All/" + myId;
            var response = await TestHelper.GetRouteSuccessful(route);

            var users = TestHelper.DeserializeResponseList(new UserFollowViewModel(), response);
            Assert.Equal(35, users.Count);
        }
        [Fact]
        public async void GetUser_ShouldGetUserOverview()
        {
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"/FindIdByName/{firstName}/{lastName}";
            string content = await TestHelper.GetRouteSuccessful(route);

            string myId = content;

            route = "/"+myId;
            content = await TestHelper.GetRouteSuccessful(route);

            var result = TestHelper.DeserializeResponse(new UserOverviewViewModel(), content);
            Assert.Equal(firstName+" "+lastName, result.FullName);
        }
        [Fact]
        public async void SearchUser_ShouldFindUsers()
        {
            string first = "Joe";
            string last = "Schmoe";
            string route = $"/FindIdByName/{first}/{last}";
            string content = await TestHelper.GetRouteSuccessful(route);

            // Any name with the vowel "a" in it
            string searchTerm = "a";

            string myId = content;
            route = $"/Search/{myId}/{searchTerm}";
            content = await TestHelper.GetRouteSuccessful(route);
        }

    }
}
