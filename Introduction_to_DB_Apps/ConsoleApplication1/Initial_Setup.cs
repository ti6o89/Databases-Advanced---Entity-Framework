using System.Data.SqlClient;
using System.IO;

namespace ConsoleApplication1
{
    class Initial_Setup
    {
        static void Main(string[] args)
        {
            string insertDB = File.ReadAllText(@"G:\SoftUni\Databases Advanced - Entity Framework\Introduction_to_DB_Apps\ConsoleApplication1\insertMinionsDB.sql");
            SqlConnection connection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;
                                                          Integrated Security=true");
            connection.Open();
            string sqlCreateDB = "CREATE DATABASE MinionsDB";
            
            SqlCommand createDBCommand = new SqlCommand(sqlCreateDB, connection);
            SqlCommand insertDbCommand = new SqlCommand(insertDB, connection);

            using (connection)
            {
                createDBCommand.ExecuteNonQuery();
                insertDbCommand.ExecuteNonQuery();
            }

        }
    }
}
