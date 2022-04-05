using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DB2TaskTRNA8AForm
{
    static class Connection
    {
        private static string server = "localhost";
        private static string database = "db2task";
        private static string userid = "root";
        private static string password = "";
        private static string connetionString = $"server={server};database={database};uid={userid};pwd={password};";
        private static MySqlConnection Conn;
        private static void CreateConnection()
        {
            try
            {
                Conn = new MySqlConnection(connetionString);
                Conn.Open();
            }
            catch (Exception)
            {
                //Console.WriteLine("Az adatbázishoz való kapcsolódás meghiúsult!");
                //Console.WriteLine(e.Message);
            }
        }
        private static void CloseConnection()
        {
            try
            {
                Conn.Close();
            }
            catch (Exception)
            {
                //Console.WriteLine("A kapcsolabontás sikertelen!");
                //Console.WriteLine(e.Message);
            }
        }
        public static void Query(string sql)
        {
            CreateConnection();
            try
            {
                MySqlCommand query = new MySqlCommand(sql, Conn);
                query.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //Console.WriteLine(e.Message);
            }
            CloseConnection();
        }
        public static int QueryWithLastId(string sql)
        {
            CreateConnection();
            try
            {
                MySqlCommand query = new MySqlCommand(sql, Conn);
                query.ExecuteNonQuery();
                CloseConnection();
                return int.Parse(Convert.ToString(query.LastInsertedId));
            }
            catch (Exception)
            {
                //Console.WriteLine(e.Message);
                return 0;
            }
        }
        public static string[,] Select(string sql)
        {
            CreateConnection();
            MySqlCommand cmd = new MySqlCommand(sql, Conn);
            MySqlDataReader dataReader = cmd.ExecuteReader();
            int row = 0;
            while (dataReader.Read())
                row++;
            dataReader.Close();
            dataReader = cmd.ExecuteReader();
            int column = dataReader.FieldCount;
            string[,] result = new string[row, column];
            int i = 0;
            while (dataReader.Read())
            {
                for (int j = 0; j < column; j++)
                {
                    result[i, j] = Convert.ToString(dataReader[j]);
                }
                i++;
            }
            CloseConnection();
            return result;
        }
        public static bool CheckConnection()
        {
            try
            {
                Conn = new MySqlConnection(connetionString);
                Conn.Open();
                Conn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string[] MatrixToVector(string[,] matrix, int index)
        {
            string[] vector = new string[matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                vector[i] = matrix[index, i];
            }
            return vector;
        }
    }
}
