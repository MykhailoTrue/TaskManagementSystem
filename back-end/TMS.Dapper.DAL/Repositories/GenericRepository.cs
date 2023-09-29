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
        private readonly IDbConnection _connection;
        private readonly string _tableName;

        public GenericRepository(IDbConnection connection, string? tableName = null)
        {
            _connection = connection;
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
            var entities = await _connection.QueryAsync<T>($"SELECT * FROM dbo.[{_tableName}]");
            return entities;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM dbo.[{_tableName}] " +
                "WHERE Id = @Id";
            var entity = await _connection.QuerySingleOrDefaultAsync<T>(sql,
                new 
                { 
                    @Id = id,
                });
            return entity;
        }

        public async Task<int> CreateAsync(T entity)
        {
            var sql = GenerateInsertQuery();

            var entityId = await _connection.ExecuteScalarAsync<int>(sql,
                param: entity);

            return entityId;
        }

        public async Task<int> CreateRangeAsync(IEnumerable<T> entities)
        {
            var sql = GenerateInsertQuery();
            var insertedRows = await _connection.ExecuteAsync(sql,
                param: entities);

            return insertedRows;
        }

        public async Task UpdateAsync(T entity)
        {
            var sql = GenerateUpdateQuery();
            await _connection.ExecuteAsync(sql,
                param: entity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = $"DELETE FROM dbo.[{_tableName}] Where Id=@Id";
            var rowsAffected = await _connection.ExecuteAsync(sql,
                param : new 
                { 
                    @Id = id 
                });

            return rowsAffected;
        }

        public void Dispose()
        {
            _connection.Dispose();
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
