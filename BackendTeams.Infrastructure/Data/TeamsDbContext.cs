using BackendTeams.Application.Interfaces;
using BackendTeams.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BackendTeams.Infrastructure.Data;

public class TeamsDbContext : DbContext, IApplicationDbContext
{
    public TeamsDbContext(DbContextOptions<TeamsDbContext> options) : base(options)
    {
    }

    public DbSet<GroupEntity> Groups { get; set; }
    public DbSet<MemberEntity> Members { get; set; }







    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("teams"); //SCHEMA

        base.OnModelCreating(modelBuilder);
    }
}