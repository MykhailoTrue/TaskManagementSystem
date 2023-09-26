using TMS.Dapper.DAL.Entities.Abstract;

namespace TMS.Dapper.DAL.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }

        public Workspace? Workspace { get; set; }
        public int WorkspaceId { get; set; }

        public User? ProjectLead { get; set; }
        public int? ProjectLeadId { get; set; }

        public ProjectCategory? ProjectCategory { get; set; }
        public int? ProjectCategoryId { get; set; }

    }
}
