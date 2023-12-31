﻿using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.DAL.Repositories.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<Project?> GetProjectWithMembersAsync(int id);
        Task<Project?> GetByIdWithCategoryAsync(int id);
        Task<IEnumerable<Project>> GetAllWithCategoryAsync();
    }
}
