using j10sql;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace j10sql
{
    public class DatabaseManager
    {
        private string connectionString = "Data Source=student_management.db;Version=3;";

        public void CreateSchool(School school)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Schools (Name) VALUES (@Name); SELECT last_insert_rowid();";
                    command.Parameters.AddWithValue("@Name", school.Name);
                    int schoolId = Convert.ToInt32(command.ExecuteScalar());

                    school.SchoolId = schoolId;

                    foreach (var @class in school.Classes)
                    {
                        CreateClass(@class, schoolId);
                    }
                }
            }
        }

        public void CreateClass(Class @class, int schoolId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Classes (Name, SchoolId) VALUES (@Name, @SchoolId); SELECT last_insert_rowid();";
                    command.Parameters.AddWithValue("@Name", @class.Name);
                    command.Parameters.AddWithValue("@SchoolId", schoolId);
                    int classId = Convert.ToInt32(command.ExecuteScalar());

                    @class.ClassId = classId;

                    foreach (var student in @class.Students)
                    {
                        CreateStudent(student, classId);
                    }
                }
            }
        }

        public void CreateStudent(Student student, int classId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Students (Name, Age, Grade, ClassId) VALUES (@Name, @Age, @Grade, @ClassId); SELECT last_insert_rowid();";
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Grade", student.Grade);
                    command.Parameters.AddWithValue("@ClassId", classId);
                    int studentId = Convert.ToInt32(command.ExecuteScalar());

                    student.StudentId = studentId;
                }
            }
        }

        public void UpdateStudent(Student student)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "UPDATE Students SET Name = @Name, Age = @Age, Grade = @Grade WHERE StudentId = @StudentId;";
                    command.Parameters.AddWithValue("@Name", student.Name);
                    command.Parameters.AddWithValue("@Age", student.Age);
                    command.Parameters.AddWithValue("@Grade", student.Grade);
                    command.Parameters.AddWithValue("@StudentId", student.StudentId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "DELETE FROM Students WHERE StudentId = @StudentId;";
                    command.Parameters.AddWithValue("@StudentId", studentId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetStudentsByClass(int classId)
        {
            List<Student> students = new List<Student>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Students WHERE ClassId = @ClassId", connection))
                {
                    command.Parameters.AddWithValue("@ClassId", classId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var student = new Student
                            {
                                StudentId = Convert.ToInt32(reader["StudentId"]),
                                Name = reader["Name"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                Grade = reader["Grade"].ToString(),
                                ClassId = classId
                            };
                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }

        // Similar methods for Schools and Classes (Create, Update, Delete, Query)
    }
}