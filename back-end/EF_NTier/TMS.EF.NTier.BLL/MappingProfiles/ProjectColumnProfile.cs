using AutoMapper;
using TMS.EF.NTier.Common.DTO.ProjectColumns;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.BLL.MappingProfiles
{
    public class ProjectColumnProfile : Profile
    {
        public ProjectColumnProfile()
        {
            CreateMap<ProjectColumnUpdateDTO, ProjectColumn>();
            CreateMap<ProjectColumnCreateDTO, ProjectColumn>();

            CreateMap<ProjectColumn, ProjectColumnReadDTO>();

            CreateMap<ProjectColumn, ProjectColumnWithIssuesDTO>();
        }
    }
}
