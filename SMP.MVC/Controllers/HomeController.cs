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
using SMP.Models.ViewModels.HomeViewModels;
using SMP.Models;
using SMP.Service.Controllers;
using SMP.MVC.WebServiceAccess.Base;
using System.Security.Claims;

namespace SMP.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;

        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
        //public IEmailSender EmailSender { get; }

        public HomeController(IWebApiCalls webApiCalls, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _webApiCalls = webApiCalls;
            UserManager = userManager;
            SignInManager = signInManager;
            //EmailSender = emailSender;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        [Route("~/")]
        public async Task<IActionResult> Index()
        {
            if (!SignInManager.IsSignedIn(User)) return RedirectToAction("Login");

            User user = await UserManager.GetUserAsync(HttpContext.User);
            Console.WriteLine("USER ID: " + user.Id);
            IList<UserPostViewModel> posts = await _webApiCalls.GetFollowingPostsAsync(user.Id);

            return View(posts);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFollowingPosts(string userId, [FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            return Ok(await _webApiCalls.GetFollowingPostsAsync(userId));
        }
        [HttpGet]
        public async Task<IActionResult> Search( [FromQuery] string search )
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);
            IList<UserFollowViewModel> results = await _webApiCalls.SearchAsync(user.Id, search);
            ViewBag.User = user;

            return View(results);
        }
        [HttpGet]
        public async Task<IActionResult> UserDirectory()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);
            ViewBag.User = user;

            IList<UserFollowViewModel> results = await _webApiCalls.GetAllUsers(user.Id);

            return View(results);
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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            User user = await UserManager.FindByEmailAsync(model.Email);

            if (user == null) return View(model);

            var result = await SignInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (!result.Succeeded) return View(model);

            return RedirectToAction( "Index" );
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //return View(model);
            }

            if (model.Password != model.ConfirmPassword) return View(model);

            User user = new User()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName
            };

            var result = await UserManager.CreateAsync(user, model.Password);

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            if (!result.Succeeded)
            {
                Console.WriteLine("RESULT DIDN'T SUCCEED!");
                return View(model);
            }
            //var user = await UserManager.FindByEmailAsync(model.Email);
            var signin = await SignInManager.PasswordSignInAsync(user, model.Password, true, lockoutOnFailure: false);

            if (!signin.Succeeded)
            {
                Console.WriteLine("SIGNIN DIDN'T SUCCEED!");
                return View(model);
            }

            //return View(model);
            return RedirectToAction( "Index" );
        }
    }
}
