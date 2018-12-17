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
        public async void ShouldGetAllUsersPlusFollowStatus()
        {
            string route = "/FindIdByName/Joe/Schmoe";
            string content = await RouteSuccessful(route);
            Console.WriteLine("ASSERT TRUE: " + route);
            string myId = content;

            route = "/All/" + myId;
            Console.WriteLine("ASSERT TRUE Follows of user 2: " + route);
            var response = await RouteSuccessful(route);

            var users = DeserializeResponseList(new UserFollowViewModel(), response);
            Assert.Equal(35, users.Count);
        }
        [Fact]
        public async void ShouldGetUserOverview()
        {
            string firstName = "Joe";
            string lastName = "Schmoe";
            string route = $"/FindIdByName/{firstName}/{lastName}";
            string content = await RouteSuccessful(route);
            Console.WriteLine("ASSERT TRUE: " + route);

            string myId = content;

            route = "/"+myId;
            Console.WriteLine("ASSERT TRUE user overview 2: " + route);
            content = await RouteSuccessful(route);

            var result = DeserializeResponse(new UserOverviewViewModel(), content);
            Assert.Equal(firstName+" "+lastName, result.FullName);
        }
        [Fact]
        public async void ShouldFindUsers()
        {
            string first = "Joe";
            string last = "Schmoe";
            string route = $"/FindIdByName/{first}/{last}";
            string content = await RouteSuccessful(route);

            string searchTerm = "a";

            string myId = content;
            route = $"/Search/{myId}/{searchTerm}";
            content = await RouteSuccessful(route);
            var result = DeserializeResponseList(new UserFollowViewModel(), content);
            Assert.NotEmpty(result);
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
            Console.WriteLine("ASSERT TRUE: " + route);
            Assert.True(response.IsSuccessStatusCode);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
