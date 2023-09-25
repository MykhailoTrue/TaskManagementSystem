namespace TaskManagementSystem.Ado.Dall.Entities.Abstract
{
    public class BaseEntityWithUpdatedCreatedDates : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
