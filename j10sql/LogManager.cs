using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace j10sql
{
    public class LogManager
    {
        private string connectionString = "Data Source=student_management.db;Version=3;";

        public void LogAction(string action)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = "INSERT INTO Log (Action, Timestamp) VALUES (@Action, @Timestamp);";
                    command.Parameters.AddWithValue("@Action", action);
                    command.Parameters.AddWithValue("@Timestamp", DateTime.Now);

                    command.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetLogs()
        {
            List<string> logs = new List<string>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (var command = new SQLiteCommand("SELECT * FROM Log ORDER BY Timestamp DESC", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            logs.Add($"{reader["Timestamp"]} - {reader["Action"]}");
                        }
                    }
                }
            }

            return logs;
        }
    }
}
