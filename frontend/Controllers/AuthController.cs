using Microsoft.AspNetCore.Mvc;
using WorldOfArt.Data;
using WorldOfArt.Models;

namespace WorldOfArt.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(User login)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == login.Email && x.password == login.password);

            if (user == null)
       
                return Unauthorized();
            return Ok(user);
        }
    }
}
   
