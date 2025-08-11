using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

using backend.Models;
using backend.Data;
using backend.DTOs.Job;
namespace backend.Repositories;

public class JobRepository
{

    public Job getJobById(int JobId)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Jobs WHERE jobId = @jobId";
            command.Parameters.AddWithValue("@jobId", JobId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    return new Job
                    {
                        Position = reader.GetString(0),
                        Company = reader.GetString(1),
                        Location = reader.GetString(2),
                        DatePosted = DateOnly.Parse(reader.GetString(3)),
                        JobId = reader.GetInt32(4),
                        UserId = reader.GetInt32(5),
                        Jobstatus = reader.GetString(6),
                        Jobtype = reader.GetString(7)
                    };
                }
            }
        }

        return null;
    }
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

    public List<Job> getJobs(SearchQueryRequest filter)
    {
        var jobs = new List<Job>();

        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            var query = "SELECT * FROM Jobs WHERE 1=1 ";

            if (filter.userType == "Employer")
            {
                query += " AND userId = @userId ";
                command.Parameters.AddWithValue("@userId", filter.userId);
            }

            if (!string.IsNullOrEmpty(filter.search))
            {
                query += " AND company = @company ";
                command.Parameters.AddWithValue("@company", filter.search);

            }

            if (filter.jobstatus != "Any")
            {
                query += " AND jobStatus = @jobStatus ";
                command.Parameters.AddWithValue("@jobStatus", filter.jobstatus);

            }

            if (filter.jobtype != "Any")
            {
                query += " AND jobType = @jobType ";
                command.Parameters.AddWithValue("@jobType", filter.jobtype);

            }


            command.CommandText = query;

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    jobs.Add(new Job
                    {
                        Position = reader.GetString(0),
                        Company = reader.GetString(1),
                        Location = reader.GetString(2),
                        DatePosted = DateOnly.Parse(reader.GetString(3)),
                        JobId = reader.GetInt32(4),
                        UserId = reader.GetInt32(5),
                        Jobstatus = reader.GetString(6),
                        Jobtype = reader.GetString(7)
                    });
                }
            }
        }
        return jobs;
    }

    public void UpdateJob(Job job)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();

            command.CommandText = @"
                UPDATE Jobs
            SET
                position = @position,
                company = @company,
                location = @location,
                jobStatus = @jobStatus,
                jobType = @jobType
            WHERE jobId = @jobId;
                ";

            command.Parameters.AddWithValue("@position", job.Position);
            command.Parameters.AddWithValue("@company", job.Company);
            command.Parameters.AddWithValue("@location", job.Location);
            command.Parameters.AddWithValue("@jobId", job.JobId);
            command.Parameters.AddWithValue("@jobStatus", job.Jobstatus);
            command.Parameters.AddWithValue("@jobType", job.Jobtype);

            command.ExecuteNonQuery();
        }
    }

    public void DeleteJob(int JobId)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();

            command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Jobs WHERE JobId = @jobId";
            command.Parameters.AddWithValue("@jobId", JobId);

            command.ExecuteNonQuery();
        }
    }
}
