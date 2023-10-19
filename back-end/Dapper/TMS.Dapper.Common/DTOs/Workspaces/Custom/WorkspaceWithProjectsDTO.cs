using TMS.Dapper.Common.DTOs.Projects.CRUD;

namespace TMS.Dapper.Common.DTOs.Workspaces.Custom
{
    public class WorkspaceWithProjectsDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public List<ProjectReadDTO> Projects { get; set; }
    }
}
