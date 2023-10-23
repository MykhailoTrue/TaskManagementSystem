using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TMS.EF.NTier.DAL.Context;
using TMS.EF.NTier.DAL.Repositories.Interfaces;

namespace TMS.EF.NTier.DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TaskManagementSystemDbContext _context;

        public GenericRepository(TaskManagementSystemDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> FindAll()
        {
            return _context.Set<T>()
                .AsNoTracking();
        }

        public IQueryable<T?> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>()
                .Where(expression)
                .AsNoTracking();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
