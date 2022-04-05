using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2TaskTRNA8AForm
{
    class Customer : Person
    {
        private string _email;
        private string _address;
        private string _city;
        private int _postcode;
        private DateTime _regDate;
        private static Customer currentCustomer;

        public string Email { get => _email; set => _email = value; }
        public string Address { get => _address; set => _address = value; }
        public string City { get => _city; set => _city = value; }
        public int Postcode { get => _postcode; set => _postcode = value; }
        public DateTime RegDate { get => _regDate; set => _regDate = value; }
        internal static Customer CurrentCustomer { get => currentCustomer; set => currentCustomer = value; }

        public Customer(int id, string firstname, string lastname, string email, string address, string city, int postcode, string phone, DateTime regDate) : base(id, firstname, lastname, phone)
        {
            Email = email;
            Address = address;
            City = city;
            Postcode = postcode;
            RegDate = regDate;
        }
        public Customer(string firstname, string lastname, string email, string address, string city, int postcode, string phone) : base(firstname, lastname, phone)
        {
            Email = email;
            Address = address;
            City = city;
            Postcode = postcode;
            RegDate = DateTime.Now;
        }
        public Customer(string[] vector) : base(int.Parse(vector[0]), vector[1], vector[2], vector[7])
        {
            Email = vector[3];
            Address = vector[4];
            City = vector[5];
            Postcode = int.Parse(vector[6]);
            RegDate = DateTime.Parse(vector[8]);
        }
        public Customer() : base("", "", "")
        { }

        public void Upload()
        {
            this.Id = Connection.QueryWithLastId($"INSERT INTO customers(firstname, lastname, email, address, city, postcode, phone, reg_date) VALUES('{this.Firstname}','{this.Lastname}','{this.Email}','{this.Address}','{this.City}',{this.Postcode},'{this.Phone}',NOW());");
        }
        public static List<Customer> GetAll()
        {
            List<Customer> result = new List<Customer>();
            try
            {
                string[,] matrix = Connection.Select($"SELECT * FROM customers");
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    result.Add(new Customer(Connection.MatrixToVector(matrix, i)));
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public static Customer GetCustomerById(int id)
        {
            Customer result = new Customer();
            try
            {
                result = new Customer(Connection.MatrixToVector(Connection.Select($"SELECT * FROM customers WHERE id='{id}'"), 0));
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        public static bool CustomerExistsById(int id)
        {
            try
            {
                for (int i = 0; i < Customer.GetAll().Count; i++)
                {
                    if (Customer.GetAll()[i].Id == id)
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }
        public string GetAddress()
        {
            return $"{this.Postcode} {this.City} {this.Address}";
        }
        public string ToStringShort()
        {
            return $"{this.Fullname} {this.City} {this.Phone}";
        }
        public string ToStringUser()
        {
            return $"\t{this.Fullname}\n\t{this.GetAddress()}\n\t{this.Email}\n\t{this.Phone}";
        }
        public override string ToString()
        {
            return $"\tAzonosító: {this.Id}\n\tVezetéknév: {this.Lastname}\n\tKeresztnév: {this.Firstname}\n\tEmail: {this.Email}\n\tCím: {this.GetAddress()}\n\tTelefonszám: {this.Phone}\n\tRegisztráció: {this.RegDate}";
        }
    }
}
