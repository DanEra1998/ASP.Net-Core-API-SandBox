using User.Api.DTOs;

namespace User.Api.Services;

public interface ICarLoanService
{
    // Get all car loans
    Task<List<CarLoanDto>> GetAllAsync();

    // Get one car loan by ID
    Task<CarLoanDto?> GetByIdAsync(int id);

    // Get all loans for a specific user
    Task<List<CarLoanDto>> GetByUserIdAsync(int userId);

    // Create a new car loan
    Task<CarLoanDto> CreateAsync(CreateCarLoanDto dto);

    // Delete a car loan
    Task<bool> DeleteAsync(int id);
}