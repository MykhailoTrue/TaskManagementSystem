using TMS.Dapper.Common.Attributes;

namespace TMS.Dapper.DAL.Entities.Abstract
{
    public class BaseEntityWithUpdatedCreatedDates : BaseEntity
    {
        [IgnoreUpdate]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
