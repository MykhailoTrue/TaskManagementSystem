namespace TMS.Dapper.Common.DTOs.Workspaces.CRUD
{
    public class WorkspaceReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
