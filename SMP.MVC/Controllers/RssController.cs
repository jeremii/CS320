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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMP.MVC.Controllers
{
    public class RssController : Controller
    {
        private readonly IWebApiCalls _webApiCalls;
        public UserManager<User> UserManager { get; }

        public RssController(IWebApiCalls webApiCalls, UserManager<User> userManager, IHostingEnvironment env)
        {
            _webApiCalls = webApiCalls;
            UserManager = userManager;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Make(Rss model)
        {

            string userId = (await UserManager.GetUserAsync(HttpContext.User)).Id;
            //model.UserId = userId;
            userId = model.UserId;

            Console.WriteLine("RSS CREATE: " + userId);

            if (!ModelState.IsValid) return RedirectToAction("Index", "Home");

            var result = await _webApiCalls.CreateAsync(model);

            Console.WriteLine("RSS CREATED: " + userId);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Remove ( int id )
        {
            await _webApiCalls.DeleteAsync(new Rss(), id);
            return RedirectToAction("Index", "Home");
        }
    }
}
