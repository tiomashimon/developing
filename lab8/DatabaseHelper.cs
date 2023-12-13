using Npgsql;
using System;

public class DatabaseHelper
{
    private const string ConnectionString = "Host=localhost; Username=postgress; Password=postgrespassqord; Database=dblab";

    public static void ExecuteQuery(string query, params NpgsqlParameter[] parameters)
    {
        using (var connection = new NpgsqlConnection(ConnectionString))
        {
            connection.Open();
            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddRange(parameters);
                command.ExecuteNonQuery();
            }
        }
    }
}
