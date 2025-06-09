using Hourly.Abstractions.Services;
using Hourly.Shared.Contracts.Requests.GitRepositoryRequests;
using Hourly.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Hourly.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GitRepositoryController : Controller
    {
        private readonly IGitRepositoryService _gitRepositoryService;
        private readonly ILogger<GitRepositoryController> _logger;

        public GitRepositoryController(IGitRepositoryService gitRepositoryService, ILogger<GitRepositoryController> logger)
        {
            _gitRepositoryService = gitRepositoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGitRepositories()
        {
            try
            {
                var gitRepositories = await _gitRepositoryService.GetAll();
                return Ok(gitRepositories.Select(gr => gr).ToList());
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

        [HttpGet("{gitRepositoryId}")]
        public async Task<IActionResult> GetGitRepositoryById(Guid gitRepositoryId)
        {
            try
            {
                var result = await _gitRepositoryService.GetById(gitRepositoryId);
                return Ok(result);
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

        [HttpPost]
        public async Task<IActionResult> CreateGitRepository([FromBody] CreateGitRepositoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var created = await _gitRepositoryService.Create(request);
                return CreatedAtAction(nameof(GetGitRepositoryById), new { gitRepositoryId = created.Id }, created);
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
                _logger.LogError(ex, "An unexpected error occurred while updating a gitRepository.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpPut("{gitRepositoryId}")]
        public async Task<IActionResult> UpdateGitRepository(Guid gitRepositoryId, [FromBody] UpdateGitRepositoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _gitRepositoryService.Update(gitRepositoryId, request);
                return Ok(updated);
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
                _logger.LogError(ex, "An unexpected error occurred while updating a gitRepository.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpDelete("{gitRepositoryId}")]
        public async Task<IActionResult> DeleteGitRepository(Guid gitRepositoryId)
        {
            try
            {
                await _gitRepositoryService.Delete(gitRepositoryId);
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
                _logger.LogError(ex, "An unexpected error occurred while deleting a gitRepository.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
