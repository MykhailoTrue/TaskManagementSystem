using System.ComponentModel.DataAnnotations.Schema;
using TMS.Dapper.DAL.Entities.Abstract;

namespace TMS.Dapper.DAL.Entities
{
    public class ProjectMember : BaseEntity
    {
        public int ProjectId { get; set; }
        [NotMapped]
        public Project? Project { get; set; }

        public int MemberId { get; set; }
        [NotMapped]
        public User? Member { get; set; }
    }
}
