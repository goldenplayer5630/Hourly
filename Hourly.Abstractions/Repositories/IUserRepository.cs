using Hourly.Abstractions.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<IUser?> GetById(Guid IUserId);
        Task<IEnumerable<IUser>> GetAll();
        Task<IUser> Create(IUser IUser);
        Task<IUser> Update(IUser IUser);
        Task Delete(Guid IUserId);
        Task SaveChanges();
    }
}
