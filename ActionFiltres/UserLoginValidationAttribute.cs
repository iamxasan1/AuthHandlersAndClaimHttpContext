using AuthHandlers56.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace AuthHandlers56.ActionFiltres;

public class UserLoginValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var HttpContext = context.HttpContext;
        if (HttpContext.Request.Cookies.ContainsKey("id"))
        {

            var id = HttpContext.Request.Cookies["id"];
            var user_id = Guid.Parse(id);

            if (UserRepository.Instanance.Users.Any(u => u.Id == user_id))
            {
                var user = UserRepository.Instanance.Users.FirstOrDefault(u => u.Id == user_id);
                Console.WriteLine("Valid");
                var claims = new List<Claim>()
                {
                    new Claim("username", user.UserName),
                    new Claim("id", user.Id.ToString())
                };
                HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    Code = 401,
                    Error = "user not logged in"
                });
                Console.WriteLine("invalid");
            }
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.HttpContext.Response.WriteAsJsonAsync(new
            {
                Code = 401,
                Error = "user not logged in"
            });
            Console.WriteLine("invalid");
        }
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        Console.WriteLine("filter tugadi");
    }
}
