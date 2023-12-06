using AutoMapper;
using TMS.EF.NTier.Common.DTO.Issues;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.BLL.MappingProfiles
{
    public class IssueProfile : Profile
    {
        public IssueProfile()
        {
            CreateMap<Issue, IssueReadDTO>();
        }
    }
}
