using AutoMapper;
using TMS.Dapper.Common.DTOs.Workspaces.Custom;
using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.BLL.MappingProfiles
{
    public class WorkspaceProfile : Profile
    {
        public WorkspaceProfile()
        {
            CreateMap<Workspace, WorkspaceWithProjectsDTO>();
        }
    }
}
