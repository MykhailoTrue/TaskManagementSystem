namespace TMS.Dapper.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IWorkspaceRepository WorkspaceRepository { get; }
        IProjectRepository ProjectRepository { get; }
        IProjectCategoryRepository ProjectCategoryRepository { get; }
        void Commit();
    }
}
