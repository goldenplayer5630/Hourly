namespace Hourly.Abstractions.Entities
{
    public interface IUser
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
        Guid RoleId { get; }
        IRole Role { get; }

        Guid? DepartmentId { get; }
        IDepartment? Department { get; }

        string? GitEmail { get; }
        string? GitUsername { get; }
        string? GitAccessToken { get; }

        float TVTHourBalance { get; }

        IReadOnlyCollection<IGitCommit> GitCommits { get; }
        IReadOnlyCollection<IUserContract> Contracts { get; }

        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }

        void AssignToDepartment(IDepartment department);
        void RemoveFromDepartment();
        void AssignToRole(IRole role);
    }
}
