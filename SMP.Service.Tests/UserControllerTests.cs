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
            string route = "/All";
            var response = await GetRouteSuccessful(route);
            var users = DeserializeResponseList(new User(), response);
            Assert.Equal(35, users.Count);
        }
        [Fact]
        public async void GetAllUsers2_ShouldGetAllUsersPlusFollowStatus()
        {
            string route = "/FindIdByName/Joe/Schmoe";
            string content = await GetRouteSuccessful(route);
            string myId = content;

            route = "/All/" + myId;
            var response = await GetRouteSuccessful(route);

            var users = DeserializeResponseList(new UserFollowViewModel(), response);
            Assert.Equal(35, users.Count);
        }
        [Fact]
        public async void GetUser_ShouldGetUserOverview()
        {
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"/FindIdByName/{firstName}/{lastName}";
            string content = await GetRouteSuccessful(route);

            string myId = content;

            route = "/"+myId;
            content = await GetRouteSuccessful(route);

            var result = DeserializeResponse(new UserOverviewViewModel(), content);
            Assert.Equal(firstName+" "+lastName, result.FullName);
        }
        [Fact]
        public async void SearchUser_ShouldFindUsers()
        {
            string first = "Joe";
            string last = "Schmoe";
            string route = $"/FindIdByName/{first}/{last}";
            string content = await GetRouteSuccessful(route);

            // Any name with the vowel "a" in it
            string searchTerm = "a";

            string myId = content;
            route = $"/Search/{myId}/{searchTerm}";
            content = await GetRouteSuccessful(route);
        }

        public List<T> DeserializeResponseList<T>( T item, string response) where T : class, new()
        {
            return JsonConvert.DeserializeObject<List<T>>(response);
        }
        public T DeserializeResponse<T>(T item, string response) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(response);
        }
        public async Task PutRouteSuccessful(string route, ByteArrayContent content)
        {
            HttpResponseMessage response = await new HttpClient().PutAsync($"{ServiceAddress}{RootAddress}{route}", content);
            Assert.Equal(200, response.StatusCode.GetHashCode());
        }
        public async Task<string> GetRouteSuccessful(string route )
        {
            HttpResponseMessage response = await new HttpClient().GetAsync($"{ServiceAddress}{RootAddress}{route}");
            Assert.True(response.IsSuccessStatusCode);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
