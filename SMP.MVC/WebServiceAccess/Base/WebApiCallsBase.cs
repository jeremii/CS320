using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SMP.MVC.Configuration;
using SMP.Models.Entities;
using SMP.Models.ViewModels;

namespace SMP.MVC.WebServiceAccess.Base
{
    public abstract class WebApiCallsBase
    {
        protected readonly string ServiceAddress;
        protected readonly string UserBaseUri;
        protected readonly string PostBaseUri;
        protected readonly string FollowBaseUri;
        protected readonly string SearchUserUri;
        protected readonly string FollowerUri;
        protected readonly string FollowingUri;
        protected readonly string FollowingPostsUri;
        protected readonly string LoginUri;
        protected readonly string LogoutUri;
        protected readonly string IsFollowingUri;
        protected readonly string RssUri;
        protected readonly string MessageUri;

        protected WebApiCallsBase(IWebServiceLocator settings)
        {
            ServiceAddress = settings.ServiceAddress+"api/";

            // User
            UserBaseUri = $"{ServiceAddress}User/";

            LoginUri = $"{UserBaseUri}Login/";
            LogoutUri = $"{UserBaseUri}Logout/";

            SearchUserUri = $"{UserBaseUri}Search/";

            // Post
            PostBaseUri = $"{ServiceAddress}Post/";
            FollowingPostsUri = $"{ServiceAddress}Post/Following/";

            // Follow
            FollowBaseUri = $"{ServiceAddress}Follow/";
            FollowerUri = $"{FollowBaseUri}Followers/";
            FollowingUri = $"{FollowBaseUri}Following/";
            IsFollowingUri = $"{FollowBaseUri}IsFollowing/";

            // Rss
            RssUri = $"{ServiceAddress}Rss/";

            // Message
            MessageUri = $"{ServiceAddress}Message/";

        }
        public string GetUri<T>(T item)
        {
            string uri = "";

            if (item is User || item is IList<User>)
                uri = UserBaseUri;
            else if (item is Post || item is IList<Post>)
                uri = PostBaseUri;
            else if (item is Follow || item is IList<Follow>)
                uri = FollowBaseUri;
            else if (item is Rss || item is IList<Rss>)
                uri = RssUri;
            else if (item is Message || item is IList<Message>)
                uri = MessageUri;
            else if (item is MessageInboxViewModel || item is IList<MessageInboxViewModel>)
                uri = MessageUri;
            else if (item is MessageViewModel || item is IList<MessageViewModel>)
                uri = MessageUri;
            else if (item is UserOverviewViewModel || item is IList<UserOverviewViewModel>)
                uri = UserBaseUri;
            else if (item is UserPostViewModel || item is IList<UserPostViewModel>)
                uri = PostBaseUri;

            return uri;
        }
        public async Task<string> GetJsonFromGetResponseAsync(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"The Call to {uri} failed.  Status code: {response.StatusCode}");
                    }
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }

        }
        internal async Task<T> GetPrimitiveItemAsync<T>(string uri)
            where T : new()
        {
            try
            {
                var json = await GetJsonFromGetResponseAsync(uri);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }
        }
        internal async Task<T> GetItemAsync<T>(string uri)
            where T : class, new()
        {
            try
            {
                var json = await GetJsonFromGetResponseAsync(uri);
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }
        }
        internal async Task<IList<T>> GetItemListAsync<T>(string uri)
            where T : class, new()
        {
            Console.WriteLine(uri);
            try
            {
                return JsonConvert.DeserializeObject<IList<T>>(await GetJsonFromGetResponseAsync(uri));
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }
        }

        protected static async Task<string> ExecuteRequestAndProcessResponse(
            string uri, Task<HttpResponseMessage> task)
        {
            try
            {
                var response = await task;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"The Call to {uri} failed.  Status code: {response.StatusCode}");
                }
                //return response.Headers.Location.AbsoluteUri;
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }
        }

        protected StringContent CreateStringContent(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected async Task<string> SubmitPostRequestAsync(string uri, string json)
        {
            using (var client = new HttpClient())
            {
                var task = client.PostAsync(uri, CreateStringContent(json));
                return await ExecuteRequestAndProcessResponse(uri, task);
            }
        }

        protected async Task<string> SubmitPutRequestAsync(string uri, string json)
        {
            Console.WriteLine(uri);
            Console.WriteLine(json);
            using (var client = new HttpClient())
            {
                Task<HttpResponseMessage> task = client.PutAsync(uri, CreateStringContent(json));
                return await ExecuteRequestAndProcessResponse(uri, task);
            }
        }
        protected async Task SubmitDeleteRequestAsync(string uri)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    Task<HttpResponseMessage> deleteAsync = client.DeleteAsync(uri);
                    var response = await deleteAsync;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception(response.StatusCode.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //Do something intelligent here
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}