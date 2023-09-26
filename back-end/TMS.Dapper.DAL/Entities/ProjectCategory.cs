using TMS.Dapper.DAL.Entities.Abstract;

namespace TMS.Dapper.DAL.Entities
{
    public class ProjectCategory : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        public Workspace? Workspace { get; set; }
        public int WorkspaceId { get; set; }
    }
}
