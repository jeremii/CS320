using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using SMP.Models;
using SMP.MVC.WebServiceAccess;
using SMP.MVC.WebServiceAccess.Base;
using System.Security.Claims;

namespace SMP.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }


        public UserController(IWebApiCalls webApiCalls, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _webApiCalls = webApiCalls;
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string id = (await UserManager.GetUserAsync(HttpContext.User)).Id;
            UserOverviewViewModel user = await _webApiCalls.GetOneAsync(new UserOverviewViewModel(), id);
            ViewBag.Posts = await _webApiCalls.GetSomeAsync(new UserPostViewModel(), id);
            ViewBag.UserIsUser = true;
            Console.WriteLine("CURRENT: " + ViewBag.UserIsUser.ToString());

            return View(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            UserOverviewViewModel user = await _webApiCalls.GetOneAsync(new UserOverviewViewModel(), id);
            ViewBag.Posts = await _webApiCalls.GetSomeAsync(new UserPostViewModel(), id);
            string userId = (await UserManager.GetUserAsync(HttpContext.User)).Id;
            ViewBag.UserIsUser = id == userId;
            Console.WriteLine("CURRENT: " + ViewBag.UserIsUser.ToString());

            ViewBag.IsFollowing = await _webApiCalls.IsFollowingAsync(userId, id);
            ViewBag.MyUserId = userId;
            ViewBag.UserId = id;

            return View(user);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            User user = await _webApiCalls.GetOneAsync(new User(), id);
            return View(user);
        }


        [HttpPost("{id}")]
        public async Task<IActionResult> Edit(string id, User user)
        {
            if (!ModelState.IsValid) return View(user.Id);

            var result = await _webApiCalls.UpdateAsync(user.Id, user);

            return View("Index", user);
        }

        public IActionResult Error()
        {
            return View();
        }


    }
}
