using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace TRNA8A_DB2_MYSQL
{
    class DatabaseConnection
    {
        private string server;
        private string database;
        private string userid;
        private string password;
        private MySqlConnection conn;
        private string connetionString;
        public DatabaseConnection(string server, string database, string userid, string password)
        {
            this.server = server;
            this.database = database;
            this.userid = userid;
            this.password = password;
            this.connetionString = $"server={this.server};database={this.database};uid={this.userid};pwd={this.password};";
        }
        private void CreateConnection()
        {
            try
            {
                this.conn = new MySqlConnection(this.connetionString);
                this.conn.Open();
                Console.WriteLine("Sikeres kapcsolódás!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Az adatbázishoz való kapcsolódás meghiúsult!");
                Console.WriteLine(e.Message);
            }
        }
        private void CloseConnection()
        {
            try
            {
                this.conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("A kapcsolabontás sikertelen!");
                Console.WriteLine(e.Message);
            }
        }
        public void CreateTable(string sql)
        {
            this.CreateConnection();
            try
            {
                MySqlCommand CreateTable = new MySqlCommand(sql, this.conn);
                CreateTable.ExecuteNonQuery();
                Console.WriteLine("Sikeres tábla létrehozás!");
            }
            catch (Exception e)
            {
                Console.WriteLine("A tábla létrehozás sikertelen!");
                Console.WriteLine(e.Message);
            }
            this.CloseConnection();
        }
        public void AlterTable(string sql)
        {
            this.CreateConnection();
            try
            {
                MySqlCommand CreateTable = new MySqlCommand(sql, this.conn);
                CreateTable.ExecuteNonQuery();
                Console.WriteLine("Sikeres tábla módosítás!");
            }
            catch (Exception e)
            {
                Console.WriteLine("A tábla módosítása sikertelen!");
                Console.WriteLine(e.Message);
            }
            this.CloseConnection();
        }
        public void DropTable(string table)
        {
            this.CreateConnection();
            try
            {
                MySqlCommand CreateTable = new MySqlCommand($"DROP TABLE {table};", this.conn);
                CreateTable.ExecuteNonQuery();
                Console.WriteLine("Sikeres tábla törlés!");
            }
            catch (Exception e)
            {
                Console.WriteLine("A tábla törlése sikertelen!");
                Console.WriteLine(e.Message);
            }
            this.CloseConnection();
        }
        public MySqlDataReader Select(string sql)
        {
            this.CreateConnection();
            MySqlCommand cmd = new MySqlCommand(sql, this.conn);
            this.CreateConnection();
            return cmd.ExecuteReader();
        }
        public void InsertInto(string sql)
        {
            this.CreateConnection();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                Console.WriteLine("Sikeres feltöltés!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Feltöltés sikertelen!");
                Console.WriteLine(e.Message);
            }
            this.CloseConnection();
        }
        public void DeleteFrom(string sql)
        {
            this.CreateConnection();
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, this.conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                Console.WriteLine("Sikeres Törlés!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Törlés sikertelen!");
                Console.WriteLine(e.Message);
            }
            this.CloseConnection();
        }
        public override string ToString()
        {
            return this.connetionString;
        }
    }
    class Auto
    {
        private string rsz;
        private string tipus;
        private string szin;
        private int evjarat;
        private int ar;
        private int tulaj_id;

        public string Rsz { get => rsz; set => rsz = value; }
        public string Tipus { get => tipus; set => tipus = value; }
        public string Szin { get => szin; set => szin = value; }
        public int Evjarat { get => evjarat; set => evjarat = value; }
        public int Ar { get => ar; set => ar = value; }
        public int Tulaj_id { get => tulaj_id; set => tulaj_id = value; }

        public Auto(string rsz, string tipus, string szin, int evjarat, int ar, int tulaj_id)
        {
            this.Rsz = rsz;
            this.Tipus = tipus;
            this.Szin = szin;
            this.Evjarat = evjarat;
            this.Ar = ar;
            this.Tulaj_id = tulaj_id;
        }
        public Auto()
        {
            Console.Write("Add meg a rendszámot: ");
            this.Rsz = Console.ReadLine();
            Console.Write("Add meg a típust: ");
            this.Tipus = Console.ReadLine();
            Console.Write("Add meg a színt: ");
            this.Szin = Console.ReadLine();
            Console.Write("Add meg az évjáratot: ");
            this.Evjarat = int.Parse(Console.ReadLine());
            Console.Write("Add meg az árat: ");
            this.Ar = int.Parse(Console.ReadLine());
            Console.Write("Add meg a tulajdonos azonosítóját: ");
            this.Tulaj_id = int.Parse(Console.ReadLine());
        }
        public Auto(MySqlDataReader dataReader)
        {
            try
            {
                this.Rsz = Convert.ToString(dataReader[0]);
                this.Tipus = Convert.ToString(dataReader[1]);
                this.Szin = Convert.ToString(dataReader[2]);
                this.Evjarat = int.Parse(Convert.ToString(dataReader[3]));
                this.Ar = int.Parse(Convert.ToString(dataReader[4]));
                this.Tulaj_id = int.Parse(Convert.ToString(dataReader[5]));
            }
            catch (Exception e)
            {
                Console.WriteLine("Sikertelen lekérdezés!");
                Console.WriteLine(e.Message);
            }
        }
        public override string ToString()
        {
            return $"Rendszam: {this.Rsz}\nTípus: {this.Tipus}\nSzín: {this.Szin}\nEvjárat: {this.Evjarat}\nÁr: {this.Ar}\nTulajdonos: {this.Tulaj_id}\n";
        }
        public void Upload(DatabaseConnection Connection)
        {
            Connection.InsertInto($@"
                INSERT INTO auto VALUES(
                    '{this.Rsz}',
                    '{this.Tipus}',
                    '{this.Szin}',
                    {this.Evjarat},
                    {this.Ar},
                    {this.Tulaj_id}
                );
            ");
        }
        public static void DeleteByRsz(DatabaseConnection Connection, string rsz)
        {
            Connection.DeleteFrom($"DELETE FROM auto WHERE rsz='{rsz}';");
            Console.WriteLine($"A {rsz} rendszámú autó törölve.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DatabaseConnection Connection = new DatabaseConnection("localhost", "test", "root", "");
            Connection.CreateTable(@"
                CREATE TABLE auto(
                    rsz char(6) PRIMARY KEY,
                    tipus char(10) NOT NULL,
                    szin char(10) DEFAULT 'feher',
                    evjarat int(4),
                    ar int(8) CHECK(ar>0)
                );
            ");
            Connection.CreateTable(@"
                CREATE TABLE tulaj(
                    id int(3) PRIMARY KEY,
                    nev char(20) NOT NULL,
                    cim char(20),
                    szuldatum date
                );
            ");

            Connection.AlterTable("ALTER TABLE auto ADD tulaj_id int(3) references tulaj.id;");

            Connection.InsertInto(@"
                INSERT INTO tulaj VALUES(
                    1,
                    'Tóth Máté',
                    'Miskolc',
                    '1980.05.12'
                );
            ");
            Connection.InsertInto(@"
                INSERT INTO auto VALUES(
                    'aaa111',
                    'opel',
                    'piros',
                    2014,
                    1650000,
                    1
                );
            ");
            Connection.InsertInto(@"
                INSERT INTO auto VALUES(
                    'bbb222',
                    'mazda',
                    null,
                    2016,
                    2800000,
                    1
                );
            ");
            Connection.InsertInto(@"
                INSERT INTO auto (rsz, tipus, evjarat, ar) VALUES(
                    'ccc333',
                    'ford',
                    2009,
                    15000000
                );
            ");

            Auto auto1 = new Auto();
            auto1.Upload(Connection);
            
            Auto.DeleteByRsz(Connection, "ddd444"); 
            

            List<Auto> autok = new List<Auto>();
            MySqlDataReader dataReader = Connection.Select("SELECT * FROM auto;");
            while (dataReader.Read())
            {
                autok.Add(new Auto(dataReader));
            }
            foreach (var item in autok)
            {
                Console.WriteLine(item.ToString());
            }
            
            Console.ReadKey();
        }
    }
}
