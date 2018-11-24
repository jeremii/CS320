using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMP.DAL.Repos.Interfaces;
using SMP.DAL.Repos;
using SMP.Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SMP.Service.Controllers
{
    [Route("api/[controller]")]
    public class MessageController : Controller
    {
        private IMessageRepo Repo { get; set; }
        //private UserManager<User> userManager;
        //private SignInManager<User> signInManager;

        public MessageController(IMessageRepo repo)
        {
            Repo = repo;
        }

        // GET: api/values
        [HttpGet("{userId}/{oppositeUserId}")]
        public IActionResult GetThread( string userId, string oppositeUserId)
        {
            var data = Repo.GetThread(userId, oppositeUserId);

            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }

        // GET api/values/5
        [HttpGet("{userId}")]
        public IActionResult GetInbox(string userId)
        {
            var data = Repo.GetInbox(userId);

            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }

        // POST api/values
        [HttpPost("Create")]
        public IActionResult Create([FromBody]Message item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            item.Time = DateTime.Now;

            Repo.Add(item);

            return CreatedAtRoute("GetAllMessages", null, null);
        }
        [HttpGet("", Name = "GetAllMessages")]
        public IActionResult GetAll()
        {

            var data = Repo.GetAll();

            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }


    }
}
