using Microsoft.EntityFrameworkCore;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.DAL.Context;

public partial class TaskManagementSystemDbContext : DbContext
{
    public TaskManagementSystemDbContext(DbContextOptions<TaskManagementSystemDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Issue> Issues { get; set; }

    public virtual DbSet<IssueType> IssueTypes { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectColumn> ProjectColumns { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Configure();

        modelBuilder.Seed();
    }
}
