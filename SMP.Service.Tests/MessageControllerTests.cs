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
    public class MessageControllerTests : BaseTests
    {
        public MessageControllerTests()
        {
            RootAddress = "api/Message";
        }
        [Fact]
        public async void GetAllMessages_ShouldGetAllMessage()
        {
            string route = $"{RootAddress}";

            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new Message(), response);

            Assert.NotEqual(0, result.Count);
        }
        [Fact]
        public async void GetThread_ShouldGetThreadBetweenTwoUsers()
        {

            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);
            string userId = await TestHelper.GetRouteSuccessful(FindIdRoute2);

            string route = $"{RootAddress}/{myId}/{userId}";

            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new MessageViewModel(), response);

            Assert.NotNull(result[0].ReceiverId);
        }
        [Fact]
        public async void GetInbox_ShouldGetAllThreadForUser()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);

            string route = $"{RootAddress}/{myId}";

            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new MessageInboxViewModel(), response);

            Assert.NotNull(result[0].UserId);
        }
        [Fact]
        public async void CreateMessage_ShouldCreateAMessage()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);
            string userId = await TestHelper.GetRouteSuccessful(FindIdRoute2);

            // Now, Create follow
            string route = $"{RootAddress}/Create";
            Message obj = new Message()
            {
                SenderId = userId,
                ReceiverId = myId,
                Text = "Test message."
            };
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            var buffer = System.Text.Encoding.UTF8.GetBytes(json);
            var content = new ByteArrayContent(buffer);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await TestHelper.PostRouteSuccessful(route, content);
        }

        [Fact]
        public async void GetAllMessages_ShouldGetAllMessageRecords()
        {
            string myId = await TestHelper.GetRouteSuccessful(FindIdRoute);

            string route = $"{RootAddress}";

            var response = await TestHelper.GetRouteSuccessful(route);
            var result = TestHelper.DeserializeResponseList(new Message(), response);

            Assert.NotNull(result[0].SenderId);
        }
    }
}
