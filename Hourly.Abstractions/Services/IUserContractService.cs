using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Services
{
    public interface IUserContractService
    {
        Task<IEnumerable<UserContract>> GetAll();
        Task<UserContract> GetById(Guid userContractId);
        Task<UserContract> Create(UserContract userContract);
        Task<UserContract> Update(UserContract userContract);
        Task Delete(Guid userContractId);
    }
}
