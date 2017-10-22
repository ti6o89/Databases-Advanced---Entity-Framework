using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Print_All_Minion_Names
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
                string query = "USE MinionsDB SELECT Name FROM Minions";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                List<string> minionNames = new List<string>();

                while (reader.Read())
                {
                    string currentName = (string)reader["Name"];
                    minionNames.Add(currentName);
                }

                for (int i = 0; i < minionNames.Count / 2 ; i++)
                {

                    Console.WriteLine(minionNames[i]);
                    Console.WriteLine(minionNames[(minionNames.Count - i - 1)]);
                }
                if (minionNames.Count % 2 != 0)
                {
                    Console.WriteLine(minionNames[(minionNames.Count / 2)]);
                }
            }
        }
    }
}
