namespace DesignPatterns.Object_Pool_Pattern
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class PooledSqlConnection
    {
        private readonly SqlConnectionPool _pool =
            new SqlConnectionPool(() => new SqlConnection(_connectionString));
        private static string _connectionString;

        public PooledSqlConnection()
            : this("Server=.\\SQLEXPRESS;Database=master;Integrated Security=SSPI;App=IDisposablePattern") {}
        public PooledSqlConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public object Execute(IDbCommand cmd)
        {
            var connection = _pool.Allocate();
            try
            {
                connection.Open();
                cmd.Connection = connection;
                // do some stuff with it this is just an example
                return cmd.ExecuteScalar().ToString();
            }
            finally
            {
                _pool.Free(connection);
            }
        }
    }

    public class SqlConnectionPool : ObjectPool<SqlConnection>
    {
        public override void Free(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
            base.Free(connection);
        }

        public SqlConnectionPool(Func<SqlConnection> factory) : base(factory)
        {
        }
        public SqlConnectionPool(Func<SqlConnection> factory, int size) : base(factory, size)
        {
        }
    }
}
