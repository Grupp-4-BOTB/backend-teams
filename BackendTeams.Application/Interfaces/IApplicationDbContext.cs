using BackendTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendTeams.Application.Interfaces;



//DENNA LÖSER KOPPLING MELLAN SERVICE OCH DBCONTEXT EFTERSOM JAG INTE VILLE FÖRSTÖRA CLEAN ARCHITECTURE GENOM FEL DEPENDENCY
public interface IApplicationDbContext
{
    DbSet<MemberEntity> Members { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}