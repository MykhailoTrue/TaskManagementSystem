using TaskManagementSystem.Ado.Dall.Entities.Abstract;

namespace TaskManagementSystem.Ado.Dall.Entities
{
    public class Workspace : BaseEntityWithUpdatedCreatedDates
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        
        public User Author { get; set; }
        public int AutherId { get; set; }

    }
}
