namespace User.Api.DTOs;

// What we RECEIVE from client to CREATE a car loan
public record CreateCarLoanDto
{
    // required = must be provided
    public required int UserId { get; init; }           // which user is this loan for?
    public required string CarMake { get; init; }
    public required string CarModel { get; init; }
    public required int CarYear { get; init; }
    public required decimal LoanAmount { get; init; }
    public required decimal InterestRate { get; init; }

    // optional fields
    public decimal? MonthlyPayment { get; init; }       // might be calculated later
    public required DateOnly StartDate { get; init; }
    public DateOnly? EndDate { get; init; }             // loan might not have end date yet

    // defaults to Active if not provided
    public string Status { get; init; } = "Active";
}