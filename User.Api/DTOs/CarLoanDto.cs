namespace User.Api.DTOs;

// What we RETURN to client for car loan data
public record CarLoanDto
{
    public int Id { get; init; }
    public int UserId { get; init; }                    // which user owns this loan
    public string CarMake { get; init; } = string.Empty;  // Toyota, Honda etc
    public string CarModel { get; init; } = string.Empty; // Camry, Civic etc
    public int CarYear { get; init; }
    public decimal LoanAmount { get; init; }            // total loan amount
    public decimal InterestRate { get; init; }          // annual interest rate
    public decimal? MonthlyPayment { get; init; }       // nullable, might not be calculated
    public DateOnly StartDate { get; init; }            // DateOnly = date without time
    public DateOnly? EndDate { get; init; }             // nullable, loan might still be active
    public string Status { get; init; } = string.Empty; // Active, Closed etc

    // Notice: NO CreatedAt ← internal field, hidden ✅
}