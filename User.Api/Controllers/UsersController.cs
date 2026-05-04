// Gives us MVC tools like ControllerBase, IActionResult, Ok(), NotFound() etc.
using Microsoft.AspNetCore.Mvc;
// Gives us ToListAsync(), FindAsync() — async database query methods from EF Core
using Microsoft.EntityFrameworkCore;
// Import our Data folder so we can reference AppDbContext
using User.Api.Data;
// Import our Models folder so we can reference UserEntity
using User.Api.Models;

namespace User.Api.Controllers;

// Tells ASP.NET this class is an API Controller — enables automatic features like
// automatic 400 Bad Request responses if the request body is invalid
[ApiController]

// Defines the URL route for this controller
// [controller] is replaced automatically with "Users" (from "UsersController")
// So all endpoints here are under: http://localhost:5000/api/users
[Route("api/[controller]")]
public class UsersController : ControllerBase
// ControllerBase gives us HTTP response helpers like Ok(), NotFound(), NoContent() etc.
{
    // _db is our database connection — private so only this class can use it
    // readonly means it can only be assigned once (in the constructor below)
    private readonly AppDbContext _db;

    // CONSTRUCTOR — runs when the controller is created
    // ASP.NET automatically passes in AppDbContext via Dependency Injection
    // meaning you never manually create AppDbContext, ASP.NET handles it for you
    // This is why we registered it in Program.cs with builder.Services.AddDbContext
    public UsersController(AppDbContext db)
    {
        _db = db; // store it so all methods in this class can use _db
    }

    // [HttpGet] = this method responds to GET requests at /api/users
    // Task = this is async (non-blocking)
    // IActionResult = can return any HTTP response (200, 404, etc.)
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Go to the Users table and fetch ALL rows as a List
        // await = wait for the database to respond without blocking the thread
        var users = await _db.Users.ToListAsync();
        // Return HTTP 200 OK with the list of users as JSON
        return Ok(users);
    }

    // [HttpGet("{id}")] = responds to GET /api/users/1 (or any id number)
    // {id} in the route maps to the "int id" parameter below
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // Find a single user by their primary key (Id column)
        var user = await _db.Users.FindAsync(id);
        // If user is null (not found) return 404, otherwise return 200 with the user
        return user is null ? NotFound() : Ok(user);
    }

    // [HttpPost] = responds to POST requests at /api/users
    // The UserEntity object is automatically deserialized from the JSON request body
    [HttpPost]
    public async Task<IActionResult> Create(UserEntity user)
    {
        // Add the new user to the Users table (in memory, not saved yet)
        _db.Users.Add(user);
        // Actually save it to the Postgres database
        await _db.SaveChangesAsync();
        // Return HTTP 201 Created with:
        // - A Location header pointing to GET /api/users/{newId}
        // - The newly created user as JSON in the body
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    // [HttpDelete("{id}")] = responds to DELETE /api/users/1 (or any id)
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // First find the user — we need the object to delete it
        var user = await _db.Users.FindAsync(id);
        // If not found return 404
        if (user is null) return NotFound();
        // Mark the user for deletion (in memory, not deleted yet)
        _db.Users.Remove(user);
        // Actually delete it from the Postgres database
        await _db.SaveChangesAsync();
        // Return HTTP 204 No Content — success but nothing to return
        return NoContent();
    }
}