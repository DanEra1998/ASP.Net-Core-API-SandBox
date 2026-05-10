namespace User.Api.Models;


public class UserEntity
{public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public DateTime CreatedAt { get; set; }   // ← in DB
    public DateTime? UpdatedAt { get; set; }  // ← in DB
    public bool IsDeleted { get; set; }       // ← in DB

    // below is a navigation property and is not a column in the db
    //Navigation properties are: C# only concept, EF Core uses them to understand relationships NOT stored in database

    public List<CarLoanEntity> CarLoans { get; set; }
}