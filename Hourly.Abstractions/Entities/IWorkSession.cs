namespace Hourly.Abstractions.Entities
{
    public interface IWorkSession
    {
        Guid Id { get; }
        Guid UserContractId { get; }
        IUserContract UserContract { get; }
        string TaskDescription { get; }
        DateTime StartTime { get; }
        DateTime EndTime { get; }
        float BreakTime { get; }
        float Factor { get; }
        float TVTAccruedHours { get; }
        float TVTUsedHours { get; }
        bool WBSO { get; }
        bool Locked { get; }
        string? OtherRemarks { get; }
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }

        float RawEffectiveHours { get; }
        float NetEffectiveHours { get; }

        ICollection<IGitCommit> GitCommits { get; }

        void AddGitCommit(IGitCommit gitCommit);
        void RemoveGitCommit(IGitCommit gitCommit);
        void AssignToUserContract(IUserContract userContract);
        void Validate();
        void Update(IWorkSession updated);
    }
}
