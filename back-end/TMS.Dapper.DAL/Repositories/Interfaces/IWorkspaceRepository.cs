using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.DAL.Repositories.Interfaces
{
    public interface IWorkspaceRepository : IGenericRepository<Workspace>
    {
        /// <summary>
        /// Get worksapce with projects.
        /// Use ADO.NET
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <returns>Workspace with projects</returns>
        Task<Workspace?> GetWorkspaceWithProjectsAsync(int workspaceId);
    }
}
