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
        public async void ShouldGetAllUsers()
        {
            string route = "/All";
            var response = await RouteSuccessful(route);
            var users = DeserializeResponseList(new User(), response);
            Assert.Equal(35, users.Count);
        }
        [Fact]
        public async void ShouldGetAllFollowsOfUser()
        {
            string route = "/FindIdByName/Joe/Schmoe";
            HttpResponseMessage httpResponse = await new HttpClient().GetAsync($"{ServiceAddress}{RootAddress}{route}");
            Assert.True(httpResponse.IsSuccessStatusCode);
            string myId = httpResponse.Content.ToString();

            route = "/All/" + myId;
            var response = await RouteSuccessful(route);
            users = DeserializeResponseList(new UserFollowViewModel(), response);
            Assert.Equal(35, users.Count);
        }
        [Fact]
        public async void ShouldGetUserOverview()
        {
            string route = "/FindIdByName/Joe/Schmoe";
            HttpResponseMessage httpResponse = await new HttpClient().GetAsync($"{ServiceAddress}{RootAddress}{route}");
            Assert.True(httpResponse.IsSuccessStatusCode);
            string myId = httpResponse.Content.ToString();

            route = myId;

            var response = await RouteSuccessful(route);
            var result = DeserializeResponse(new UserOverviewViewModel(), response);
            Assert.Equal("Joe Schmoe", result.FullName);
        }
        public List<T> DeserializeResponseList<T>( T item, string response) where T : class, new()
        {
            return JsonConvert.DeserializeObject<List<T>>(response);
        }
        public T DeserializeResponse<T>(T item, string response) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(response);
        }
        public async Task<string> RouteSuccessful(string route )
        {
            HttpResponseMessage response = await new HttpClient().GetAsync($"{ServiceAddress}{RootAddress}{route}");
            Assert.True(response.IsSuccessStatusCode);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
