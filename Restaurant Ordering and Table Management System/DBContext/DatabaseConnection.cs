using System;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Restaurant_Ordering_and_Management_System.DBContext
{
   
    public class DatabaseConnection
    {
        private readonly string _connectionString;

        
        public DatabaseConnection()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["RestaurantDb"];

            if (settings == null)
            {
                throw new InvalidOperationException(
                    "Connection string 'RestaurantDb' was not found in App.config. " +
                    "Check the <connectionStrings> section.");
            }

            _connectionString = settings.ConnectionString;
        }

        
        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        
        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
