namespace Hourly.Abstractions.Entities
{
    public interface IGitRepository
    {
        Guid Id { get; }
        string ExtRepositoryId { get; }
        string Name { get; }
        string Namespace { get; }
        string WebUrl { get; }

        IReadOnlyCollection<IGitCommit> GitCommits { get; }

        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
