namespace Hourly.Abstractions.Entities
{
    public interface IDepartment
    {
        Guid Id { get; }
        string Name { get; }
        ICollection<IUser> Users { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
