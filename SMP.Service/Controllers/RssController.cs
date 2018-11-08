using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMP.DAL.Repos.Interfaces;
using SMP.DAL.Repos;
using SMP.Models.Entities;
//using SMP.Models.ViewModels;

namespace SMP.Service.Controllers
{
    [Route("api/[controller]")]
    public class RssController : Controller
    {
        private IRssRepo Repo { get; set; }

        public RssController(IRssRepo repo)
        {
            Repo = repo;
        }

        //http://localhost:40001/api/[controller]/[user id]/
        [HttpGet("{userId}", Name = "GetRssOfUser")]
        public IActionResult GetRssOfUser(string userId)
        {
            var data = Repo.GetRssOfUser(userId);

            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }

        //http://localhost:40001/api/[controller]/create
        [HttpPost("Create")]
        public IActionResult Create([FromBody] Rss item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Add(item);

            return CreatedAtRoute("GetAllRssFeeds", null, null);
        }
        //http://localhost:40001/api/[controller]/delete/0
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var item = Repo.Find(id);
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Delete(item);
            return CreatedAtRoute("GetAllRssFeeds", null, null);
        }
        //http://localhost:40001/api/[controller]/
        [HttpGet("", Name = "GetAllRssFeeds")]
        public IActionResult GetAll()
        {
            //if (!SignInManager.IsSignedIn(User)) return RedirectToRoute("~/Account/Login");

            //var user = await UserManager.GetUserAsync(User);

            var data = Repo.GetAll();
            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }

    }
}
