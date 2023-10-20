using System.ComponentModel.DataAnnotations;

namespace TMS.EF.NTier.DAL.Entities;

public partial class IssueType
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int ProjectId { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual Project Project { get; set; } = null!;
}
