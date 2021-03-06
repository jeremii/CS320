﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMP.DAL.Repos.Interfaces;
using SMP.DAL.Repos;
using SMP.Models.Entities;
using Microsoft.AspNetCore.Identity;
//using SMP.Models.ViewModels;

namespace SMP.Service.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private IPostRepo Repo { get; set; }
        //private UserManager<User> userManager;
        //private SignInManager<User> signInManager;

        public PostController(IPostRepo repo)
        {
            Repo = repo;
        }

        //http://localhost:40001/api/[controller]/[user id]/
        [HttpGet("{id}", Name = "GetPostsOfUser")]
        public IActionResult GetAllFollowersOfUser(string id)
        {
            var data = Repo.GetPostsOfUser(id);

            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }
        //http://localhost:40001/api/[controller]/Following/[user id]/
        [HttpGet("Following/{userId}")]
        public IActionResult GetFollowingPosts(string userId)
        {
            var data = Repo.GetFollowingPosts(userId);

            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }

        //http://localhost:40001/api/[controller]/create
        [HttpPost("Create")]
        public IActionResult Create([FromBody] Post item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Add(item);

            return CreatedAtRoute("GetAllPosts", null, null);
        }
        //http://localhost:40001/api/[controller]/
        [HttpGet("", Name = "GetAllPosts")]
        public IActionResult GetAll()
        {
            //if (!SignInManager.IsSignedIn(User)) return RedirectToRoute("~/Account/Login");

            //var user = await UserManager.GetUserAsync(User);

            var data = Repo.GetAll();
            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }

    }
}
