using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

using backend.Models;
using backend.Data;

namespace backend.Repositories;

public class ApplicationRepository
{
    public void Apply(int userId, int jobId)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
            INSERT INTO Applications
            (userId, jobId)
            VALUES
            (@userId, @jobId)
            ";

            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@jobId", jobId);

            command.ExecuteNonQuery();

        }
    }

    public bool ApplyCheck(int userId, int jobId)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
            SELECT COUNT(*) FROM Applications
            WHERE userId = @userId
            AND jobId = @jobId
            ";

            command.Parameters.AddWithValue("@userId", userId);
            command.Parameters.AddWithValue("@jobId", jobId);

            var count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
    }
}
