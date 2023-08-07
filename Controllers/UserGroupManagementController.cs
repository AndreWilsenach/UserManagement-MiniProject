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

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult> AssignUserToGroup(int userId, int groupId)
        {
            try { 
            var user = await _context.Users.FindAsync(userId);
            var group = await _context.Groups.FindAsync(groupId);

            if (user == null || group == null)
            {
                return NotFound();
            }

            // Check if the user is already assigned to the group
            if (group.UserList.Contains(user))
            {
                return BadRequest("User is already assigned to the group.");
            }

            group.UserList.Add(user);
            await _context.SaveChangesAsync();

            return Ok($"User '{user.Name}' has been assigned to group '{group.Name}'.");
            }
            catch(Exception error) {
            return BadRequest(error.Message);
            }
        }
        [HttpPost("group/remove-user")]
        public async Task<ActionResult> RemoveUserFromGroup(int userId, int groupId)
        {
            try { 
            var user = await _context.Users.FindAsync(userId);
            var group = await _context.Groups.FindAsync(groupId);

            if (user == null || group == null)
            {
                return NotFound();
            }

            // Check if the user is assigned to the group
            if (!group.UserList.Contains(user))
            {
                return BadRequest("User is not assigned to the group.");
            }

            group.UserList.Remove(user);
            await _context.SaveChangesAsync();

            return Ok($"User '{user.Name}' has been removed from group '{group.Name}'.");
            }
            catch (Exception error) {
                return BadRequest(error.Message);
            }
        }
    }
}
