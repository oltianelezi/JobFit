using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

using backend.Models;
using backend.Data;

namespace backend.Repositories;

public class UserRepository
{

    public void AddUser(User user)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
    INSERT INTO Users 
    (username, password, email, firstName, lastName, role, currJobTitle, desiredJob, industry, educationalBg, yearsOfExp, cv, phoneNumber) 
    VALUES 
    (@username, @password, @email, @firstName, @lastName, @role, @currJobTitle, @desiredJob, @industry, @educationalBg, @yearsOfExp, @cv, @phoneNumber)";
            command.Parameters.AddWithValue("@username", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@firstName", user.FirstName);
            command.Parameters.AddWithValue("@lastName", user.LastName);
            command.Parameters.AddWithValue("@role", user.Type);
            command.Parameters.AddWithValue("@currJobTitle", user.CurrJobTitle);
            command.Parameters.AddWithValue("@desiredJob", user.DesiredJob);
            command.Parameters.AddWithValue("@industry", user.Industry);
            command.Parameters.AddWithValue("@educationalBg", user.EducationalBg);
            command.Parameters.AddWithValue("@cv", user.Cv);
            command.Parameters.AddWithValue("@phoneNumber", user.PhoneNumber);

            if (user.YearsOfExp.HasValue)
            {
                command.Parameters.AddWithValue("@yearsOfExp", user.YearsOfExp.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@yearsOfExp", DBNull.Value);
            }

            command.ExecuteNonQuery();
        }
    }

    public bool UsernameTaken(string username)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT userId FROM Users WHERE username = @username";
            command.Parameters.AddWithValue("@username", username);

            var count = Convert.ToInt32(command.ExecuteScalar());
            return count > 0;
        }
    }

    public User? GetUserByUsername(string username)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE username = @username LIMIT 1";
            command.Parameters.AddWithValue("@username", username);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new User
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("userId")),
                        Username = reader.GetString(reader.GetOrdinal("username")),
                        Password = reader.GetString(reader.GetOrdinal("password")),
                        Type = reader.GetString(reader.GetOrdinal("role"))
                    };
                }
            }
        }

        return null;
    }

    public User? GetUserById(int id)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE userId = @userId LIMIT 1";
            command.Parameters.AddWithValue("@userId", id);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    var user = new User
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("userId")),
                        FirstName = reader.GetString(reader.GetOrdinal("firstName")),
                        LastName = reader.GetString(reader.GetOrdinal("lastName")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        PhoneNumber = reader.GetString(reader.GetOrdinal("phoneNumber")),
                        Type = reader.GetString(reader.GetOrdinal("role")),
                        Username = reader.GetString(reader.GetOrdinal("username")),
                        CurrJobTitle = reader.GetString(reader.GetOrdinal("currJobTitle")),
                        DesiredJob = reader.GetString(reader.GetOrdinal("desiredJob")),
                        Industry = reader.GetString(reader.GetOrdinal("industry")),
                        EducationalBg = reader.GetString(reader.GetOrdinal("educationalBg")),
                        Cv = reader.GetString(reader.GetOrdinal("cv")),
                        YearsOfExp = reader.IsDBNull(reader.GetOrdinal("yearsOfExp"))
                     ? (int?)null
                     : reader.GetInt32(reader.GetOrdinal("yearsOfExp"))
                    };
                    return user;
                }
            }
        }

        return null;
    }
}
