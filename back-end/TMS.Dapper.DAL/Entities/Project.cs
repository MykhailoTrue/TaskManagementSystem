using System.ComponentModel.DataAnnotations.Schema;
using TMS.Dapper.DAL.Entities.Abstract;

namespace TMS.Dapper.DAL.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }

        [NotMapped]
        public Workspace? Workspace { get; set; }
        public int WorkspaceId { get; set; }

        [NotMapped]
        public ProjectCategory? ProjectCategory { get; set; }
        public int? ProjectCategoryId { get; set; }

        [NotMapped]
        public List<User> Members { get; set; } = new List<User> ();

    }
}
