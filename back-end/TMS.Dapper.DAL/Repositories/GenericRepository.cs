using Dapper;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;
using TMS.Dapper.DAL.Entities.Abstract;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.DAL.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly string _tableName;
        protected readonly IDbConnection _connection;
        protected readonly IDbTransaction _transaction;

        public GenericRepository(
            IDbConnection connection,
            IDbTransaction transaction,
            string tableName)
        {
            _connection = connection;
            _transaction = transaction;

            if (tableName is not null)
            {
                _tableName = tableName;
            }
            else
            {
                _tableName = GenericRepository<T>.GetTableName();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = $"SELECT * FROM dbo.[{_tableName}]";

            var entities = await _connection.QueryAsync<T>(
                    query,
                    transaction: _transaction);

            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var query = $"SELECT * FROM dbo.[{_tableName}] " +
                "WHERE Id = @Id";

            var entity = await _connection.QuerySingleOrDefaultAsync<T>(
                    query,
                    param: new
                    {
                        @Id = id,
                    },
                    transaction: _transaction);
            return entity;
        }

        public async Task<int> CreateAsync(T entity)
        {
            var query = GenerateInsertQuery();

            var entityId = await _connection.ExecuteScalarAsync<int>(query,
                    param: entity,
                    transaction: _transaction);

            return entityId;

        }

        public async Task<int> CreateRangeAsync(IEnumerable<T> entities)
        {
            var query = GenerateInsertQuery();

            var insertedRows = await _connection.ExecuteAsync(query,
                param: entities,
                transaction: _transaction);

            return insertedRows;
        }

        public async Task UpdateAsync(T entity)
        {
            var query = GenerateUpdateQuery();

            await _connection.ExecuteAsync(query,
                param: entity,
                transaction: _transaction);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var query = $"DELETE FROM dbo.[{_tableName}] Where Id=@Id";

            var rowsAffected = await _connection.ExecuteAsync(query,
                param: new
                {
                    @Id = id
                },
                transaction: _transaction);

            return rowsAffected;
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

        private static string GetTableName()
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
        private static List<string> GetEntityProperties()
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
