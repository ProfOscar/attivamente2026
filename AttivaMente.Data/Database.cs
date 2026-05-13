using Microsoft.Data.SqlClient;
using System.Data;

namespace AttivaMente.Data
{
    public class Database
    {
        private readonly string _connectionString;

        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public SqlDataReader ExecuteReader(string query)
        {
            var connection = GetConnection();
            using var command = new SqlCommand(query, connection);
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int ExecuteNonQuery(string sql)
        {
            using var connection = GetConnection();
            using var command = new SqlCommand(sql, connection);
            connection.Open();
            return command.ExecuteNonQuery();
        }

    }
}
