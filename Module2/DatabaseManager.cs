using System;
using Npgsql;
using System.Collections.Generic;

public class DatabaseManager
{
    private const string ConnectionString = "Host=localhost; Username=postgress; Password=postgrespassword; Database=dblab";

    public void CreateStudentTable()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        using var cmd = new NpgsqlCommand(
            "CREATE TABLE IF NOT EXISTS Student (" +
            "Id SERIAL PRIMARY KEY," +
            "LastName VARCHAR(255)," +
            "Faculty VARCHAR(255)," +
            "Course INTEGER," +
            "Gender VARCHAR(10)," +
            "Scholarship DECIMAL," +
            "Grade1 INTEGER," +
            "Grade2 INTEGER," +
            "Grade3 INTEGER)",
            connection);

        cmd.ExecuteNonQuery();
    }

    public void InsertSampleData()
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        using var cmd = new NpgsqlCommand(
            "INSERT INTO Student (LastName, Faculty, Course, Gender, Scholarship, Grade1, Grade2, Grade3) " +
            "VALUES ('Ivan', 'Computer Science', 2, 'Male', 1500, 85, 90, 88)," +
            "('Tioma', 'Physics', 3, 'Female', 1200, 78, 85, 92)," +
            "('Sasha', 'Mathematics', 1, 'Male', 1800, 92, 88, 90)",
            connection);

        cmd.ExecuteNonQuery();
    }

    public List<Student> GetAllStudents()
    {
        var students = new List<Student>();
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        using var cmd = new NpgsqlCommand("SELECT * FROM Student", connection);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            var student = new Student
            {
                Id = reader.GetInt32(0),
                LastName = reader.GetString(1),
                Faculty = reader.GetString(2),
                Course = reader.GetInt32(3),
                Gender = reader.GetString(4),
                Scholarship = reader.GetDecimal(5),
                Grade1 = reader.GetInt32(6),
                Grade2 = reader.GetInt32(7),
                Grade3 = reader.GetInt32(8)
            };
            students.Add(student);
        }

        return students;
    }

    public Student GetStudentInfoByLastName(string lastName)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        using var cmd = new NpgsqlCommand(
            "SELECT * FROM Student WHERE LastName = @lastName",
            connection);

        cmd.Parameters.AddWithValue("lastName", lastName);

        using var reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            var student = new Student
            {
                Id = reader.GetInt32(0),
                LastName = reader.GetString(1),
                Faculty = reader.GetString(2),
                Course = reader.GetInt32(3),
                Gender = reader.GetString(4),
                Scholarship = reader.GetDecimal(5),
                Grade1 = reader.GetInt32(6),
                Grade2 = reader.GetInt32(7),
                Grade3 = reader.GetInt32(8)
            };
            return student;
        }

        return null;
    }

    public int CountExcellentMaleStudentsByCourseAndFaculty(int course, string faculty)
    {
        using var connection = new NpgsqlConnection(ConnectionString);
        connection.Open();

        using var cmd = new NpgsqlCommand(
            "SELECT COUNT(*) FROM Student WHERE Gender = 'Male' AND Course = @course AND Faculty = @faculty " +
            "AND Grade1 >= 90 AND Grade2 >= 90 AND Grade3 >= 90",
            connection);

        cmd.Parameters.AddWithValue("course", course);
        cmd.Parameters.AddWithValue("faculty", faculty);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }
}
