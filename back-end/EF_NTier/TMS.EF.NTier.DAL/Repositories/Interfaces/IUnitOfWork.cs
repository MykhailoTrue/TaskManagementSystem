namespace TMS.EF.NTier.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        IProjectColumnRepository ProjectColumnRepository { get; }

        Task SaveAsync();
    }
}
