using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IUserContractRepository
    {
        Task<UserContract?> GetById(Guid userContractId);
        Task<IEnumerable<UserContract>> GetAll();
        Task<UserContract> Create(UserContract userContract);
        Task<UserContract> Update(UserContract userContract);
        Task Delete(Guid userContractId);
        Task SaveChanges();
    }
}
