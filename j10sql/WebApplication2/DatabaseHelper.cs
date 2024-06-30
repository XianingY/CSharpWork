using Microsoft.Data.Sqlite;

public class DatabaseHelper
{
    private string connectionString = "Data Source=school.db";

    public void CreateTables()
    {

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string schoolTable = "CREATE TABLE IF NOT EXISTS Schools (Id INTEGER PRIMARY KEY, Name TEXT)";
        string classroomTable = "CREATE TABLE IF NOT EXISTS Classrooms (Id INTEGER PRIMARY KEY, Name TEXT, SchoolId INTEGER)";
        string studentTable = "CREATE TABLE IF NOT EXISTS Students (Id INTEGER PRIMARY KEY, Name TEXT, ClassroomId INTEGER)";
        string logTable = "CREATE TABLE IF NOT EXISTS Logs (Id INTEGER PRIMARY KEY, Operation TEXT, Timestamp DATETIME)";

        using var cmd = new SqliteCommand($"{schoolTable}; {classroomTable}; {studentTable}; {logTable}", connection);
        cmd.ExecuteNonQuery();
    }

    public void AddSchool(School school)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Schools (Name) VALUES ($name)";
        command.Parameters.AddWithValue("$name", school.Name);
        command.ExecuteNonQuery();
    }

    // Implement similar methods for Classroom and Student (CRUD)
    // Implement logging

    public void AddStudent(Student student)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Students (Name, ClassroomId) VALUES ($name, $classroomId)";
        command.Parameters.AddWithValue("$name", student.Name);
        command.Parameters.AddWithValue("$classroomId", student.ClassroomId);
        command.ExecuteNonQuery();

        LogOperation("Added student");
    }

    public List<Student> GetStudents()
    {
        var students = new List<Student>();

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Name, ClassroomId FROM Students";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            students.Add(new Student
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                ClassroomId = reader.GetInt32(2)
            });
        }

        return students;
    }

    public void LogOperation(string operation)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Logs (Operation, Timestamp) VALUES ($operation, $timestamp)";
        command.Parameters.AddWithValue("$operation", operation);
        command.Parameters.AddWithValue("$timestamp", DateTime.Now);
        command.ExecuteNonQuery();
    }
}