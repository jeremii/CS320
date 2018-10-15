using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMP.DAL.Repos.Interfaces;
using SMP.DAL.Repos;
using SMP.Models.Entities;
//using SMP.Models.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace SMP.Service.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserRepo Repo { get; set; }
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public UserController(IUserRepo repo, UserManager<User> manager, SignInManager<User> manager2)
        {
            Repo = repo;
            userManager = manager;
            signInManager = manager2;
        }


        [HttpGet("{id}")]
        public IActionResult Index(string id)
        {
            var item = Repo.GetUser(id);
            if (item == null)
            {
                return NotFound();
            }
            return Json(item);
        }

        //http://localhost:40001/api/[controller]/update/0
        [HttpPut("Update/{id}")]
        public IActionResult Update(string id, [FromBody] User item)
        {
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Update(item);

            return CreatedAtRoute("GetAllUsers", null, null);
        }
        [HttpPost("{password}")]
        public async Task<IActionResult> Create(string password, [FromBody] User user)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return Created($"api/User/Get/{user.Id}", user);
            }

            return NotFound();
        }
        //http://localhost:40001/api/[controller]/delete/0
        //[HttpDelete("Delete/{id}")]
        //public IActionResult Delete(string id)
        //{
        //    User item = Repo.Find(id);
        //    if (item == null || item.Id != id || !ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    Repo.Delete(item);

        //    return CreatedAtRoute("GetAllUsers", null, null);
        //}
        //http://localhost:40001/api/[controller]/
        [HttpGet("", Name = "GetAllUsers")]
        public IActionResult GetAll()
        {
            var data = Repo.GetAll();
            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }

    }
}
