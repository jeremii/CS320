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
    public class TestHelper
    {
        public TestHelper()
        {
        }
        public static List<T> DeserializeResponseList<T>(T item, string response) where T : class, new()
        {
            return JsonConvert.DeserializeObject<List<T>>(response);
        }
        public static T DeserializeResponse<T>(T item, string response) where T : class, new()
        {
            return JsonConvert.DeserializeObject<T>(response);
        }
        public static async Task<string> GetRouteSuccessful(string route)
        {
            HttpResponseMessage response = await new HttpClient().GetAsync($"{BaseTests.ServiceAddress}{route}");
            Assert.True(response.IsSuccessStatusCode);
            return await response.Content.ReadAsStringAsync();
        }
        public static async Task PutRouteSuccessful( string route, ByteArrayContent content)
        {
            HttpResponseMessage response = await new HttpClient().PutAsync($"{BaseTests.ServiceAddress}{route}", content);
            Assert.Equal(200, response.StatusCode.GetHashCode());
        }
        public static async Task PostRouteSuccessful(string route, ByteArrayContent content)
        {
            HttpResponseMessage response = await new HttpClient().PostAsync($"{BaseTests.ServiceAddress}{route}", content);
            Assert.True(response.IsSuccessStatusCode);
        }
        public static async Task DeleteRouteSuccessful(string route)
        {
            HttpResponseMessage response = await new HttpClient().DeleteAsync($"{BaseTests.ServiceAddress}{route}");
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
