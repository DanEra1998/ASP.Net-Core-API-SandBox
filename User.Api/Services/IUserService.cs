using User.Api.DTOs;

namespace User.Api.Services;

// Interface = the CONTRACT
// defines WHAT the service can do
// without HOW it does it
// Controllers depend on this
// not on the concrete class
public interface IUserService
{
    // Get all users that are not deleted
    Task<List<UserDto>> GetAllAsync();

    // Get one user by ID
    // returns null if not found
    Task<UserDto?> GetByIdAsync(int id);

    // Create a new user
    // returns created user with generated ID
    Task<UserDto> CreateAsync(CreateUserDto dto);

    // Soft delete a user
    // returns true if deleted
    // returns false if not found
    Task<bool> DeleteAsync(int id);
}