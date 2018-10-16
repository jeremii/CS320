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
    public class FollowController : Controller
    {
        private IFollowRepo Repo { get; set; }
        public FollowController(IFollowRepo repo)
        {
            Repo = repo;
        }
        [HttpGet("IsFollowing/{userId}/{followerId}")]
        public async Task<IActionResult> IsFollowing(string userId, string followerId )
        {
            bool data = await Repo.IsFollowingAsync(userId, followerId);
            return new ObjectResult(data);
        }
        //http://localhost:40001/api/[controller]/Followers/[user id]/
        [HttpGet("Followers/{id}/{myId}", Name = "GetAllFollowersOfUser2")]
        public IActionResult GetFollowers(string id, string myId)
        {
            var data = Repo.GetFollowers(id, myId);

            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }
        //http://localhost:40001/api/[controller]/[user id]/
        [HttpGet("Following/{id}/{myId}", Name = "GetAllFollowingsOfUser")]
        public IActionResult GetFollowing(string id, string myId)
        {
            var data = Repo.GetFollowing(id, myId);

            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }
        [HttpPost("Create/{userId}/{followId}")]
        public async Task<IActionResult> CreateFollow(string userId, string followId)
        {
            Follow item = new Follow()
            {
                UserId = userId,
                FollowerId = followId
            };
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            if(await Repo.IsFollowingAsync(userId, followId))
            {
                return BadRequest();
            }
            Repo.Add(item);

            return CreatedAtRoute("GetAllFollows", null, null);
        }
        [HttpDelete("Delete/{userId}/{followId}")]
        public async Task<IActionResult> DeleteFollow(string userId, string followId)
        {
            if (!await Repo.IsFollowingAsync(userId, followId))
            {
                return BadRequest();
            }

            Follow item = Repo.GetOne(userId, followId);

            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Delete(item);

            return CreatedAtRoute("GetAllFollows", null, null);
        }

        //http://localhost:40001/api/[controller]/create
        [HttpPost("Create")]
        public IActionResult Create([FromBody] Follow item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Add(item);

            return CreatedAtRoute("GetAllFollows", null, null);
        }
        //http://localhost:40001/api/[controller]/update/0
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Follow item)
        {
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Update(item);

            return CreatedAtRoute("GetAllFollows", null, null);
        }
        //http://localhost:40001/api/[controller]/delete/0
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            Follow item = Repo.Find(id);
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Repo.Delete(item);

            return CreatedAtRoute("GetAllFollows", null, null);
        }
        //http://localhost:40001/api/[controller]/
        [HttpGet("", Name = "GetAllFollows")]
        public IActionResult GetAll()
        {
            var data = Repo.GetAll();
            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }

    }
}
