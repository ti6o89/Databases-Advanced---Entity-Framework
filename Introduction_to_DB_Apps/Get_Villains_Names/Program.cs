using System;
using System.Data.SqlClient;
using System.IO;

namespace Get_Villains_Names
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;
                                                          Integrated Security=true");
            connection.Open();
            string query = File.ReadAllText (@"G:\SoftUni\Databases Advanced - Entity Framework\Introduction_to_DB_Apps\Get_Villains_Names\Villain.sql");

            SqlCommand command = new SqlCommand(query, connection);

            using (connection)
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string villainsNames = (string)reader["Name"];
                    int countMinions = (int)reader["MinionsCount"];

                    Console.WriteLine($"{villainsNames} {countMinions}");
                }
            }
        }
    }
}
