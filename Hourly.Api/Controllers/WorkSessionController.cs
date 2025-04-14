using Hourly.Abstractions.Contracts.Requests.WorkSessionRequests;
using Hourly.Abstractions.Exceptions;
using Hourly.Abstractions.Mappers;
using Hourly.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hourly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSessionController : Controller
    {
        private readonly IWorkSessionService _workSessionService;
        private readonly ILogger<WorkSessionController> _logger;

        public WorkSessionController(IWorkSessionService workSessionService, ILogger<WorkSessionController> logger)
        {
            _workSessionService = workSessionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkSessions()
        {
            try
            {
                var workSessions = await _workSessionService.GetAll();
                return Ok(workSessions.Select(ws => ws.ToResponse()).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving workSessions.");
                return StatusCode(500, "An unexpected error occurred.");
            }

        }

        [HttpGet("{workSessionId}")]
        public async Task<IActionResult> GetWorkSessionById(Guid workSessionId)
        {
            try
            {
                var result = await _workSessionService.GetById(workSessionId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving a workSession.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkSession([FromBody] CreateWorkSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workSession = request.ToWorkSession();

            try
            {
                var created = await _workSessionService.Create(workSession);
                return CreatedAtAction(nameof(GetWorkSessionById), new { id = created.Id }, created.ToResponse());
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                // Log the exception details for diagnostics
                _logger.LogError(ex, "An unexpected error occurred while updating a workSession.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{workSessionId}")]
        public async Task<IActionResult> UpdateWorkSession(Guid workSessionId, [FromBody] UpdateWorkSessionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var workSession = request.ToWorkSession(workSessionId);

            try
            {
                var updated = await _workSessionService.Update(workSession);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating a workSession.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{workSessionId}")]
        public async Task<IActionResult> DeleteWorkSession(Guid workSessionId)
        {
            try
            {
                await _workSessionService.Delete(workSessionId);
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting a workSession.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
