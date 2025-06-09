using Hourly.Shared.Contracts.Requests.UserContractRequests;
using Hourly.Shared.Contracts.Responses.UserContractResponses;

namespace Hourly.Abstractions.Services
{
    public interface IUserContractService
    {
        Task<IEnumerable<UserContractSummaryResponse>> GetAll();
        Task<IEnumerable<UserContractSummaryResponse>> FilterUserContracts(Guid? userId, int? year, int? month);
        Task<UserContractResponse> GetById(Guid userContractId);
        Task<UserContractResponse> Create(CreateUserContractRequest userContract);
        Task<UserContractResponse> Update(Guid id, UpdateUserContractRequest userContract);
        Task Delete(Guid userContractId);
    }
}
