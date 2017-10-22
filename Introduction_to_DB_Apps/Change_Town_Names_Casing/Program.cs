using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;

namespace Change_Town_Names_Casing
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;
                                                          Integrated Security=true");
            connection.Open();

            string country = Console.ReadLine();

            using (connection)
            {
                string query = File.ReadAllText(@"../../findTowns.sql");
                SqlCommand findTownByContry = new SqlCommand(query, connection);
                SqlParameter countryName = new SqlParameter("@country", country);
                findTownByContry.Parameters.Add(countryName);

                SqlDataReader reader = findTownByContry.ExecuteReader();

                if (reader.Read())
                {
                    List<string> towns = new List<string>();

                    while (reader.Read())
                    {
                        string currentTown = (string)reader["Name"];
                        towns.Add(currentTown.ToUpper());
                    }
                    Console.WriteLine($"[{towns.Count}{String.Join(", ", towns)}]");
                }
                else
                {
                    Console.WriteLine("No town names were affected.");
                }

             }
        }
    }
}
