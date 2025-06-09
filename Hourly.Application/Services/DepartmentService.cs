using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using Hourly.Domain.Mappers;
using Hourly.Shared.Contracts.Requests.DepartmentRequests;
using Hourly.Shared.Contracts.Responses.DepartmentResponses;
using Hourly.Shared.Exceptions;

namespace Hourly.Domain.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<DepartmentResponse> GetById(Guid departmentId)
        {
            var result = await _repository.GetById(departmentId) as Department
                ?? throw new EntityNotFoundException("Department not found!");

            return result.ToResponse();
        }

        public async Task<IEnumerable<DepartmentSummaryResponse>> GetAll()
        {
            var result = await _repository.GetAll() as List<Department> ?? new();
            return result.Select(d => d.ToSummaryResponse());
        }

        public async Task<DepartmentResponse> Create(CreateDepartmentRequest request)
        {
            var department = request.ToDepartment();
            department.Id = Guid.NewGuid();
            department.CreatedAt = DateTime.UtcNow;

            department.Validate();

            var result = await _repository.Create(department) as Department
                ?? throw new InvalidOperationException("Failed to create Department.");

            return result.ToResponse();
        }

        public async Task<DepartmentResponse> Update(Guid id, UpdateDepartmentRequest request)
        {
            var existing = await _repository.GetById(id) as Department
                ?? throw new EntityNotFoundException("Department not found!");

            var updated = request.ToDepartment(id);
            updated.UpdatedAt = DateTime.UtcNow;

            updated.Validate();
            existing.Update(updated);

            var result = await _repository.Update(existing) as Department
                ?? throw new InvalidOperationException("Failed to update Department.");

            return result.ToResponse();
        }

        public async Task Delete(Guid departmentId)
        {
            await _repository.Delete(departmentId);
        }
    }
}
