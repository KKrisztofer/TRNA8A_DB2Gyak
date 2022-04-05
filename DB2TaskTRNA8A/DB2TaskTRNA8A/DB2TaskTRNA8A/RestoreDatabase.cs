using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2TaskTRNA8A
{
    static class RestoreDatabase
    {
        public static void DropTables()
        {
            Connection.Query("DROP TABLE orderitems, orders, customers, workers, products;");
        }
        public static void CreateTables()
        {
            Connection.Query(@"
                CREATE TABLE Workers
                (
                  id INT NOT NULL AUTO_INCREMENT,
                  firstname VARCHAR(20) NOT NULL,
                  lastname VARCHAR(20) NOT NULL,
                  username VARCHAR(20) UNIQUE NOT NULL,
                  phone VARCHAR(11) NOT NULL,
                  password VARCHAR(20) NOT NULL,
                  PRIMARY KEY (id)
                );
            ");
            Connection.Query(@"
                CREATE TABLE Customers
                (
                  id INT NOT NULL AUTO_INCREMENT,
                  firstname VARCHAR(20) NOT NULL,
                  lastname VARCHAR(20) NOT NULL,
                  email VARCHAR(30) UNIQUE NOT NULL,
                  address VARCHAR(30),
                  city VARCHAR(30),
                  postcode INT(4),
                  phone VARCHAR(11) NOT NULL,
                  reg_date DATETIME NOT NULL,
                  PRIMARY KEY (id)
                );
            ");
            Connection.Query(@"
                CREATE TABLE Products
                (
                  id INT NOT NULL AUTO_INCREMENT,
                  name VARCHAR(20) NOT NULL,
                  description VARCHAR(120),
                  price INT NOT NULL,
                  reg_date DATETIME NOT NULL,
                  PRIMARY KEY (id)
                );
            ");
            Connection.Query(@"
                CREATE TABLE Orders
                (
                  id INT NOT NULL AUTO_INCREMENT,
                  price INT NOT NULL,
                  order_date DATETIME  NOT NULL,
                  customer_id INT NOT NULL,
                  PRIMARY KEY (id),
                  FOREIGN KEY (customer_id) REFERENCES Customers(id)
                );
            ");
            Connection.Query(@"
                CREATE TABLE OrderItems
                (
                  order_id INT NOT NULL,
                  product_id INT NOT NULL,
                  FOREIGN KEY (order_id) REFERENCES Orders(id),
                  FOREIGN KEY (product_id) REFERENCES Products(id)
                );
            ");
        }
        public static void InsertDatas()
        {
            Connection.Query(@"
                INSERT INTO workers(firstname, lastname, username, phone, password) VALUES ('Krisztofer','Kerekes','krisz00','06706762436','alma');
                INSERT INTO workers(firstname, lastname, username, phone, password) VALUES ('Teszt fiók','','teszt','00000000000','12345');

                INSERT INTO products(name, description, price, reg_date) VALUES ('Pizza Margareta','paradicsomszósz, sajt, oregano',1590,NOW());
                INSERT INTO products(name, description, price, reg_date) VALUES ('Pizza Prosciutto','paradicsomszósz, sonka, sajt, oregano',1990,NOW());
                INSERT INTO products(name, description, price, reg_date) VALUES ('Pizza Salami','paradicsomszósz,sajt,szalámi,oregano',1990,NOW());
                INSERT INTO products(name, description, price, reg_date) VALUES ('Pizza Hawaii','paradicsomszósz, sajt, sonka, ananász, oregano',2100,NOW());
                INSERT INTO products(name, description, price, reg_date) VALUES ('Pizza Magyaros','paradicsomszósz, sajt, szalámi, bacon, hagyma, erőspaprika, oregano',2190,NOW());
                INSERT INTO products(name, description, price, reg_date) VALUES ('Pizza Négysajtos','paradicsomszósz,sajt,füstölt sajt,trappista sajt,mozzarella,parmezán,oregano',2200,NOW());
                INSERT INTO products(name, description, price, reg_date) VALUES ('Coca Cola 1l',NULL,450,NOW());
                INSERT INTO products(name, description, price, reg_date) VALUES ('Pepsi Cola 1l',NULL,450,NOW());
                INSERT INTO products(name, description, price, reg_date) VALUES ('Ásványvíz',NULL,300,NOW());

                INSERT INTO customers(firstname, lastname, email, address, city, postcode, phone, reg_date) VALUES
                ('Cintia','Vass','vasscintia@gmail.com','Ady Endre utca 43','Emőd',3432,'06708435622',NOW());
                INSERT INTO customers(firstname, lastname, email, address, city, postcode, phone, reg_date) VALUES
                ('Nikoletta','Kovács','kovacsnikoletta@gmail.com','Balassi Bálint utca 22','Nyékládháza',3433,'06308467367',NOW());
                INSERT INTO customers(firstname, lastname, email, address, city, postcode, phone, reg_date) VALUES
                ('Dávid','Szabó','szabodavid@gmail.com','Vasút utca 10','Nyékládháza',3433,'06709463726',NOW());
                INSERT INTO customers(firstname, lastname, email, address, city, postcode, phone, reg_date) VALUES
                ('Márton','Fehér','fehermarton@gmail.com','Kossuth Lajos utca 9','Nyékládháza',3433,'06709557888',NOW());
                INSERT INTO customers(firstname, lastname, email, address, city, postcode, phone, reg_date) VALUES
                ('Hanna','Szilágyi','szilagyihanna@gmail.com','Sport utca 54','Nyékládháza',3433,'06208432944',NOW());
            ");
        }
    }
}
