namespace Hourly.Tests.Data.Repositories
{
    internal interface IRepositoryTests
    {
        Task GetAll_ShouldReturnAllEntities();
        Task GetById_ShouldReturnEntity_WhenExists();
        Task GetById_ShouldReturnNull_WhenNotExists();
        Task Create_ShouldAddEntity();
        Task Update_ShouldUpdateEntity();
        Task Delete_ShouldDeleteEntity();
    }
}
