using Microsoft.AspNetCore.Mvc;
using User.Api.DTOs;
using User.Api.Services;

namespace User.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CarLoansController : ControllerBase
{
    private readonly ICarLoanService _carLoanService;

    public CarLoansController(ICarLoanService carLoanService)
    {
        _carLoanService = carLoanService;
    }

    // GET /api/v1/carloans
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var loans = await _carLoanService.GetAllAsync();
        return Ok(loans);
    }

    // GET /api/v1/carloans/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var loan = await _carLoanService.GetByIdAsync(id);
        return loan is null ? NotFound() : Ok(loan);
    }

    // GET /api/v1/carloans/user/1
    // get all loans for a specific user
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var loans = await _carLoanService.GetByUserIdAsync(userId);
        return Ok(loans);
    }

    // POST /api/v1/carloans
    [HttpPost]
    public async Task<IActionResult> Create(CreateCarLoanDto dto)
    {
        var loan = await _carLoanService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = loan.Id }, loan);
    }

    // DELETE /api/v1/carloans/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _carLoanService.DeleteAsync(id);
        return result ? NoContent() : NotFound();
    }
}