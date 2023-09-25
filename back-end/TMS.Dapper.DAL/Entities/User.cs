using TaskManagementSystem.Ado.Dall.Entities.Abstract;

namespace TaskManagementSystem.Ado.Dall.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public DateTime BirhtDate { get; set; }

    }
}
