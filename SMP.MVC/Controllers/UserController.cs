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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace SMP.MVC.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }
        private readonly IHostingEnvironment he;


        public UserController(IWebApiCalls webApiCalls, UserManager<User> userManager, SignInManager<User> signInManager, IHostingEnvironment e)
        {
            _webApiCalls = webApiCalls;
            UserManager = userManager;
            SignInManager = signInManager;
            he = e;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string id = (await UserManager.GetUserAsync(HttpContext.User)).Id;
            UserOverviewViewModel user = await _webApiCalls.GetOneAsync(new UserOverviewViewModel(), id);
            ViewBag.Posts = await _webApiCalls.GetSomeAsync(new UserPostViewModel(), id);
            ViewBag.UserIsUser = true;
            ViewBag.UserId = id;
            Console.WriteLine("CURRENT: " + ViewBag.UserIsUser.ToString());

            return View(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(string id)
        {
            string userId = (await UserManager.GetUserAsync(HttpContext.User)).Id;
            UserOverviewViewModel user = await _webApiCalls.GetOneAsync(new UserOverviewViewModel(), id);
            ViewBag.Posts = await _webApiCalls.GetSomeAsync(new UserPostViewModel(), id);
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
        public async Task<IActionResult> Edit(string id, User item, IFormFile pic)
        {
            //User user = await _webApiCalls.GetOneAsync(new User(), id);
            //User user = new User();
            User user = await UserManager.FindByIdAsync(id);

            if (!ModelState.IsValid) return View(item.Id);

            if(item.FirstName != null )
            {
                user.FirstName = item.FirstName;
            }
            if( item.LastName != null )
            {
                user.LastName = item.LastName;
            }
            if( item.Bio != null )
            {
                user.Bio = item.Bio;
            }
            if (item.EduExp != null)
            {
                user.EduExp = item.EduExp;
            }
            if (item.JobExp != null)
            {
                user.JobExp = item.JobExp;
            }

            if (pic != null)
            {
                string fileName = Path.Combine(he.WebRootPath, Path.GetFileName(pic.FileName));
                pic.CopyTo(new FileStream(fileName, FileMode.Create));
                Console.WriteLine(fileName);
                user.PicturePath = Path.GetFileName(pic.FileName);
            }
            //var result = await _webApiCalls.UpdateAsync(id, user);
            var result = await UserManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }


    }
}
