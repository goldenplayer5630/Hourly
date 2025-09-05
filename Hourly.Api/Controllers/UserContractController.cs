using Hourly.Abstractions.Services;
using Hourly.Application.Services;
using Hourly.Domain.Contracts.Requests.UserContractRequests;
using Hourly.Domain.Exceptions;
using Hourly.Domain.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Hourly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContractController : Controller
    {
        private readonly IUserContractService _userContractService;
        private readonly ISummaryService _summaryService;
        private readonly ILogger<UserContractController> _logger;

        public UserContractController(IUserContractService userContractService, ISummaryService summaryService, ILogger<UserContractController> logger)
        {
            _userContractService = userContractService;
            _summaryService = summaryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserContracts()
        {
            try
            {
                var userContracts = await _userContractService.GetAll();
                var response = userContracts.Select(userContract => userContract.ToResponse()).ToList();
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

        [HttpGet("Filter")]
        public async Task<IActionResult> FilterUserContracts([FromQuery] Guid? userId, [FromQuery] int? year, [FromQuery] int? month, bool? isActive)
        {
            try
            {
                var results = await _userContractService.FilterUserContracts(userId, year, month, isActive);
                return Ok(results.Select(uc => uc.ToResponse()));
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
                _logger.LogError(ex, "An unexpected error occurred while filtering user contracts.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{userContractId}")]
        public async Task<IActionResult> GetUserContractById(Guid userContractId)
        {
            try
            {
                var result = await _userContractService.GetById(userContractId);
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
                _logger.LogError(ex, "An unexpected error occurred while retrieving a userContract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("{userContractId}/MonthlySummary")]
        public async Task<IActionResult> GetUserMonthlySummary(Guid userContractId, [FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                var summary = await _summaryService.GenerateMonthlySummary(userContractId, year, month);
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
                _logger.LogError(ex, "Error generating monthly summary for UserId: {UserId}, Year: {Year}, Month: {Month}", userContractId, year, month);
                return StatusCode(500, "An error occurred while generating the monthly summary.");
            }
        }

        [HttpGet("{userContractId}/YearlySummary")]
        public async Task<IActionResult> GetUserYearlySummary(Guid userContractId, [FromQuery] int year)
        {
            try
            {
                var summary = await _summaryService.GenerateYearlySummary(userContractId, year);
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
                _logger.LogError(ex, "Error generating yearly summary for UserId: {UserId}, Year: {Year}", userContractId, year);
                return StatusCode(500, "An error occurred while generating the yearly summary.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserContract([FromBody] CreateUserContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userContract = request.ToUserContract();

            try
            {
                var created = await _userContractService.Create(userContract);
                return CreatedAtAction(nameof(GetUserContractById), new { userContractId = created.Id }, created.ToResponse());
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
                _logger.LogError(ex, "An unexpected error occurred while updating a userContract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{userContractId}")]
        public async Task<IActionResult> UpdateUserContract(Guid userContractId, [FromBody] UpdateUserContractRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userContract = request.ToUserContract(userContractId);

            try
            {
                var updated = await _userContractService.Update(userContract);
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
                _logger.LogError(ex, "An unexpected error occurred while updating a userContract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("{userContractId}/LockMonth/")]
        public async Task<IActionResult> LockMonth(Guid userContractId, [FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                var result = await _userContractService.AddLockedMonth(userContractId, year, month);
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
                _logger.LogError(ex, "An unexpected error occurred while locking a month for a user contract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPatch("{userContractId}/UnlockMonth/")]
        public async Task<IActionResult> UnlockMonth(Guid userContractId, [FromQuery] int year, [FromQuery] int month)
        {
            try
            {
                var result = await _userContractService.RemoveLockedMonth(userContractId, year, month);
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
                _logger.LogError(ex, "An unexpected error occurred while unlocking a month for a user contract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{userContractId}")]
        public async Task<IActionResult> DeleteUserContract(Guid userContractId)
        {
            try
            {
                await _userContractService.Delete(userContractId);
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
                _logger.LogError(ex, "An unexpected error occurred while deleting a userContract.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
