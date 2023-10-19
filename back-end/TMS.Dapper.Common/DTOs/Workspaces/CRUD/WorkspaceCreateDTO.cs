namespace TMS.Dapper.Common.DTOs.Workspaces.CRUD
{
    public class WorkspaceCreateDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int AuthorId { get; set; }
    }
}
