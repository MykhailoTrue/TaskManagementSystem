using System.Data;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbTransaction _transaction;

        public IUserRepository UserRepository { get; }
        public IWorkspaceRepository WorkspaceRepository { get; }
        public IProjectRepository ProjectRepository { get; }
        public IProjectCategoryRepository ProjectCategoryRepository { get; }

        public UnitOfWork(
            IUserRepository userRepository,
            IWorkspaceRepository workspaceRepository,
            IProjectRepository projectRepository,
            IProjectCategoryRepository projectCategoryRepository,
            IDbTransaction transaction)
        {
            UserRepository = userRepository;
            WorkspaceRepository = workspaceRepository;
            ProjectRepository = projectRepository;
            ProjectCategoryRepository = projectCategoryRepository;
            _transaction = transaction;
        }

        public void Commit()
        {
            try 
            { 
                _transaction.Commit(); 
            } 
            catch 
            {
                _transaction.Rollback();
            }

        }

        public void Dispose()
        {
            _transaction.Connection?.Close();
            _transaction.Connection?.Dispose();
            _transaction.Dispose();
        }
    }
}
