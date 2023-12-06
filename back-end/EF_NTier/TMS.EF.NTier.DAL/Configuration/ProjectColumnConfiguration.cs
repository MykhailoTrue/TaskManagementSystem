using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.DAL.Configuration
{
    public class ProjectColumnConfiguration : IEntityTypeConfiguration<ProjectColumn>
    {
        public void Configure(EntityTypeBuilder<ProjectColumn> builder)
        {
            builder.HasOne(d => d.Project).WithMany(p => p.ProjectColumns)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectColumns_To_Projects");
        }
    }
}
