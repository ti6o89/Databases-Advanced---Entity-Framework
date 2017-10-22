using System;
using System.Data.SqlClient;
using System.IO;

namespace Increase_Age_Procedure
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;
                                                          Integrated Security=true");
            connection.Open();

            using (connection)
            {
                Console.Write("Enter the minion ID: ");
                int ageParam = int.Parse(Console.ReadLine());
                string increaseAge =
                File.ReadAllText("../../IncreaseAge.sql");
                SqlCommand increaseAgeCommand =
                new SqlCommand(increaseAge, connection);
                SqlParameter param =
                new SqlParameter("@mid", ageParam);
                increaseAgeCommand.Parameters.Add(param);
                SqlDataReader reader = increaseAgeCommand.ExecuteReader();
                if (reader.Read())
                {
                    Console.WriteLine($"{(string)reader["Name"]} {(int)reader["Age"]}");
                }
            }
        }
    }
}
