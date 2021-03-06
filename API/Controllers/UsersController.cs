using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers

{
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdentityUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<IdentityUser>> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}