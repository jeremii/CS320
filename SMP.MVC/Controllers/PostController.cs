using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using SMP.MVC.WebServiceAccess.Base;

namespace SMP.MVC.Controllers
{
    public class PostController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public UserManager<User> UserManager { get; }

        public PostController(IWebApiCalls webApiCalls, UserManager<User> userManager, IHostingEnvironment env)
        {
            _webApiCalls = webApiCalls;
            UserManager = userManager;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(string userId)
        {
            var posts = await _webApiCalls.GetSomeAsync(new UserPostViewModel(), userId);
            return Ok(posts);
        }
        [HttpPost]
        public async Task<IActionResult> Make(Post model)
        {

            string userId = (await UserManager.GetUserAsync(HttpContext.User)).Id;
            //model.UserId = userId;
            userId = model.UserId;
            model.Time = DateTime.Now;

            Console.WriteLine("POST CREATE: " + userId);

            if (!ModelState.IsValid) return RedirectToAction("Index", "User", new { id = userId });

            var result = await _webApiCalls.CreateAsync(model);

            Console.WriteLine("POST CREATED: " + userId);

            return RedirectToAction("Index", "User", new { id = userId });
        }

        public IActionResult Index()
        {
            return View();
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
        public IActionResult Post()
        {
            ViewData["Message"] = "Your Post.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
