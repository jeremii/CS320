using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
//using SMP.MVC.Authentication;

namespace SMP.MVC.Filters
{
    public class AuthActionFilter : IActionFilter
    {
        //private IAuthHelper _authHelper;
        //public AuthActionFilter(IAuthHelper authHelper)
        public AuthActionFilter()
        {
            //_authHelper = authHelper;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //var viewBag = ((Controller)context.Controller).ViewBag;
            //var authHelper = (IAuthHelper)context.HttpContext.RequestServices.GetService(typeof(IAuthHelper));
            //var employee = _authHelper.GetEmployeeInfo();
            ////viewBag.EmployeeId = employee.Id;
            //viewBag.Employee.Id = employee.Id;
            //viewBag.Employee.FirstName = employee.FirstName;
            //viewBag.Employee.LastName = employee.LastName;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }
    }



}
