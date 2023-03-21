using DotNetAPI2;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly YourDbContext _context;

        public UserController(YourDbContext context)
        {
            _context = context;
        }

        // GET api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // POST api/user
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("User name cannot be empty");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {

            if (string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("User name cannot be empty");
            }

            var existingUser = await _context.Users.FindAsync(id);
            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
            existingUser.Currency = user.Currency;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // GET api/user
        [HttpPost("signin")]
        public async Task<ActionResult<IEnumerable<User>>> Signin([FromBody] User user)
        {
            var foos = await _context.Users.Where(x => x.Username == user.Username).ToListAsync();

            if (foos[0].Password== user.Password)
            {
                return foos;
            }
            else
            {
                return BadRequest("Error");
            }
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserbyid(int id)
        {
            var foos = await _context.Users.Where(x => x.UserId == id).ToListAsync();

            return foos;
        }

        // PUT api/user/{id}/newcurrency/{newcurrency}
        [HttpPut("{id}/newcurrency/{newcurrency}")]
        public async Task<IActionResult> UpdateUserCurrency(int id, int newcurrency)
        {
            var existingUser = await _context.Users.FindAsync(id);
            existingUser.Currency = newcurrency;
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
