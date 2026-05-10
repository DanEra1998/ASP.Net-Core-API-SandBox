namespace User.Api.Models;

public class CarLoanEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }                      // FK to Users
    public string CarMake { get; set; } = string.Empty;
    public string CarModel { get; set; } = string.Empty;
    public int CarYear { get; set; }
    public decimal LoanAmount { get; set; }
    public decimal InterestRate { get; set; }
    public decimal? MonthlyPayment { get; set; }         // nullable
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }               // nullable
    public string Status { get; set; } = "Active";
    public DateTime CreatedAt { get; set; }

    // Navigation property back to User
    public UserEntity User { get; set; } = null!;
}