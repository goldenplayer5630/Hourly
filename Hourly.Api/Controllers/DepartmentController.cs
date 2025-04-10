using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hourly.Shared.Entities;
using Hourly.Abstractions.Services;

namespace Hourly.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAll();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(Guid id)
        {
            var department = await _departmentService.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return Ok(department);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _departmentService.Create(department);
            return Created();
            //return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartment);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] Department department)
        //{
        //    var existingDepartment = await _departmentService.GetById(id);

        //    if (existingDepartment == null)
        //    {
        //        return NotFound();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var updatedDepartment = await _departmentService.Update(department);

        //    return Ok(updatedDepartment);
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            await _departmentService.Delete(id);

            return Ok();
        }
    }
}
