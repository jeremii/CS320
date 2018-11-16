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
            IList<Message> inbox = await _webApiCalls.GetSomeAsync(new Message(), userId);

            return View(inbox);
        }
        [HttpGet("{userId}/{oppositeUserId}")]
        public async Task<IActionResult> Thread(string userId, string oppositeUserId)
        {
            IList<Message> messages = await _webApiCalls.GetSomeAsync(new Message(), userId+"/"+oppositeUserId);

            ViewBag.Messages = messages;

            Message message = new Message()
            {
                SenderId = userId,
                ReceiverId = oppositeUserId
            };

            return View(message);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Message model)
        {
            string SenderId = (await UserManager.GetUserAsync(HttpContext.User)).Id;
            //model.UserId = userId;
            SenderId = model.SenderId;

            model.Time = DateTime.Now;

            Console.WriteLine("POST CREATE: " + SenderId);

            if (!ModelState.IsValid) return RedirectToAction("Inbox", "Message", new { id = userId });

            var result = await _webApiCalls.CreateAsync(model);

            Console.WriteLine("POST CREATED: " + SenderId);

            return RedirectToAction("Inbox", "Message", new { id = SenderId });
        }
    }
}
