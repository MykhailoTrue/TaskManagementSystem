namespace TMS.Dapper.Common.DTOs.ProjectCategories.CRUD
{
    public class ProjectCategoryUpdateDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int WorkspaceId { get; set; }
    }
}
