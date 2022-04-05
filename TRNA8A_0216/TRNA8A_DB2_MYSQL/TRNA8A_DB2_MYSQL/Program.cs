using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TRNA8A_DB2_MYSQL
{
    class Database
    {
        public static MySqlConnection CreateConnection(string server, string database, string userid, string password)
        {
            MySqlConnection conn;
            string connetionString = $"server={server};database={database};uid={userid};pwd={password};";
            conn = new MySqlConnection(connetionString);
            try
            {
                conn.Open();
                Console.WriteLine("Sikeres csatlakozás!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Csatlakozás sikertelen!");
                Console.WriteLine(e.Message);
            }
            return conn;
        }
        public static void CloseConnection(MySqlConnection conn)
        {
            try
            {
                conn.Close();
                Console.WriteLine("Sikeres kapcsolatbontás!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Kapcsolatbontás sikertelen!");
                Console.WriteLine(e.Message);
            }
        }
        public static MySqlDataReader Select(MySqlConnection conn, string sql)
        {
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            return cmd.ExecuteReader();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection conn = Database.CreateConnection("localhost", "test", "root", "");
            MySqlDataReader resultSet = Database.Select(conn, "SELECT nev FROM table1");
            while (resultSet.Read())
            {
                Console.WriteLine(resultSet[0]);
            }
            Database.CloseConnection(conn);
            Console.ReadKey();
        }
    }
}
