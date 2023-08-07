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
public class GroupController : ControllerBase
{
    private readonly MyDbContext _context;

    public GroupController(MyDbContext context)
    {
        _context = context;
    }

    // GET: api/Group
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
    {
        try { 
        return await _context.Groups.ToListAsync();
        }
        catch(Exception error) {
     Console.WriteLine(error.Message);
            return BadRequest();
        }
    }

    // GET: api/Group/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> GetGroup(int id)
    {
        var group = await _context.Groups.FindAsync(id);

        if (group == null)
        {
            return NotFound();
        }

        return group;
    }

    // POST: api/Group
    [HttpPost]
    public async Task<ActionResult<Group>> CreateGroup(Group group)
    {
        try { 
        _context.Groups.Add(group);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetGroup), new { id = group.Id }, group);
        }
        catch (Exception error) { 
            Console.WriteLine(error.Message);

            return BadRequest(error);
        }
    }

    // PUT: api/Group/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGroup(int id, Group group)
    {
        try
        {
            if (id != group.Id)
        {
            return BadRequest();
        }

        _context.Entry(group).State = EntityState.Modified;

     
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!GroupExists(id))
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

    // DELETE: api/Group/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGroup(int id)
    {
        try { 
        var group = await _context.Groups.FindAsync(id);
        if (group == null)
        {
            return NotFound();
        }

        _context.Groups.Remove(group);
        
        await _context.SaveChangesAsync();

        return NoContent();
        }
        catch (Exception error) {
        return BadRequest(error.Message);
        }
    }

    private bool GroupExists(int id)
    {
        try {
        return _context.Users.Any(e => e.Id == id);
        }
        catch (Exception error) {
            Console.WriteLine(error.Message);
            return false;
              }
    }
}