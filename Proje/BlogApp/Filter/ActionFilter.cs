using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogApp.Filter
{
    public class ActionFilter: IActionFilter
    {
        //It triggered after the action executed.
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine($"Action executed. Executed Time: {DateTime.Now}");
        }
        //It works when action is executing.
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Debug.WriteLine($"Action is executing. Executing time: {DateTime.Now}");
        }
    }
}
