using AutoMapper;
using TMS.Dapper.Common.DTOs.Workspaces.CRUD;
using TMS.Dapper.Common.DTOs.Workspaces.Custom;
using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.BLL.MappingProfiles
{
    public class WorkspaceProfile : Profile
    {
        public WorkspaceProfile()
        {
            CreateMap<Workspace, WorkspaceReadDTO>();
            CreateMap<Workspace, WorkspaceWithProjectsDTO>();
            CreateMap<WorkspaceCreateDTO, Workspace>()
                .ForMember(w => w.CreatedAt, src => src.MapFrom(_ => DateTime.UtcNow))
                .ForMember(w => w.UpdatedAt, src => src.MapFrom(_ => DateTime.UtcNow));

            CreateMap<WorkspaceUpdateDTO, Workspace>()
                .ForMember(w => w.UpdatedAt, src => src.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
