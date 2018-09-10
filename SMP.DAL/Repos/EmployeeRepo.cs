using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PRS.DAL.EF;
using PRS.DAL.Repos.Base;
using PRS.Models.Entities;
using PRS.Models.ViewModels.Base;
using PRS.DAL.Repos.Interfaces;

namespace PRS.DAL.Repos
{
    public class EmployeeRepo : RepoBase<Employee>, IEmployeeRepo
    {
        public EmployeeRepo(DbContextOptions<StoreContext> options) : base(options)
        {
        }
        public EmployeeRepo() : base()
        {
        }
        internal EmployeeBase GetRecord(Employee e, string name)
            => new EmployeeBase()
            {
                EmployeeId = e.Id,
                FullName = e.FirstName + " " + e.LastName,
                SupervisorName = e.EmployeeGroup.Parent.Head.FirstName + " " + e.EmployeeGroup.Parent.Head.LastName,
                GroupName = e.EmployeeGroup.Name,
                Active = e.Active,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                EmailAddress = e.Email,
                Password = e.Password
            };
        public override IEnumerable<Employee> GetAll()
            => Table.OrderBy(x => x.Id);

        public override IEnumerable<Employee> GetRange(int skip, int take)
            => GetRange(Table.OrderBy(x => x.Id), skip, take);
        public IEnumerable<EmployeeBase> Search(string searchString)
            => Table
                .Where(p =>
                       p.FirstName.ToLower().Contains(searchString.ToLower()))
                .Include(p => p.FirstName)
                .Select(item => GetRecord(item, item.FirstName))
                .OrderBy(x => x.EmployeeId);
        
    }
}