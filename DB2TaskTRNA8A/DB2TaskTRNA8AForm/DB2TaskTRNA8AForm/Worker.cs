using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2TaskTRNA8AForm
{
    class Worker : Person
    {
        private string _username;
        private static Worker currentUser;

        public Worker(int id, string firstname, string lastname, string username, string phone) : base(id, firstname, lastname, phone)
        {
            _username = username;
        }
        public Worker(string[] vector) : base(int.Parse(vector[0]), vector[1], vector[2], vector[4])
        {
            _username = vector[3];
        }
        public Worker() : base(-1, "", "", "")
        {
            _username = "";
        }
        public string Username { get => _username; }
        internal static Worker CurrentUser { get => currentUser; set => currentUser = value; }

        public static bool TryLogin(string username, string password)
        {
            Worker user = Worker.GetByUsername(username);
            Worker.CurrentUser = user;
            return user.CheckPassword(password);
        }
        public static void LogOut()
        {
            Worker.CurrentUser = new Worker();
        }
        public bool CheckPassword(string password)
        {
            return Connection.Select($"SELECT id FROM workers WHERE username = '{this.Username}' AND password = '{password}';").GetLength(0) == 1;
        }
        public bool ChangePassword(string oldPassword1, string oldPassword2, string newPassword)
        {
            try
            {
                if (oldPassword1 == oldPassword2 && Connection.Select($"SELECT password FROM workers WHERE id='{Worker.currentUser.Id}';")[0, 0] == oldPassword1)
                {
                    Connection.Query($"UPDATE workers SET password='{newPassword}' WHERE id='{this.Id}'");
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public static Worker GetByUsername(string username)
        {
            try
            {
                string[,] matrix = Connection.Select($"SELECT * FROM workers WHERE username = '{username}'");
                return new Worker(Connection.MatrixToVector(matrix, 0));
            }
            catch (Exception)
            {
                return new Worker();
            }
        }
        public static List<Worker> GetAll()
        {
            List<Worker> result = new List<Worker>();
            try
            {
                string[,] matrix = Connection.Select($"SELECT * FROM workers");
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    result.Add(new Worker(Connection.MatrixToVector(matrix, i)));
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public string Welcome()
        {
            return $"Szia {this.Firstname}!";
        }
        public override string ToString()
        {
            return $"\tAzonosító: {this.Id}\n\tNév: {this.Lastname} {this.Firstname}\n\tFelhasználónév: {this.Username}\n\tTelefonszám: {this.Phone}";
        }
    }
}
