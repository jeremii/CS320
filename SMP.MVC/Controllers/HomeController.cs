using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
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
using SMP.MVC.RSSFeed;

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

            ViewBag.User = user;
            ViewBag.RSS = new List<Rss>(); 
            //await getAggregateFeeds(user.Id);
            ViewBag.RssList = await _webApiCalls.GetRss(user.Id);

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
        [HttpGet]
        public async Task<IActionResult> RSSFeed()
        {
            User user = await UserManager.GetUserAsync(HttpContext.User);

            return View( await getAggregateFeeds(user.Id));
        }
        public async Task<List<AggregateFeed>> getAggregateFeeds(string userId)
        {
            IList<Rss> rssFeeds = await _webApiCalls.GetRss(userId);
            List<Feed> feeds = new List<Feed>();
            foreach (Rss rssFeed in rssFeeds)
            {
                Console.WriteLine("RSS : " + rssFeed.Url);
                Feed feed = await getFeed(rssFeed.Url);
                feed.Link = ConvertLinkToPublication(feed.Link);
                feeds.Add(feed);
            }
            List<AggregateFeed> aggFeed = CombineFeedsAndSort(feeds);
            return aggFeed;
        }
        public List<AggregateFeed> CombineFeedsAndSort(List<Feed> feeds)
        {
            List<AggregateFeed> aggList = new List<AggregateFeed>();

            foreach(Feed feed in feeds)
            {
                foreach(FeedItem item in feed.Items )
                {
                    AggregateFeed aggItem = new AggregateFeed();
                    aggItem.FeedLink = ConvertLinkToPublication(feed.Link);
                    aggItem.FeedTitle = feed.Title;
                    aggItem.Author = item.Author;
                    aggItem.Categories = item.Categories;
                    aggItem.Content = item.Content;
                    aggItem.Description = item.Description;
                    aggItem.Id = item.Id;
                    aggItem.Link = item.Link;
                    aggItem.PublishingDate = item.PublishingDate;
                    aggItem.PublishingDateString = item.PublishingDateString;
                    aggItem.SpecificItem = item.SpecificItem;
                    aggItem.Title = item.Title;
                    aggList.Add(aggItem);
                }
            }
            aggList = aggList.OrderByDescending(x => x.PublishingDate).ToList();


            return aggList;
        }
        public string ConvertLinkToPublication( string feedlink )
        {
            return Regex.Replace(feedlink, "(https?\\:\\/\\/)?(www\\.)?", "");
        }
        private async Task<Feed> getFeed(string url )
        {
            IEnumerable<HtmlFeedLink> urls = await FeedReader.GetFeedUrlsFromUrlAsync(url);

            string feedUrl;
            if (urls == null || urls.Count() < 1)
                feedUrl = url;
            else if (urls.Count() == 1)
                feedUrl = urls.First().Url;
            else if (urls.Count() == 2) // if 2 urls, then its usually a feed and a comments feed, so take the first per default
                feedUrl = urls.First().Url;
            else
            {
                int i = 1;
                Console.WriteLine("Found multiple feed, please choose:");
                foreach (var feedurl in urls)
                {
                    Console.WriteLine($"{i++.ToString()} - {feedurl.Title} - {feedurl.Url}");
                }
                var input = Console.ReadLine();

                if (!int.TryParse(input, out int index) || index < 1 || index > urls.Count())
                {
                    Console.WriteLine("Wrong input. Press key to exit");
                    Console.ReadKey();
                    return null;
                }
                feedUrl = urls.ElementAt(index).Url;
            }

            return await FeedReader.ReadAsync(feedUrl);
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
