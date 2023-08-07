using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject_UserManagement.Models;

namespace MiniProject_UserManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupPermissionController : ControllerBase
    {
        private readonly MyDbContext _context;

        public GroupPermissionController(MyDbContext context)
        {
            _context = context;
        }
        [HttpPost("group/add-permission")]
        public async Task<ActionResult> AddPermissionToGroup(int permissionId, int groupId)
        {
            try
            {
                var permission = await _context.Permissions
                        .Include(p => p.GroupList)
                        .FirstOrDefaultAsync(u => u.Id == permissionId);

                var group = await _context.Groups
                          .Include(g => g.UserList)
                          .FirstOrDefaultAsync(g => g.Id == groupId);

                if (permission == null || group == null)
                {
                    return NotFound();
                }

                // Check if the permission is already assigned to the group
                if (group.PermissionList.Contains(permission))
                {
                    return BadRequest("Permission is already assigned to the group.");
                }

                group.PermissionList.Add(permission);
                await _context.SaveChangesAsync();

                return Ok($"Permission '{permission.Name}' has been added to group '{group.Name}'.");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPost("group/remove-permission")]
        public async Task<ActionResult> RemovePermissionFromGroup(int permissionId, int groupId)
        {
            try
            {

                var permission = await _context.Permissions
                    .Include(p => p.GroupList)
                    .FirstOrDefaultAsync(u => u.Id == permissionId);
                var group = await _context.Groups
                         .Include(g => g.UserList)
                         .FirstOrDefaultAsync(g => g.Id == groupId);

                if (permission == null || group == null)
                {
                    return NotFound();
                }

                // Check if the permission is assigned to the group
                if (!group.PermissionList.Contains(permission))
                {
                    return BadRequest("Permission is not assigned to the group.");
                }

                group.PermissionList.Remove(permission);
                await _context.SaveChangesAsync();

                return Ok($"Permission '{permission.Name}' has been removed from group '{group.Name}'.");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
