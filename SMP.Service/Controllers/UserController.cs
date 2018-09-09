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
    public class UserController : Controller
    {
        private IUserRepo Repo { get; set; }
        public UserController(IUserRepo repo)
        {
            Repo = repo;
        }

        //http://localhost:40001/api/user/0
        [HttpGet("{id}", Name = "GetOneUser")]
        public IActionResult GetEmployeesByDeptId(int id)
        {
            var data = Repo.Find(id);
            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }
        //http://localhost:40001/api/user/create
        [HttpPost("Create")]
        public IActionResult Create([FromBody] User item)
        {
            if (item == null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Add(item);

            return CreatedAtRoute("GetAllUsers", null, null);
        }
        //http://localhost:40001/api/user/update/0
        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] User item)
        {
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            Repo.Update(item);

            return CreatedAtRoute("GetAllUsers", null, null);
        }

        //http://localhost:40001/api/user/delete/0
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            User item = Repo.Find(id);
            if (item == null || item.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            Repo.Delete(item);

            return CreatedAtRoute("GetAllUsers", null, null);
        }
        //http://localhost:40001/api/user/
        [HttpGet("", Name = "GetAllUsers")]
        public IActionResult GetAll()
        {
            var data = Repo.GetAll();
            return data == null ? (IActionResult)NotFound() : new ObjectResult(data);
        }

    }
}
