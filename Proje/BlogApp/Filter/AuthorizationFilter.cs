using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BlogApp.Filters
{
    // Purpose: This filter checks if the user is authenticated
    // before allowing access to certain actions or controllers. This is useful for protecting sensitive areas of the application.

    //At first: How HTTP request reaches to the action method?
    // 1. Middleware (Program.cs) 
    // 2. Routing (Program.cs)
    // 3. Authorization Filter (Filters/AuthorizationFilter.cs)
    // 4. Action Filter (Filters/ActionFilter.cs)
    // 5. Action Method (Controllers/SomeController.cs)
    // 6. Result Filter (Filters/ResultFilter.cs)
    // 7. Middleware (Program.cs)
    // 8. Response to the client
    // At last: How HTTP response reaches to the client?
    // 1. Middleware (Program.cs)
    // 2. Result Filter (Filters/ResultFilter.cs)
    // 3. Middleware (Program.cs)
    // 4. Response to the client
    // Note: Authorization filters run before action filters.
    // If the authorization filter denies access, the action filter will not run.
    // This filter can be applied globally, at the controller level, or at the action level.
    public class AuthorizationFilter : IAuthorizationFilter
    {
        //implemented interface method
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
