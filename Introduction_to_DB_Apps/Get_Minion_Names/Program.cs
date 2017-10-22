using System;
using System.Data.SqlClient;
using System.IO;

namespace Get_Minion_Names
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
                int inputVillainId = int.Parse(Console.ReadLine());
                string villainNameQuery = File.ReadAllText(@"G:\SoftUni\Databases Advanced - Entity Framework\Introduction_to_DB_Apps\Get_Minion_Names\VillainName.sql");

                SqlCommand findVillainNameCommand = new SqlCommand(villainNameQuery, connection);

                SqlParameter villainIdParam = new SqlParameter("@villainId", inputVillainId);
                findVillainNameCommand.Parameters.Add(villainIdParam);

                SqlDataReader reader = findVillainNameCommand.ExecuteReader();

                if (reader.Read())
                {
                    string villainName = (string)reader["Name"];
                    Console.WriteLine($"Villain: {villainName}");

                    string findMinionsQuery = File.ReadAllText(@"G:\SoftUni\Databases Advanced - Entity Framework\Introduction_to_DB_Apps\Get_Minion_Names\MinionInfo.sql");
                    SqlCommand findMinionsCommand = new SqlCommand(findMinionsQuery, connection);
                    SqlParameter minionsIdParam = new SqlParameter("@villainId", inputVillainId);
                    findMinionsCommand.Parameters.Add(minionsIdParam);
                    reader.Close();

                    SqlDataReader minionsReader = findMinionsCommand.ExecuteReader();

                    int index = 1;

                    while (minionsReader.Read())
                    {
                        string minionName = (string)minionsReader["Name"];
                        int minionAge = (int)minionsReader["Age"];

                        Console.WriteLine($"{index}. {minionName} {minionAge}");
                        index++;
                    }

                }
                else
                {
                    Console.WriteLine($"No villain with ID {inputVillainId} exists in the database.");
                }


            }
        }
    }
}
