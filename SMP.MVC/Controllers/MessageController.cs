using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using SMP.Models;
using SMP.MVC.WebServiceAccess;
using SMP.MVC.WebServiceAccess.Base;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMP.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class MessageController : Controller
    {

        private readonly IWebApiCalls _webApiCalls;
        public UserManager<User> UserManager { get; }

        public MessageController(IWebApiCalls webApiCalls, UserManager<User> userManager)
        {
            _webApiCalls = webApiCalls;
            UserManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Inbox()
        {
            string userId = (await UserManager.GetUserAsync(HttpContext.User)).Id;
            IList<MessageInboxViewModel> inbox = await _webApiCalls.GetSomeAsync(new MessageInboxViewModel(), userId);
            ViewBag.UserId = userId;
            return View(inbox);
        }
        [HttpGet("{oppositeUserId}")]
        public async Task<IActionResult> Thread(string oppositeUserId)
        {
            string userId = (await UserManager.GetUserAsync(HttpContext.User)).Id;

            IList<MessageViewModel> messages = await _webApiCalls.GetSomeAsync(new MessageViewModel(), userId+"/"+oppositeUserId);

            ViewBag.Messages = messages;

            ViewBag.UserId = userId;

            Message message = new Message()
            {
                SenderId = userId,
                ReceiverId = oppositeUserId,
            };

            return View(message);
        }
        [HttpPost]
        public async Task<IActionResult> Make(Message model)
        {
            string SenderId = (await UserManager.GetUserAsync(HttpContext.User)).Id;
            //model.UserId = userId;
            SenderId = model.SenderId;

            Console.WriteLine("MESSAGE CREATE: " + SenderId);

            if (!ModelState.IsValid) return RedirectToAction("Inbox", "Message");

            var result = await _webApiCalls.CreateAsync(model);

            Console.WriteLine("MESSAGE CREATED: " + SenderId);

            return RedirectToAction("Thread", "Message", new { oppositeUserId = model.ReceiverId });
        }
    }
}
