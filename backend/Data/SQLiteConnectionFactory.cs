using System;
using Microsoft.Data.Sqlite;


namespace backend.Data;

public class SQLiteConnectionFactory
{
    public static SqliteConnection CreateConnection()
        {
            var connectionString = "Data Source=Data/database.db";
            return new SqliteConnection(connectionString);
        }
}
