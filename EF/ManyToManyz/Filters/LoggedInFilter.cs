using ManyToManyz.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ManyToManyz.Filters
{
    public class LoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.HttpContext.Session.GetInt32(HomeController.USER_KEY) == null)
            {
                context.Result = new RedirectToActionResult("Index", "Home", null);
            }
        }
    }
}