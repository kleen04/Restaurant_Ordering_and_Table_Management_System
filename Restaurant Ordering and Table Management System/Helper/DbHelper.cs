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
            using (MySqlCommand command = BuildCommand(connection, storedProcedureName, parameters))
            {
                connection.Open();
                return command.ExecuteNonQuery();
            }
        }

        
        public DataTable ExecuteQuery(string storedProcedureName, params MySqlParameter[] parameters)
        {
            using (MySqlConnection connection = _databaseConnection.GetConnection())
            using (MySqlCommand command = BuildCommand(connection, storedProcedureName, parameters))
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
            using (MySqlCommand command = BuildCommand(connection, storedProcedureName, parameters))
            {
                connection.Open();
                return command.ExecuteScalar();
            }
        }

        private static MySqlCommand BuildCommand(MySqlConnection connection, string storedProcedureName, MySqlParameter[] parameters)
        {
            MySqlCommand command = new MySqlCommand(storedProcedureName, connection)
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
}
