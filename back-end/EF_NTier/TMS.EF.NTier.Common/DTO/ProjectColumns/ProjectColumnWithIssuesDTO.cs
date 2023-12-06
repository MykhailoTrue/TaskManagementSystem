using TMS.EF.NTier.Common.DTO.Issues;

namespace TMS.EF.NTier.Common.DTO.ProjectColumns
{
    public class ProjectColumnWithIssuesDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public IEnumerable<IssueReadDTO> Issues { get; set; }

    }
}
