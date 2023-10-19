using System.ComponentModel.DataAnnotations.Schema;
using TMS.Dapper.DAL.Entities.Abstract;

namespace TMS.Dapper.DAL.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }

        [NotMapped]
        public List<Workspace> Workspaces { get; set; } = new List<Workspace>();

    }
}
