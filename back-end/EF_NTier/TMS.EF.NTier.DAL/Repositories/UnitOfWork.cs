using TMS.EF.NTier.DAL.Context;
using TMS.EF.NTier.DAL.Repositories.Interfaces;

namespace TMS.EF.NTier.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagementSystemDbContext _context;
        private bool _disposed;
        public UnitOfWork( 
            TaskManagementSystemDbContext context,
            IProjectColumnRepository projectColumnRepository)
        {
            _context = context;
            ProjectColumnRepository = projectColumnRepository;   
        }

        public IProjectColumnRepository ProjectColumnRepository { get; }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
            }

            _disposed = true;

            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                await _context.DisposeAsync();
            }

            _disposed = true;

            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
