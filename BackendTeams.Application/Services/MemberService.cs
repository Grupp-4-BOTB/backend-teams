using BackendTeams.Application.Interfaces;
using BackendTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTeams.Application.Services;

public class MemberService : IMemberService
{
    // Byt ut TeamsDbContext mot ditt nya interface här:
    private readonly IApplicationDbContext _context;

    // Ändra även här i konstruktorn:
    public MemberService(IApplicationDbContext context)
    {
        _context = context;
    }



    //LÄGGER TILL MEDLEMAMR I GRUPPEN
    public async Task<bool> AddMemberAsync(string groupId, string userId)
    {
        var exists = await _context.Members.AnyAsync(m => m.GroupId == groupId && m.UserId == userId);
        if (exists) return true;

        _context.Members.Add(new MemberEntity { GroupId = groupId, UserId = userId });
        return await _context.SaveChangesAsync() > 0;
    }


    public async Task<IEnumerable<MemberEntity>> GetMembersByGroupIdAsync(string groupId)
    {
        return await _context.Members.Where(m => m.GroupId == groupId).ToListAsync();
    }
}