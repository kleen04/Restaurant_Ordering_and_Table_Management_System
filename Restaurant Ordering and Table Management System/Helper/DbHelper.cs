using System;
using System.Data;
using MySql.Data.MySqlClient;
using Restaurant_Ordering_and_Management_System.DBContext;

namespace Restaurant_Ordering_and_Management_System.Helper
{
    public class DbHelper
    {
        private readonly DatabaseConnection _databaseConnection;

        public DbHelper(DatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection ?? throw new ArgumentNullException(nameof(databaseConnection));
        }

        public int ExecuteNonQuery(string storedProcedureName, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = _databaseConnection.GetConnection())
            using (MySqlCommand command = BuildCommand(connection, null, storedProcedureName, parameters))
            {
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        public DataTable ExecuteQuery(string storedProcedureName, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = _databaseConnection.GetConnection())
            using (MySqlCommand command = BuildCommand(connection, null, storedProcedureName, parameters))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                DataTable table = new DataTable();
                adapter.Fill(table);
                return table;
            }
        }

        public object ExecuteScalar(string storedProcedureName, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = _databaseConnection.GetConnection())
            using (MySqlCommand command = BuildCommand(connection, null, storedProcedureName, parameters))
            {
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        /// <summary>
        /// Runs several stored-procedure calls on ONE connection inside ONE transaction.
        /// If anything throws, everything is rolled back and the exception is rethrown.
        /// </summary>
        public void ExecuteInTransaction(Action<DbTransactionContext> work)
        {
            if (work == null)
            {
                throw new ArgumentNullException(nameof(work));
            }

            using (MySqlConnection connection = _databaseConnection.GetConnection())
            {
                connection.Open();
                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        work(new DbTransactionContext(connection, transaction));
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>True when MySQL refused a delete because other rows still reference it (error 1451).</summary>
        public static bool IsForeignKeyViolation(Exception ex)
        {
            while (ex != null)
            {
                MySqlException mysqlException = ex as MySqlException;
                if (mysqlException != null && mysqlException.Number == 1451)
                {
                    return true;
                }
                ex = ex.InnerException;
            }
            return false;
        }

        internal static MySqlCommand BuildCommand(MySqlConnection connection, MySqlTransaction transaction,
                                                  string storedProcedureName, MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand(storedProcedureName, connection, transaction)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }
    }

    /// <summary>Handed to the work inside DbHelper.ExecuteInTransaction.</summary>
    public class DbTransactionContext
    {
        private readonly MySqlConnection _connection;
        private readonly MySqlTransaction _transaction;

        internal DbTransactionContext(MySqlConnection connection, MySqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public int ExecuteNonQuery(string storedProcedureName, params MySqlParameter[] parameters)
        {
            using (MySqlCommand command = DbHelper.BuildCommand(_connection, _transaction, storedProcedureName, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }
    }
}
