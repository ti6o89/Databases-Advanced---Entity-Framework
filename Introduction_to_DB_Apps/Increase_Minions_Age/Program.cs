using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Increase_Minions_Age
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> minionsId = Console.ReadLine().Split().Select(int.Parse).ToList();

            SqlConnection connection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;
                                                            Database=MinionsDB;
                                                          Integrated Security=true");
            connection.Open();

            using (connection)
            {
                string updateQuery = $@"UPDATE Minions
                                     SET Age = Age + 1
                                     WHERE Id IN ({String.Join(", ", minionsId)})";

                SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                updateCommand.ExecuteNonQuery();

                string minionsQuery = @"SELECT Name, Age
                                    FROM Minions";
                SqlCommand minionsCommand = new SqlCommand(minionsQuery, connection);
                SqlDataReader reader = minionsCommand.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader[0], 12} | {reader[1], 3}");
                }
            }
        }
    }
}
