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


        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            UserOverviewViewModel user = await _webApiCalls.GetOneAsync(new UserOverviewViewModel(), id);
            ViewBag.Posts = await _webApiCalls.GetSomeAsync(new UserPostViewModel(), id);

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


        [HttpGet("{id}")]
        public async Task<IActionResult> Followers(string id)
        {
            ViewData["Title"] = "Followers";
            var followers = await _webApiCalls.GetFollowersAsync(id);
            return View("UserList", followers);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Following(string id)
        {
            ViewData["Title"] = "Following";
            var following = await _webApiCalls.GetFollowingAsync(id);
            return View("UserList", following);
        }


        [HttpPost("{id}/{followerId}")]
        public async Task<IActionResult> ToggleFollow(string id, string followerId)
        {
            bool isFollowing = await _webApiCalls.IsFollowingAsync(id, followerId);

            if (isFollowing)
            {
                await _webApiCalls.Delete2StringIdsAsync(new Follow(), id, followerId);
                return NoContent();
            }
            else
            {
                await _webApiCalls.CreateAsync(new Follow() { UserId = id, FollowerId = followerId });
                return Ok();
            }
        }


        [HttpGet("{id}/{followerId}")]
        public async Task<IActionResult> IsFollowing(string id, string followerId)
        {
            var result = await _webApiCalls.IsFollowingAsync(id, followerId);
            return Ok(result);
        }


        public IActionResult Error()
        {
            return View();
        }


    }
}
