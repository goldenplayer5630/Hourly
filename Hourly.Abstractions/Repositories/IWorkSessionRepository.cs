using Hourly.Abstractions.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IWorkSessionRepository
    {
        Task<IWorkSession?> GetById(Guid IWorkSessionId);
        Task<IEnumerable<IWorkSession>> GetAll();
        Task<IEnumerable<IWorkSession>> Filter(Guid? userContractId, int? year, int? month, bool? wbso);
        Task<IWorkSession> Create(IWorkSession IWorkSession);
        Task<IWorkSession> Update(IWorkSession IWorkSession);
        Task Delete(Guid IWorkSessionId);
        Task SaveChanges();
    }
}
