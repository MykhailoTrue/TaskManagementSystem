using System.ComponentModel.DataAnnotations;

namespace TMS.EF.NTier.Common.DTO.Issues
{
    public class IssueReadDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Descritption { get; set; }

        public int ProjectColumnId { get; set; }

        public int IssueTypeId { get; set; }

        public int AsigneeId { get; set; }
    }
}
