using BackendTeams.Domain.Entities;

namespace BackendTeams.Application.Interfaces;

public interface IMemberService
{
    Task<bool> AddMemberAsync(string groupId, string userId);
    Task<IEnumerable<MemberEntity>> GetMembersByGroupIdAsync(string groupId);
}