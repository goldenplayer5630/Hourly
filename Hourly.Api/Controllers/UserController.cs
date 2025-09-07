using Hourly.Abstractions.Services;
using Hourly.Application.Services;
using Hourly.Domain.Contracts.Requests.UserRequests;
using Hourly.Domain.Exceptions;
using Hourly.Domain.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Hourly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Secure all endpoints in this controller. If you need some to be public, move [Authorize] down to specific actions.
    [Authorize(Policy = "ApiScope")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // ===== Helpers to read identity from AAD token =====
        private static string? Claim(ClaimsPrincipal user, string type)
            => user.Claims.FirstOrDefault(c => c.Type == type)?.Value;

        /// <summary>
        /// Returns (ExternalOid, email, name) from the bearer token.
        /// </summary>
        private (Guid externalOid, string? email, string? name) ReadIdentity()
        {
            var oid = Claim(User, "oid") ?? Claim(User, ClaimTypes.NameIdentifier)
                      ?? throw new UnauthorizedAccessException("Missing 'oid' claim.");
            var email = Claim(User, "preferred_username") ?? Claim(User, ClaimTypes.Email);
            var name = Claim(User, "name") ?? Claim(User, ClaimTypes.Name);

            return (Guid.Parse(oid), email, name);
        }

        // ===== New auth-coupled endpoints =====

        /// <summary>
        /// Creates the current user if not present, or refreshes profile fields if it exists.
        /// </summary>
        [HttpPost("me/bootstrap")]
        public async Task<IActionResult> BootstrapMe()
        {
            try
            {
                var (externalOid, email, name) = ReadIdentity();
                var user = await _userService.BootstrapOrUpdate(externalOid, email, name);
                return Ok(user.ToResponse());
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while bootstrapping current user.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /// <summary>
        /// Returns the current (bootstrapped) user.
        /// </summary>
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            try
            {
                var (externalOid, _, _) = ReadIdentity();
                var user = await _userService.GetByExternalOid(externalOid);
                if (user is null) return NotFound("User not bootstrapped.");
                return Ok(user.ToResponse());
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while retrieving current user.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        // ===== Existing endpoints (now protected by [Authorize]) =====

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
                _logger.LogError(ex, "An unexpected error occurred while retrieving users.");
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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

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
                _logger.LogError(ex, "An unexpected error occurred while creating a user.");
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
            if (!ModelState.IsValid) return BadRequest(ModelState);

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
