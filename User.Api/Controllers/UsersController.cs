// Microsoft.AspNetCore.Mvc provides:
// ControllerBase, IActionResult, Ok(), NotFound() etc.
using Microsoft.AspNetCore.Mvc;

// Our DTOs live in User.Api/DTOs/
// CreateUserDto.cs = what we receive from client
// UserDto.cs = what we send back to client
using User.Api.DTOs;

// Our Services live in User.Api/Services/
// IUserService.cs = the interface (contract)
// UserService.cs = the implementation
using User.Api.Services;

namespace User.Api.Controllers;

// [ApiController] = attribute that tells ASP.NET:
//   - this class handles HTTP requests
//   - automatically returns 400 if request body is invalid
//   - automatically deserializes JSON request body
[ApiController]

// [Route] = defines the base URL for ALL endpoints in this controller
// "api" = standard prefix for API endpoints
// "v1"  = version 1 of our API
//         if we break changes later → v2, v1 still works
// "[controller]" = replaced automatically with "Users"
//                  (from "UsersController" minus "Controller")
// Full base URL = http://localhost:5059/api/v1/users
[Route("api/v1/[controller]")]

// : ControllerBase = inheritance
// gives us HTTP helper methods:
//   Ok()        → HTTP 200
//   NotFound()  → HTTP 404
//   NoContent() → HTTP 204
//   CreatedAtAction() → HTTP 201
public class UsersController : ControllerBase
{
    // private = only this class can use _userService
    // readonly = can only be assigned in constructor
    //            cannot be changed after that
    // IUserService = we depend on the INTERFACE
    //                NOT the concrete UserService class
    //                this is DEPENDENCY INJECTION pattern ✅
    //                makes it easy to:
    //                  - swap implementations
    //                  - mock in unit tests
    //                  - keep controller clean
    // Lives in: User.Api/Services/IUserService.cs
    private readonly IUserService _userService;

    // CONSTRUCTOR = runs when controller is created
    // DI in action:
    //   1. Request comes in
    //   2. ASP.NET needs to create UsersController
    //   3. Sees constructor needs IUserService
    //   4. Checks Program.cs service registrations
    //   5. Finds: builder.Services.AddScoped<IUserService, UserService>()
    //   6. Creates UserService and passes it here
    //   7. We store it as _userService
    // We never write: new UserService() ← DI handles it ✅
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // [HttpGet] = responds to GET requests
    // Full URL: GET http://localhost:5059/api/v1/users
    // async = non-blocking, frees thread while waiting for DB
    // Task<IActionResult> = async wrapper around HTTP response
    // IActionResult = can return any HTTP response type
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // delegates to UserService.GetAllAsync()
        // Lives in: User.Api/Services/UserService.cs
        // returns List<UserDto> from DB
        var users = await _userService.GetAllAsync();

        // Ok() = HTTP 200 with users as JSON body
        // ASP.NET automatically serializes
        // List<UserDto> → JSON array
        return Ok(users);
    }

    // [HttpGet("{id}")] = responds to GET with an ID
    // {id} in route maps to int id parameter below
    // Full URL: GET http://localhost:5059/api/v1/users/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        // delegates to UserService.GetByIdAsync()
        // Lives in: User.Api/Services/UserService.cs
        // returns UserDto? (nullable = might be null)
        var user = await _userService.GetByIdAsync(id);

        // ternary operator:
        // if user is null → NotFound() = HTTP 404
        // if user exists  → Ok(user)   = HTTP 200 + JSON
        return user is null ? NotFound() : Ok(user);
    }

    // [HttpPost] = responds to POST requests
    // Full URL: POST http://localhost:5059/api/v1/users
    // CreateUserDto = automatically deserialized from JSON body
    // Lives in: User.Api/DTOs/CreateUserDto.cs
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        // delegates to UserService.CreateAsync()
        // Lives in: User.Api/Services/UserService.cs
        // maps DTO → Entity → saves to DB → returns DTO
        var user = await _userService.CreateAsync(dto);

        // CreatedAtAction = HTTP 201 Created with:
        //   Location header = URL to GET the new resource
        //   nameof(GetById) = points to GetById method above
        //   new { id = user.Id } = the route parameter
        //   user = the created resource as JSON body
        // Response header: Location: /api/v1/users/4
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    // [HttpDelete("{id}")] = responds to DELETE with an ID
    // Full URL: DELETE http://localhost:5059/api/v1/users/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        // delegates to UserService.DeleteAsync()
        // Lives in: User.Api/Services/UserService.cs
        // performs SOFT DELETE (marks IsDeleted = true)
        // returns bool: true = deleted, false = not found
        var result = await _userService.DeleteAsync(id);

        // ternary:
        // true  → NoContent() = HTTP 204 (success, nothing to return)
        // false → NotFound()  = HTTP 404 (user didn't exist)
        return result ? NoContent() : NotFound();
    }
}