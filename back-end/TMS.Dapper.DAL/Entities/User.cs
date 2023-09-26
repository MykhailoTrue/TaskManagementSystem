using TMS.Dapper.DAL.Entities.Abstract;

namespace TMS.Dapper.DAL.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public DateTime BirhtDate { get; set; }

    }
}
