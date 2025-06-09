using Hourly.Abstractions.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IUserContractRepository
    {
        Task<IUserContract?> GetById(Guid IUserContractId);
        Task<IEnumerable<IUserContract>> FilterUserContracts(Guid? userId, int? year, int? month);
        Task<IEnumerable<IUserContract>> GetAll();
        Task<IUserContract> Create(IUserContract IUserContract);
        Task<IUserContract> Update(IUserContract IUserContract);
        Task Delete(Guid IUserContractId);
        Task SaveChanges();
    }
}
