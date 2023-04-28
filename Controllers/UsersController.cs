using AuthHandlers56.ActionFiltres;
using AuthHandlers56.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthHandlers56.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult SignUp(string username)
        {

            var user = new User()
            {
                Id = Guid.NewGuid(),
                UserName = username
            };
            UserRepository.Instanance.Users.Add(user);

            HttpContext.Response.Cookies.Append("id", user.Id.ToString());

            return Ok();
        }



        [UserLoginValidation]
        public string Profile()
        {
            ClaimsPrincipal user = HttpContext.User;
            var username = user.FindFirstValue("username");
            var id = Guid.Parse(user.FindFirstValue("id"));
            
            return username;
        }
        public string Tickets()
        {
            var user = IsLoggedIn();
            if(user != null)
            {
                return user.UserName + "ticket";
            }
            
            return "not logged in";
        }

        public User IsLoggedIn() 
        {
            if (HttpContext.Request.Cookies.ContainsKey("id"))
            {

                var id = HttpContext.Request.Cookies["id"];
                var user_id = Guid.Parse(id);

                if (UserRepository.Instanance.Users.Any(u => u.Id == user_id))
                {
                    var user = UserRepository.Instanance.Users.FirstOrDefault(u => u.Id == user_id);
                    return user;
                }
            }
            return null;
        }
    }
}
