using Hourly.Shared.Contracts.Requests.WorkSessionRequests;
using Hourly.Shared.Contracts.Responses.WorkSessionResponses;
using Hourly.Shared.Entities;

namespace Hourly.Shared.Mappers
{
    public static partial class WorkSessionMapper
    {
        public static WorkSessionResponse ToResponse(this WorkSession entity)
        {
            return new WorkSessionResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                User = entity.User?.ToSummaryResponse(),
                TaskDescription = entity.TaskDescription,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Factor = entity.Factor,
                Duration = entity.Duration,
                WBSO = entity.WBSO,
                OtherRemarks = entity.OtherRemarks,
                GitCommits = entity.GitCommits.Select(gc => gc.ToSummaryResponse()).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static WorkSessionSummaryResponse ToSummaryResponse(this WorkSession entity)
        {
            return new WorkSessionSummaryResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                TaskDescription = entity.TaskDescription,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Factor = entity.Factor,
                Duration = entity.Duration,
                WBSO = entity.WBSO,
                OtherRemarks = entity.OtherRemarks,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static WorkSession ToWorkSession(this CreateWorkSessionRequest request)
        {
            return new WorkSession
            {
                UserId = request.UserId,
                TaskDescription = request.TaskDescription,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Factor = request.Factor,
                WBSO = request.WBSO,
                OtherRemarks = request.OtherRemarks
            };
        }

        public static WorkSession ToWorkSession(this UpdateWorkSessionRequest request, Guid id)
        {
            return new WorkSession
            {
                UserId = request.UserId,
                TaskDescription = request.TaskDescription,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Factor = request.Factor,
                WBSO = request.WBSO,
                OtherRemarks = request.OtherRemarks
            };
        }
    }
}
