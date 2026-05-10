//these are for relaying info from // This namespace groups all DTOs together
namespace User.Api.DTOs;

// record = immutable DTO, perfect for responses
// This is what we RETURN to the client (GET response)
public record UserDto
{
    public int Id { get; init; }                    // DB generated ID
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? Phone { get; init; }             // nullable, might not have one

    // Notice: NO CreatedAt, UpdatedAt, IsDeleted
    // those are internal fields ← abstraction! ✅
}