using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TRNA8A_DB2_MYSQL_CREATE
{
    class Database
    {
        public static MySqlConnection CreateConnection(string connectionString)
        {
            MySqlConnection conn;
            conn = new MySqlConnection(connectionString);
            try
            {
                conn.Open();
                Console.WriteLine("Sikeres csatlakozás!\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Csatlakozás sikertelen!\n");
                Console.WriteLine(e.Message);
            }
            return conn;
        }
        public static void CloseConnection(MySqlConnection conn)
        {
            try
            {
                conn.Close();
                Console.WriteLine("Sikeres kapcsolatbontás!\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Kapcsolatbontás sikertelen!\n");
                Console.WriteLine(e.Message);
            }
        }
        public static void CreateTable(string connectionString, string sql)
        {
            MySqlConnection conn = CreateConnection(connectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                Console.WriteLine("Sikeres tábla létrehozás!\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Tábla létrehozása sikertelen!\n");
                Console.WriteLine(e.Message);
            }
            CloseConnection(conn);
        }
        public static void AlterTable(string connectionString, string sql)
        {
            MySqlConnection conn = CreateConnection(connectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                Console.WriteLine("Sikeres tábla módosítás!\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Tábla módosítása sikertelen!\n");
                Console.WriteLine(e.Message);
            }
            CloseConnection(conn);
        }
        public static MySqlDataReader Select(string connectionString, string sql)
        {
            MySqlConnection conn = CreateConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            return cmd.ExecuteReader();
            CloseConnection(conn);
        }
        public static void InsertInto(string connectionString, string sql)
        {
            MySqlConnection conn = CreateConnection(connectionString);
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                Console.WriteLine("Sikeres feltöltés!\n");
            }
            catch (Exception e)
            {
                Console.WriteLine("Feltöltés sikertelen!\n");
                Console.WriteLine(e.Message);
            }
            CloseConnection(conn);
        }
        public static void InsertIntoMore(string connectionString, string[] sql)
        {
            MySqlConnection conn = CreateConnection(connectionString);
            for (int i = 0; i < sql.Length; i++)
            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql[i], conn);
                    MySqlDataReader dataReader = cmd.ExecuteReader();
                    Console.WriteLine($"{i + 1}. parancs: Sikeres feltöltés!\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{i + 1}. parancs: Feltöltés sikertelen!\n");
                    Console.WriteLine(e.Message);
                }
            }
            CloseConnection(conn);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string server = "localhost";
            string database = "test";
            string username = "root";
            string password = "";

            string connectionString = $"server={server};database={database};uid={username};pwd={password};";

            Console.WriteLine("Add meg az adatokat: ");
            string tulajazonosito = Console.ReadLine();
            string nev = Console.ReadLine();
            string szemig = Console.ReadLine();
            string szulhely = Console.ReadLine();
            string szulido = Console.ReadLine();
            Database.InsertInto(connectionString, $"INSERT INTO Tulajdonos VALUES ('{tulajazonosito}','{nev}','{szemig}','{szulhely}','{szulido}')");
            
            Database.CreateTable(connectionString, "CREATE TABLE tulaj (id number(3) primary key, nev char(20) not null, cim char(20), szuldatum date)");
            Database.CreateTable(connectionString, "CREATE TABLE auto (rsz char(6) primary key, tipus char(10) not null, szin char(10) default 'feher', evjarat number(4), ar number(8) check(ar>100))");

            Console.ReadKey();
        }
    }
}
