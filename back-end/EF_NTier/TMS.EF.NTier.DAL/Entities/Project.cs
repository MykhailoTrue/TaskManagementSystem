namespace TMS.EF.NTier.DAL.Entities;

public partial class Project
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int WorkspaceId { get; set; }

    public int? ProjectCategoryId { get; set; }

    public virtual ICollection<IssueType> IssueTypes { get; set; } = new List<IssueType>();

    public virtual ICollection<ProjectColumn> ProjectColumns { get; set; } = new List<ProjectColumn>();

}
