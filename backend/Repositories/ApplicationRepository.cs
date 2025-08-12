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

    public List<int> GetApplicants(int JobId)
    {
        List<int> Applicants = new List<int>();
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = @"
            SELECT userId 
            FROM Applications 
            WHERE jobId = @jobId
            ";

            command.Parameters.AddWithValue("@jobId", JobId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Applicants.Add(reader.GetInt32(0));
                }
            }
        }

        return Applicants;

    }

    public void SetInterviewDate(Application application)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            System.Console.WriteLine(application.JobId);
            System.Console.WriteLine(application.UserId);
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = @"
            UPDATE Applications
            SET interviewDate = @interviewDate
            WHERE userId = @userId
            AND jobId = @jobId;
            ";

            command.Parameters.AddWithValue("@userId", application.UserId);
            command.Parameters.AddWithValue("@jobId", application.JobId);
            command.Parameters.AddWithValue("@interviewDate", application.InterviewDate);

            command.ExecuteNonQuery();
        }
    }
}
