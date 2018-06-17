using System;
using System.Data;
using System.Data.SqlClient;

namespace DesignPatterns.Disposable_Pattern
{
    // Couple of things to notice here.
    //
    // 1. We are creating type that holds instance of a type that implements IDisposable interface
    //  this should ring us a bell that we should implement IDisposable for our type as well!
    //
    // 2. Notice how we are using this type but we are not closing the connection anywhere, this can be done for optimization
    //  purposes. ( Having to close and open MySQL connection each time is relatively expensive so we want to keep the connection open
    //  and re-use it for later ) - Check Object pool pattern. Whats the problem with this? Well if we are running our application on multiple instances
    //  concurrently we might overflow the connection pool set to our SQL Server and this makes our application not scalable.
    //  But this class implements IDisposable so we are closing the connection.
    public class DatabaseState : IDisposable
    {
        private IDbConnection _connection;

        public string GetDate()
        {
            if (this._connection == null)
            {
                // Dummy operations
                _connection = new SqlConnection("Server=.\\SQLEXPRESS;Database=master;Integrated Security=SSPI;App=IDisposablePattern");
                _connection.Open();
            }
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "SELECT getdate()";
                return command.ExecuteScalar().ToString();
            }
        }

        /// <summary>
        ///     This method gets called when <see cref="DatabaseState"/> object
        ///     is created within using statement.
        /// </summary>
        /// <remarks>
        ///     This should right a bell however, if we are not using our <see cref="DatabaseState"/> instance
        ///     like we are supposed to we will never close our connections as said in point 3.
        ///
        ///     This is actually not the best practice of implementing IDisposable.
        ///     Refer to <see cref="DatabaseStateImpr"/> for better implementation.
        /// </remarks>
        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
