namespace Hourly.Abstractions.Entities
{
    public interface IGitCommit
    {
        Guid Id { get; }
        Guid RepositoryId { get; }
        IGitRepository Repository { get; }
        string ExtCommitId { get; }
        string ExtCommitShortId { get; }
        string Title { get; }
        string? Comment { get; }
        Guid AuthorId { get; }
        IUser Author { get; }
        DateTime AuthoredDate { get; }
        string WebUrl { get; }

        IReadOnlyCollection<IWorkSession> WorkSessions { get; }

        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}
