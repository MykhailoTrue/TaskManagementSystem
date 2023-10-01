using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;
using TMS.Dapper.DAL.Context;
using TMS.Dapper.DAL.Entities.Abstract;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DapperContext _context;
        protected readonly string _tableName;

        public GenericRepository(DapperContext context, string? tableName = null)
        {
            this._context = context;
            if (tableName is not null)
            {
                _tableName = tableName;
            }
            else
            {
                _tableName = GetTableName();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM dbo.[{_tableName}]";

            using (var connection = _context.CreateConnection())
            {
                var entities = await connection.QueryAsync<T>(query);
                return entities;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = $"SELECT * FROM dbo.[{_tableName}] " +
                "WHERE Id = @Id";

            using (var connection = _context.CreateConnection())
            {
                var entity = await connection.QuerySingleOrDefaultAsync<T>(query,
                    new 
                    {
                        @Id = id,
                    });
                return entity;
            }
        }

        public async Task<int> CreateAsync(T entity)
        {
            var query = GenerateInsertQuery();

            using (var connection = _context.CreateConnection())
            {
                var entityId = await connection.ExecuteScalarAsync<int>(query,
                    param: entity);

                return entityId;
            }
        }

        public async Task<int> CreateRangeAsync(IEnumerable<T> entities)
        {
            var query = GenerateInsertQuery();

            using (var connection = _context.CreateConnection())
            {
                var insertedRows = await connection.ExecuteAsync(query,
                param: entities);

                return insertedRows;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            var query = GenerateUpdateQuery();

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,
                param: entity);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = $"DELETE FROM dbo.[{_tableName}] Where Id=@Id";

            using (var connection = _context.CreateConnection())
            {
                var rowsAffected = await connection.ExecuteAsync(query,
                param: new
                {
                    @Id = id
                });

                return rowsAffected;
            }
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE dbo.[{_tableName}] SET ");

            var properties = GetEntityProperties();
            properties.Remove("Id");
            
            properties.ForEach(p => updateQuery.Append($"[{p}]=@{p}, "));
            updateQuery
                .Remove(updateQuery.Length - 2, 2)
                .Append(" WHERE Id=@Id"); ;

            return updateQuery.ToString();
        }

        private string GetTableName()
        {
            var type = typeof(T);
            var tableAttr = type.GetCustomAttribute<TableAttribute>(true);
            if (tableAttr != null)
            {
                return tableAttr.Name;
            }

            return $"{type.Name}s";
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO dbo.[{_tableName}] (");

            var properties = GetEntityProperties();
            properties.Remove("Id");
            properties.ForEach(p => insertQuery.Append($"[{p}], "));
            insertQuery
                .Remove(insertQuery.Length - 2, 2)
                .Append(") VALUES (");

            properties.ForEach(p => insertQuery.Append($"@{p}, "));
            insertQuery
                .Remove(insertQuery.Length - 2, 2)
                .Append("); ")
                .Append("SELECT SCOPE_IDENTITY()"); // get entity id

            return insertQuery.ToString();
        }
        private List<string> GetEntityProperties()
        {
            var prop = typeof(T).GetProperties();
            return prop
                .Where(p =>
                {
                    var attributes = p.GetCustomAttributes(typeof(NotMappedAttribute), true);
                    return attributes.Length <= 0;
                })
                .Select(p => p.Name)
                .ToList();
        }
    }
}
