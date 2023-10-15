using AutoMapper;
using TMS.Dapper.Common.DTOs.ProjectCategories.CRUD;
using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.BLL.MappingProfiles
{
    public class ProjectCategoryProfile : Profile
    {
        public ProjectCategoryProfile()
        {
            CreateMap<ProjectCategory,  ProjectCategoryReadDto>();
        }
    }
}
