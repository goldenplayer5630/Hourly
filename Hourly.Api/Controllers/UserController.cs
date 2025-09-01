using Hourly.Abstractions.Services;
using Hourly.Application.Services;
using Hourly.Domain.Contracts.Requests.UserRequests;
using Hourly.Domain.Exceptions;
using Hourly.Domain.Mappers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hourly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISummaryService _summaryService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ISummaryService summaryService, ILogger<UserController> logger)
        {
            _userService = userService;
            _summaryService = summaryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAll();
                var response = users.Select(user => user.ToSummaryResponse()).ToList();
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving a gitRepository.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            try
            {
                var result = await _userService.GetById(userId);
                return Ok(result.ToResponse());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving a user.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{userId}/MonthlySummary")]
        public async Task<IActionResult> GetUserMonthlySummary(Guid userId, [FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                var summary = await _summaryService.GenerateMonthlySummary(userId, year, month);
                return Ok(summary.ToResponse());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating monthly summary for UserId: {UserId}, Year: {Year}, Month: {Month}", userId, year, month);
                return StatusCode(500, "An error occurred while generating the monthly summary.");
            }
        }

        [HttpGet("{userId}/YearlySummary")]
        public async Task<IActionResult> GetUserYearlySummary(Guid userId, [FromQuery] int year)
        {
            try
            {
                var summary = await _summaryService.GenerateYearlySummary(userId, year);
                return Ok(summary.ToResponse());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating yearly summary for UserId: {UserId}, Year: {Year}", userId, year);
                return StatusCode(500, "An error occurred while generating the yearly summary.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = request.ToUser();

            try
            {
                var created = await _userService.Create(user);
                return CreatedAtAction(nameof(GetUserById), new { userId = created.Id }, created.ToResponse());
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details for diagnostics
                _logger.LogError(ex, "An unexpected error occurred while updating a user.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("{userId}/AddDepartment/{departmentId}")]
        public async Task<IActionResult> AddDepartment(Guid userId, Guid departmentId)
        {
            try
            {
                var result = await _userService.AddDepartment(userId, departmentId);
                return Ok(result.ToResponse());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while adding a user to a department.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost("{userId}/RemoveDepartment")]
        public async Task<IActionResult> RemoveDepartment(Guid userId)
        {
            try
            {
                var result = await _userService.RemoveDepartment(userId);
                return Ok(result.ToResponse());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while removing a user from a department.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = request.ToUser(userId);

            try
            {
                var updated = await _userService.Update(user);
                return Ok(updated.ToResponse());
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating a user.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            try
            {
                await _userService.Delete(userId);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DomainValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting a user.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
