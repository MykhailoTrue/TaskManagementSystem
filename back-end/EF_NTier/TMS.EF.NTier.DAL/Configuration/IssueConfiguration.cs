using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.DAL.Configuration
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            // can't configure without principal entity
            /*builder.HasOne(d => d.Asignee).WithMany(p => p.Issues)
                .HasForeignKey(d => d.AsigneeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Issues_To_Users_AsigneeId");*/

            builder.HasOne(d => d.IssueType).WithMany(p => p.Issues)
                .HasForeignKey(d => d.IssueTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Issues_To_IssueTypes");

            builder.HasOne(d => d.ProjectColumn).WithMany(p => p.Issues)
                .HasForeignKey(d => d.ProjectColumnId)
                .HasConstraintName("FK_Issues_To_ProjectColumns");
        }
    }
}
