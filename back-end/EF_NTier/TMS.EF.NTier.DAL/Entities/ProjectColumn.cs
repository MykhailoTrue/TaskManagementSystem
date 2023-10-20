namespace TMS.EF.NTier.DAL.Entities;

public partial class ProjectColumn
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int ProjectId { get; set; }

    public virtual ICollection<Issue> Issues { get; set; } = new List<Issue>();

    public virtual Project Project { get; set; } = null!;
}
