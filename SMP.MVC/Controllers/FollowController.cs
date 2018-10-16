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
    public class FollowController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }


        public FollowController(IWebApiCalls webApiCalls, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _webApiCalls = webApiCalls;
            UserManager = userManager;
            SignInManager = signInManager;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Followers(string userId )
        {
            User me = await UserManager.GetUserAsync(HttpContext.User);
            ViewBag.Me = me;
            ViewBag.User = await _webApiCalls.GetOneAsync(new UserOverviewViewModel(), userId);


            Console.WriteLine("FOLLOW / FOLLOWERS ACTIVATED! ");

            IEnumerable<UserFollowViewModel> followers = await _webApiCalls.GetFollowersAsync(userId, me.Id);

            return View(followers);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Following(string userId)
        {
            User me = await UserManager.GetUserAsync(HttpContext.User);
            ViewBag.Me = me;
            ViewBag.User = await _webApiCalls.GetOneAsync(new UserOverviewViewModel(), userId);

            IEnumerable<UserFollowViewModel> following = await _webApiCalls.GetFollowingAsync(userId, me.Id);

            return View(following);
        }
        [HttpPost("{userId}/{followId}")]
        public async Task<IActionResult> UnfollowUser( string userId, string followId )
        {
            await _webApiCalls.UnfollowUser(userId, followId);
            return RedirectToAction("Index", "User", new { id = userId});
        }
        [HttpPost("{userId}/{followId}")]
        public async Task<IActionResult> FollowUser(string userId, string followId)
        {
            await _webApiCalls.FollowUser(userId, followId);
            return RedirectToAction("Index", "User", new { id = userId });
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
            ViewBag.UserIsUser = id == (await UserManager.GetUserAsync(HttpContext.User)).Id;
            Console.WriteLine("CURRENT: " + ViewBag.UserIsUser.ToString());

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
