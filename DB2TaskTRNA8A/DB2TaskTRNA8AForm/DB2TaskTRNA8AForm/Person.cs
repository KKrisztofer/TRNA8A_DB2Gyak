using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2TaskTRNA8AForm
{
    abstract class Person
    {
        private int _id;
        private string _firstname;
        private string _lastname;
        private string _phone;

        protected Person(int id, string firstname, string lastname, string phone)
        {
            _id = id;
            _firstname = firstname;
            _lastname = lastname;
            _phone = phone;
        }
        protected Person(string firstname, string lastname, string phone)
        {
            _firstname = firstname;
            _lastname = lastname;
            _phone = phone;
        }

        public int Id { get => _id; set => _id = value; }
        public string Firstname { get => _firstname; }
        public string Lastname { get => _lastname; }
        public string Fullname { get => $"{_lastname} {_firstname}"; }
        public string Phone { get => _phone; set => _phone = value; }

        public override string ToString()
        {
            return $"{this.Firstname} {this.Lastname}";
        }
    }
}
