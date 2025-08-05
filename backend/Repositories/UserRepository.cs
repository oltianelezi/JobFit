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

    public int? UsernameTaken(string username)
    {
        using (var connection = SQLiteConnectionFactory.CreateConnection())
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT userId FROM Users WHERE username = @username";
            command.Parameters.AddWithValue("@username", username);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return reader.GetInt32(reader.GetOrdinal("userId"));
                }
            }
        }
        return null;
    }
}
