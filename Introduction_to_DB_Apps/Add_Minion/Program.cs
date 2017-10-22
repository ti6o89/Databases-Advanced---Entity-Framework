using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace Add_Minion
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
                Console.Write("Minion: ");
                string[] arr = Console.ReadLine().Split(' ')
                    .ToArray();

                Console.Write("Villain: ");
                string villain = Console.ReadLine();

                string isTownExist = "USE MinionsDB SELECT * FROM Towns WHERE Name = @town";
                SqlCommand isTownExistComand = new SqlCommand(isTownExist, connection);
                SqlParameter town = new SqlParameter("@town", arr[2]);
                isTownExistComand.Parameters.Add(town);

                SqlDataReader reader = isTownExistComand.ExecuteReader();
                if (!reader.Read())
                {
                    Console.Write("Country: ");
                    string countryName = Console.ReadLine();

                    string addMinionQuery = File.ReadAllText("../../AddMinion1.sql");
                    SqlCommand addMinionComand = new SqlCommand(addMinionQuery, connection);

                    addMinionComand.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@name", arr[0]),
                        new SqlParameter("@age", int.Parse(arr[1])),
                        new SqlParameter("@town", arr[2]),
                        new SqlParameter("@villain", villain),
                        new SqlParameter("@country", countryName)
                    });
                    reader.Close();
                    Console.WriteLine(addMinionComand.ExecuteNonQuery());
                    Console.WriteLine($"{arr[0]} and {arr[2]} ware added");

                }
                else
                {
                    string addMinionQuery = File.ReadAllText("../../AddMinion.sql");
                    SqlCommand addMinionComand = new SqlCommand(addMinionQuery, connection);

                    addMinionComand.Parameters.AddRange(new[]
                    {
                        new SqlParameter("@name", arr[0]),
                        new SqlParameter("@age", int.Parse(arr[1])),
                        new SqlParameter("@town", arr[2]),
                        new SqlParameter("@villain", villain)
                    });
                    reader.Close();
                    Console.WriteLine(addMinionComand.ExecuteNonQuery());
                    Console.WriteLine($"{arr[0]} was added");
                }
            }
        }
            
    }
}