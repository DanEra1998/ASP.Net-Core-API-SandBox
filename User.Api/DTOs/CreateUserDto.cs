namespace User.Api.DTOs;

// This is what we RECEIVE from client (POST request body)
// Different from UserDto because:
//   no Id needed   ← DB generates it
//   no CreatedAt   ← server sets it
public record CreateUserDto
{
    // required = client MUST provide this
    // compile error + 400 Bad Request if missing
    public required string Name { get; init; }
    public required string Email { get; init; }

    // ? = optional, client can omit this
    public string? Phone { get; init; }
}