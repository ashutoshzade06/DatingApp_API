using DatingApp_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] ///api/ssers
    public class UsersController(DataContext context) : ControllerBase
    {      

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
           var users = await context.Users.ToListAsync();
            return users;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var users = await context.Users.FindAsync(id);
            if (users == null) return NotFound();
            return users;
        }
    }
}
