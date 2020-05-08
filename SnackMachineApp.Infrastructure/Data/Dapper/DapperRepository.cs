using Dapper;
using System;
using System.Collections.Generic;
using System.Data;

namespace SnackMachineApp.Infrastructure.Data
{
    public class QueryRepositor1y : DapperRepositor1y
    {

        public QueryRepositor1y(IDbConnection dbConnection):base(dbConnection)
        {
        }

    }

    public class DapperRepositor1y: IDisposable
    {
        private readonly IDbConnection _DbConnection;

        public DapperRepositor1y(IDbConnection dbConnection)
        {
            _DbConnection = dbConnection;
        }


        public IEnumerable<T> GetAll<T>(string query, object parameters = null)
        {
            return _DbConnection.Query<T>(query, parameters);
        }

        public T GetById<T>(string query, object parameters = null)
        {
            return _DbConnection.QuerySingle<T>(query, parameters);
        }

        public IEnumerable<T> Query<T>(string query, object parameters = null)
        {
            return _DbConnection.Query<T>(query, parameters);
        }

        public IEnumerable<T> Query<T,U>(string query, Func<T, U, T> func)
        {
            return _DbConnection.Query<T,U, T>(query, func);
        }
        
        public T QuerySingle<T>(string query, object parameters = null)
        {
            return _DbConnection.QuerySingle<T>(query, parameters);
        }

        public T QueryFirst<T>(string query, object parameters = null)
        {
            return _DbConnection.QueryFirst<T>(query, parameters);
        }

        public void Dispose()
        {
            if(_DbConnection?.State != ConnectionState.Closed)
                _DbConnection?.Close();
            _DbConnection?.Dispose();
        }
    }
}