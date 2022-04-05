using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2TaskTRNA8A
{
    class Product
    {
        private int _id;
        private string _name;
        private string _description;
        private int _price;
        private DateTime _regDate;

        public Product(int id, string name, string description, int price, DateTime regDate)
        {
            _id = id;
            _name = name;
            _description = description;
            _price = price;
            _regDate = regDate;
        }
        public Product(string name, string description, int price)
        {
            _name = name;
            _description = description;
            _price = price;
        }
        public Product(string[] vector)
        {
            _id = int.Parse(vector[0]);
            _name = vector[1];
            _description = vector[2];
            _price = int.Parse(vector[3]);
            _regDate = DateTime.Parse(vector[4]);
        }
        public Product()
        {
            _id = -1;
            _name = "";
            _description = "";
            _price = 0;
        }

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public int Price { get => _price; set => _price = value; }
        public DateTime RegDate { get => _regDate; set => _regDate = value; }
        public void Upload()
        {
            Connection.Query($"INSERT INTO products(name, description, price, reg_date) VALUES ('{this.Name}','{this.Description}','{this.Price}',NOW());");
        }
        public void Update(string newName, string newDescription, string newPrice)
        {
            if (newName != "")
            {
                this.Name = newName; 
            }
            if (newDescription != "")
            {
                this.Description = newDescription;
            }
            if (newPrice != "")
            {
                this.Price = int.Parse(newPrice);
            }
            Connection.Query($"UPDATE products SET name='{this.Name}', description='{this.Description}', price='{this.Price}' WHERE id='{this.Id}';");
        }
        public void DeleteFromDatabase()
        {
            Connection.Query($"DELETE FROM products WHERE id='{this.Id}';");
        }
        public static bool ProductExistsById(int id)
        {
            try
            {
                for (int i = 0; i < Product.GetAll().Count; i++)
                {
                    if (Product.GetAll()[i].Id == id)
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
        public static List<Product> GetAll()
        {
            List<Product> result = new List<Product>();
            try
            {
                string[,] matrix = Connection.Select($"SELECT * FROM products");
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    result.Add(new Product(Connection.MatrixToVector(matrix, i)));
                }
            }
            catch (Exception)
            {
                return result;
            }
            return result;
        }
        public static Product GetProductById(int id)
        {
            Product result = new Product();
            try
            {
                result = new Product(Connection.MatrixToVector(Connection.Select($"SELECT * FROM products WHERE id='{id}'"),0));
                return result;
            }
            catch (Exception)
            {
                return result;
            }
        }
        public string PriceToString()
        {
            return $"{this.Price} Ft";
        }
        public string ToStringShort()
        {
            return $"{this.Name}, {this.PriceToString()}\n\t{this.Description}";
        }
        public override string ToString()
        {
            return $"\tAzonosító: {this.Id}\n\tElnevezés: {this.Name}\n\tLeírás: {this.Description}\n\tÁr: {this.Price}\n\tDátum: {this.RegDate}";
        }

    }
}
