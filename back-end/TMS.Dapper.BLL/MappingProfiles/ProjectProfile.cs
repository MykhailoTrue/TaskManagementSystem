using AutoMapper;
using TMS.Dapper.Common.DTOs.Projects.CRUD;
using TMS.Dapper.Common.DTOs.Projects.Custom;
using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.BLL.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectReadDTO>();
            CreateMap<Project, ProjectWithCategoryDTO>()
                .ForMember(p => p.Category, src => src.MapFrom(s => s.ProjectCategory != null ? s.ProjectCategory.Name : ""));
        }
    }
}
