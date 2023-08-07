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
public class UserController : ControllerBase
{
    private readonly MyDbContext _context;

    public UserController(MyDbContext context)
    {
        _context = context;
    }

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        try
        {
            return Ok(await _context.Users.Include(u => u.UserGroups).ToListAsync());
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return BadRequest();
        }
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.UserGroups)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<User>> CreateUser(User user)
    {
        try
        {
            //Should the context expand this will be updated to check if that spesific user already exists in db
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(CreatedAtAction(nameof(GetUser), new { id = user.Id }, user));
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);

            return BadRequest(error);
        }
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        try
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;


            await _context.SaveChangesAsync();
            return Ok($"User '{user.Name}'");
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }


    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var user = await _context.Users
                    .Include(u => u.UserGroups)
                    .FirstOrDefaultAsync(g => g.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return Ok($"User {user.Name} successfully Deleted");
        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }

    private bool UserExists(int id)
    {
        try
        {
            return _context.Users.Include(u => u.UserGroups).Any(e => e.Id == id);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return false;
        }
    }

    // GET: api/User
    [HttpGet("userCount")]
    public ActionResult<int> GetUserCount()
    {
        try
        {
            int userCount = _context.Users.Count();
            return Ok(userCount);
        }
        catch (Exception error)
        {
            Console.WriteLine(error.Message);
            return BadRequest();
        }
    }
}