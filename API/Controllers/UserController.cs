using Application.DataProvider;
using Application.Domain.User;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(UserDataProvider.GetUsers());
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            return Ok(UserDataProvider.GetUser(id));
        }
        [HttpPost("login")]
        public IActionResult LoginUser(User user)
        {
            return Ok(UserDataProvider.LoginUser(user));
        }
        [HttpPost("register")]
        public IActionResult RegisterUser(User user)
        {
            return Ok(UserDataProvider.RegisterUser(user));
        }
        [HttpPost("update")]
        public IActionResult Update(User user)
        {
            UserDataProvider.UpdateUser(user);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            UserDataProvider.DeleteUser(id);
            return Ok();
        }

    }
}