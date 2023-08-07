using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject_UserManagement.Models;

namespace MiniProject_UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupManagementController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UserGroupManagementController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet("usersByGroup")]
        public async Task<ActionResult<IEnumerable<GroupUserCount>>> GetUserCountPerGroup()
        {
            var groupUserCounts = await _context.Groups
                .Select(g => new GroupUserCount
                {
                    GroupId = g.Id,
                    GroupName = g.Name,
                    UserCount = g.UserList.Count
                })
                .ToListAsync();

            return Ok(groupUserCounts);
        
    
}
    }
}
