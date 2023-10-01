using System.ComponentModel.DataAnnotations.Schema;
using TMS.Dapper.DAL.Entities.Abstract;

namespace TMS.Dapper.DAL.Entities
{
    public class Workspace : BaseEntityWithUpdatedCreatedDates
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        [NotMapped]
        public User? Author { get; set; }
        public int AuthorId { get; set; }

    }
}
