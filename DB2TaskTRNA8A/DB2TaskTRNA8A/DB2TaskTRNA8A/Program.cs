using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2TaskTRNA8A
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Connection.CheckConnection())
                ConsoleWindow.StartScreen();
            else
                ConsoleWindow.ErrorMessage("Az adatbázishoz való csatlakozás sikertelen!");
            Console.ReadKey();
        }
    }
}
