using Microsoft.EntityFrameworkCore;
using User.Api.Data;
using User.Api.DTOs;
using User.Api.Models;

// UserService has ONE job:
// talk to the database
// and return DTOs
namespace User.Api.Services;

// Implements IUserService interface
// contains actual business logic
// and database queries via EF Core
public class UserService : IUserService
{
    // private = only this class can use it
    // readonly = can only be set in constructor
    private readonly AppDbContext _db;

    // GetAllAsync:
    //   goes to DB
    //   fetches all users
    //   maps to DTOs
    //   returns list

    // GetByIdAsync:
    //   goes to DB
    //   finds one user
    //   maps to DTO
    //   returns it or null

    // CreateAsync:
    //   takes CreateUserDto
    //   maps to Entity
    //   saves to DB
    //   returns UserDto

    // DeleteAsync:
    //   finds user in DB
    //   marks IsDeleted = true
    //   saves to DB
    //   returns true/false

    // Constructor — AppDbContext injected via DI
    // ASP.NET creates and passes it automatically
    public UserService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<UserDto>> GetAllAsync()
    {
        return await _db.Users
            // soft delete filter
            // never return deleted users
            .Where(u => !u.IsDeleted)
            // map Entity → DTO
            // hides internal fields
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Phone = u.Phone
            })
            // async DB call
            .ToListAsync();
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        // find user by ID that isn't deleted
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

        // return null if not found
        // controller returns 404
        if (user is null) return null;

        // map Entity → DTO
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone
        };
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto)
    {
        // map DTO → Entity
        // DTO = what client sent
        // Entity = what DB needs
        var user = new UserEntity
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            CreatedAt = DateTime.UtcNow  // server sets this
        };

        // stage new user in memory
        _db.Users.Add(user);

        // save to PostgreSQL
        await _db.SaveChangesAsync();

        // map back to DTO
        // user.Id now populated by DB ✅
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _db.Users.FindAsync(id);

        // return false if not found
        if (user is null) return false;

        // SOFT DELETE
        // never hard delete!
        // data stays in DB
        // just marked as deleted
        user.IsDeleted = true;
        user.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }
}