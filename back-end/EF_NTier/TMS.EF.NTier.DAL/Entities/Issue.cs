using System.ComponentModel.DataAnnotations;

namespace TMS.EF.NTier.DAL.Entities;

public partial class Issue
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public string? Descritption { get; set; }

    public int ProjectColumnId { get; set; }

    public int IssueTypeId { get; set; }

    public int AsigneeId { get; set; }

    //public virtual User Asignee { get; set; } = null!;

    //public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual IssueType IssueType { get; set; } = null!;

    public virtual ProjectColumn ProjectColumn { get; set; } = null!;
}


