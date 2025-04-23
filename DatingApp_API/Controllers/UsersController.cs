using DatingApp_API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_API.Controllers
{
    
    public class UsersController(DataContext context) : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
           var users = await context.Users.ToListAsync();
            return users;
        }
        
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var users = await context.Users.FindAsync(id);
            if (users == null) return NotFound();
            return users;
        }
    }
}
