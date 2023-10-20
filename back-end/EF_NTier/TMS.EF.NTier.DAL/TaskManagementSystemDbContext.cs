using Microsoft.EntityFrameworkCore;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.DAL;

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
        modelBuilder.Entity<Issue>(entity =>
        {
            // can't configure without principal entity
            /*entity.HasOne(d => d.Asignee).WithMany(p => p.Issues)
                .HasForeignKey(d => d.AsigneeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Issues_To_Users_AsigneeId");*/

            entity.HasOne(d => d.IssueType).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IssueTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Issues_To_IssueTypes");

            entity.HasOne(d => d.ProjectColumn).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ProjectColumnId)
                .HasConstraintName("FK_Issues_To_ProjectColumns");
        });

        modelBuilder.Entity<IssueType>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.IssueTypes)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_IssueTypes_To_Projects");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Project_Id");

            // can't configure without principal entity
            /*entity.HasOne(d => d.ProjectCategory).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectCategoryId)
                .HasConstraintName("FK_Projects_To_Categories");*/

            // can't configure without principal entity
            /*entity.HasOne(d => d.Workspace).WithMany(p => p.Projects)
                .HasForeignKey(d => d.WorkspaceId)
                .HasConstraintName("FK_Projects_To_Workspaces");*/
        });

        modelBuilder.Entity<ProjectColumn>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.ProjectColumns)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProjectColumns_To_Projects");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
