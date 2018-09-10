using System;
using PRS.DAL.Repos.Base;
using PRS.Models.Entities;
using PRS.Models.ViewModels.Base;
using System.Collections.Generic;

namespace PRS.DAL.Repos.Interfaces
{
    public interface IEmployeeRepo : IRepo<Employee>
    {
        IEnumerable<EmployeeBase> Search(string searchString);
    }
}
