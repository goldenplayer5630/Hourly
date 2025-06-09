namespace Hourly.Abstractions.Entities
{
    public interface IRole
    {
        Guid Id { get; }
        string Name { get; }
        string Permissions { get; } // JSON string representing permission set
        ICollection<IUser> Users { get; }

        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
