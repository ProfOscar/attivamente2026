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

        public SqlDataReader ExecuteReader(string query, params object[] parameters)
        {
            var connection = GetConnection();
            using var command = new SqlCommand(query, connection);
            for (int i = 0; i < parameters.Length; i++)
            {
                var item = parameters[i];
                command.Parameters.Add(new SqlParameter($"@p{i + 1}", item));
            }
            connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            using var connection = GetConnection();
            using var command = new SqlCommand(sql, connection);
            for (int i = 0; i < parameters.Length; i++)
            {
                var item = parameters[i];
                command.Parameters.Add(new SqlParameter($"@p{i + 1}", item));
            }
            connection.Open();
            return command.ExecuteNonQuery();
        }

    }
}
