namespace MiniProject_UserManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniProject_UserManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PermissionController : ControllerBase
{
    private readonly MyDbContext _context;

    public PermissionController(MyDbContext context)
    {
        _context = context;
    }

    // GET: api/Permission
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
    {
        try
        {
            return await _context.Permissions.ToListAsync();
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return BadRequest();
        }
    }

    // GET: api/Permission/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Permission>> GetPermission(int id)
    {
        var permission = await _context.Permissions.FindAsync(id);

        if (permission == null)
        {
            return NotFound();
        }

        return permission;
    }

    // POST: api/Permission
    [HttpPost]
    public async Task<ActionResult<Permission>> CreatePermission(Permission permission)
    {
        try
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPermission), new { id = permission.Id }, permission);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);

            return BadRequest(error);
        }
    }

    // PUT: api/Permission/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePermission(int id, Permission permission)
    {
        try
        {
            if (id != permission.Id)
            {
                return BadRequest();
            }

            _context.Entry(permission).State = EntityState.Modified;


            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PermissionExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Permission/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePermission(int id)
    {
        try
        {
            var permission = await _context.Permissions
                .Include(u => u.GroupList)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (permission == null)
            {
                return NotFound();
            }

            _context.Permissions.Remove(permission);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    private bool PermissionExists(int id)
    {
        try
        {
            return _context.Permissions.Any(e => e.Id == id);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return false;
        }
    }
}