using TMS.Dapper.DAL.Entities.Abstract;

namespace TMS.Dapper.DAL.Entities
{
    public class ProjectMember : BaseEntity
    {
        public int ProjectId { get; set; }
        public Project? Project { get; set; }

        public int MemberId { get; set; }
        public User? Member { get; set; }
    }
}
