using TMS.Dapper.Common.DTOs.Workspaces.Custom;

namespace TMS.Dapper.Common.DTOs.Users.Custom
{
    public class UserWithProjectsDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }

        public List<WorkspaceWithProjectsDTO> Workspaces { get; set; }
    }
}
