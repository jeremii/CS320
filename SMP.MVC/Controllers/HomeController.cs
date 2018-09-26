using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using SMP.Models;
using SMP.Service.Controllers;
using SMP.MVC.WebServiceAccess.Base;

namespace SMP.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;

        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
        public IEmailSender EmailSender { get; }

        public HomeController(IWebApiCalls webApiCalls, UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _webApiCalls = webApiCalls;
            UserManager = userManager;
            SignInManager = signInManager;
            EmailSender = emailSender;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        [Route("~/")]
        public async Task<IActionResult> Index()
        {
            if (!SignInManager.IsSignedIn(User)) return RedirectToRoute("~/Account/Login");

            var user = await UserManager.GetUserAsync(User);
            IList<UserPostViewModel> posts = await _webApiCalls.GetFollowingPostsAsync(user.Id);

            return View(posts);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFollowingPosts(string userId, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return Ok(await _webApiCalls.GetFollowingPostsAsync(userId));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult You()
        {
            ViewData["Message"] = "Your Profile.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
