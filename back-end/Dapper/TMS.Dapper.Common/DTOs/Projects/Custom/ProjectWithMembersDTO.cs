using TMS.Dapper.Common.DTOs.Users.CRUD;

namespace TMS.Dapper.Common.DTOs.Projects.Custom
{
    public class ProjectWithMembersDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public List<UserReadDto> Members { get; set; }
    }
}
