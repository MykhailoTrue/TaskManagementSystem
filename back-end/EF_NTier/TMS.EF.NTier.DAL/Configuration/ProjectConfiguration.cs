using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.DAL.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK_Project_Id");

            // can't configure without principal entity
            /*builder.HasOne(d => d.ProjectCategory).WithMany(p => p.Projects)
                .HasForeignKey(d => d.ProjectCategoryId)
                .HasConstraintName("FK_Projects_To_Categories");*/

            // can't configure without principal entity
            /*builder.HasOne(d => d.Workspace).WithMany(p => p.Projects)
                .HasForeignKey(d => d.WorkspaceId)
                .HasConstraintName("FK_Projects_To_Workspaces");*/
        }
    }
}
