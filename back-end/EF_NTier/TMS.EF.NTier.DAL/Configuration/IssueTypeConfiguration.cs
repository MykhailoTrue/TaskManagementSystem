using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.DAL.Configuration
{
    public class IssueTypeConfiguration : IEntityTypeConfiguration<IssueType>
    {
        public void Configure(EntityTypeBuilder<IssueType> builder)
        {
            builder.HasOne(d => d.Project).WithMany(p => p.IssueTypes)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_IssueTypes_To_Projects");
        }
    }
}
