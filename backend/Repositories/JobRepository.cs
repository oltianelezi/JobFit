using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

using backend.Models;
using backend.Data;
namespace backend.Repositories;

public class JobRepository
{
    public void AddJob(Job job)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
    INSERT INTO Jobs 
    (position, company, location, datePosted, userId, jobStatus, jobType) 
    VALUES 
    (@position, @company, @location, @datePosted, @userId, @jobStatus, @jobType)";
            command.Parameters.AddWithValue("@position", job.Position);
            command.Parameters.AddWithValue("@company", job.Company);
            command.Parameters.AddWithValue("@location", job.Location);
            command.Parameters.AddWithValue("@datePosted", job.DatePosted);
            command.Parameters.AddWithValue("@userId", job.UserId);
            command.Parameters.AddWithValue("@jobStatus", job.Jobstatus);
            command.Parameters.AddWithValue("@jobType", job.Jobtype);

            command.ExecuteNonQuery();
        }
    }
}
