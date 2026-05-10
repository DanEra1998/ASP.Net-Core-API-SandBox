using Microsoft.EntityFrameworkCore;
using User.Api.Data;
using User.Api.DTOs;
using User.Api.Models;

namespace User.Api.Services;

public class CarLoanService : ICarLoanService
{
    private readonly AppDbContext _db;

    public CarLoanService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CarLoanDto>> GetAllAsync()
    {
        return await _db.CarLoans
            .Select(cl => new CarLoanDto
            {
                Id = cl.Id,
                UserId = cl.UserId,
                CarMake = cl.CarMake,
                CarModel = cl.CarModel,
                CarYear = cl.CarYear,
                LoanAmount = cl.LoanAmount,
                InterestRate = cl.InterestRate,
                MonthlyPayment = cl.MonthlyPayment,
                StartDate = cl.StartDate,
                EndDate = cl.EndDate,
                Status = cl.Status
            })
            .ToListAsync();
    }

    public async Task<CarLoanDto?> GetByIdAsync(int id)
    {
        var loan = await _db.CarLoans
            .FirstOrDefaultAsync(cl => cl.Id == id);

        if (loan is null) return null;

        return new CarLoanDto
        {
            Id = loan.Id,
            UserId = loan.UserId,
            CarMake = loan.CarMake,
            CarModel = loan.CarModel,
            CarYear = loan.CarYear,
            LoanAmount = loan.LoanAmount,
            InterestRate = loan.InterestRate,
            MonthlyPayment = loan.MonthlyPayment,
            StartDate = loan.StartDate,
            EndDate = loan.EndDate,
            Status = loan.Status
        };
    }

    public async Task<List<CarLoanDto>> GetByUserIdAsync(int userId)
    {
        // get all loans for a specific user
        // this is the JOIN endpoint
        return await _db.CarLoans
            .Where(cl => cl.UserId == userId)
            .Select(cl => new CarLoanDto
            {
                Id = cl.Id,
                UserId = cl.UserId,
                CarMake = cl.CarMake,
                CarModel = cl.CarModel,
                CarYear = cl.CarYear,
                LoanAmount = cl.LoanAmount,
                InterestRate = cl.InterestRate,
                MonthlyPayment = cl.MonthlyPayment,
                StartDate = cl.StartDate,
                EndDate = cl.EndDate,
                Status = cl.Status
            })
            .ToListAsync();
    }

    public async Task<CarLoanDto> CreateAsync(CreateCarLoanDto dto)
    {
        var loan = new CarLoanEntity
        {
            UserId = dto.UserId,
            CarMake = dto.CarMake,
            CarModel = dto.CarModel,
            CarYear = dto.CarYear,
            LoanAmount = dto.LoanAmount,
            InterestRate = dto.InterestRate,
            MonthlyPayment = dto.MonthlyPayment,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Status = dto.Status,
            CreatedAt = DateTime.UtcNow
        };

        _db.CarLoans.Add(loan);
        await _db.SaveChangesAsync();

        return new CarLoanDto
        {
            Id = loan.Id,
            UserId = loan.UserId,
            CarMake = loan.CarMake,
            CarModel = loan.CarModel,
            CarYear = loan.CarYear,
            LoanAmount = loan.LoanAmount,
            InterestRate = loan.InterestRate,
            MonthlyPayment = loan.MonthlyPayment,
            StartDate = loan.StartDate,
            EndDate = loan.EndDate,
            Status = loan.Status
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var loan = await _db.CarLoans.FindAsync(id);
        if (loan is null) return false;

        _db.CarLoans.Remove(loan);
        await _db.SaveChangesAsync();
        return true;
    }
}