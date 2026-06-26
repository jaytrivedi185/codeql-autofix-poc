using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace DeepSourcePoc
{
    public class VulnerableService
    {
        private const string ConnectionString =
            "Server=myserver;Database=mydb;User Id=admin;Password=SuperSecret123!;";

        public void SearchUser(string username)
        {
            // SQL Injection
            string query =
                "SELECT * FROM Users WHERE Username = '" + username + "'";

            using var conn = new SqlConnection(ConnectionString);
            using var cmd = new SqlCommand(query, conn);

            Console.WriteLine(query);
        }

        public void ExecuteCommand(string userInput)
        {
            // Command Injection risk
            Process.Start("cmd.exe", "/c " + userInput);
        }

        public void WriteLog(string fileName, string content)
        {
            // Path Traversal risk
            string path = @"C:\logs\" + fileName;
            File.WriteAllText(path, content);
        }

        public bool ComparePasswords(string supplied, string actual)
        {
            // Timing attack risk
            return supplied == actual;
        }

        public void EmptyCatch()
        {
            try
            {
                throw new Exception("Something bad happened");
            }
            catch
            {
                // Swallowed exception
            }
        }

        public void BadResourceHandling()
        {
            // IDisposable object not disposed
            var writer = new StreamWriter("log.txt");
            writer.WriteLine("test");
        }

        public void HardcodedSecret()
        {
            string apiKey = "sk-prod-123456789";
            Console.WriteLine(apiKey);
        }
    }
}
