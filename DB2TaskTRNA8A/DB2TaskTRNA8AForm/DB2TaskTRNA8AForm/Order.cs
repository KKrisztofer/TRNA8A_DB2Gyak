using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2TaskTRNA8AForm
{
    class Order
    {
        private int _id;
        private int _fullPrice;
        private DateTime _date;
        private Customer _customer;
        private List<Product> _products;
        private static Order currentOrder;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
            _fullPrice = 0;
        }
        public Order(string[] vector)
        {
            _id = int.Parse(vector[0]);
            _fullPrice = int.Parse(vector[1]);
            _date = DateTime.Parse(vector[2]);
            _customer = Customer.GetCustomerById(int.Parse(vector[3]));
            _products = new List<Product>();
        }
        public Order()
        {
            _id = -1;
            _fullPrice = 0;
        }

        public int Id { get => _id; set => _id = value; }
        public int FullPrice { get => _fullPrice; set => _fullPrice = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public Customer Customer { get => _customer; set => _customer = value; }
        public List<Product> Products { get => _products; set => _products = value; }
        public static Order CurrentOrder { get => currentOrder; set => currentOrder = value; }

        public void Upload()
        {
            this.Date = DateTime.Now;
            this.Id = Connection.QueryWithLastId($"INSERT INTO orders(`price`, `order_date`, `customer_id`) VALUES ({this.FullPrice},NOW(),{this.Customer.Id});");
            foreach (var item in this.Products)
                Connection.Query($"INSERT INTO orderitems(`order_id`, `product_id`) VALUES ({this.Id},{item.Id});");
        }
        public void DeleteFromDatabase()
        {
            //Először az orderitems-ben kell törölni mert idegen kulcs hivatkozik rá!
            Connection.Query($"DELETE FROM orderitems WHERE order_id='{this.Id}';");
            Connection.Query($"DELETE FROM orders WHERE id='{this.Id}';");
        }

        public void AddProduct(int id)
        {
            this.Products.Add(Product.GetProductById(id));
            this.FullPrice = this.FullPrice + Product.GetProductById(id).Price;
        }
        public void AddProductWithoutPrice(int id)
        {
            this.Products.Add(Product.GetProductById(id));
            //this.FullPrice = this.FullPrice + Product.GetProductById(id).Price;
        }
        public static List<Order> GetAll()
        {
            List<Order> result = new List<Order>();
            try
            {
                string[,] matrix = Connection.Select($"SELECT * FROM orders ORDER BY order_date DESC;");
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    string[] vector = Connection.MatrixToVector(matrix, i);
                    Order order = new Order(vector);
                    string[,] matrix2 = Connection.Select($"SELECT product_id FROM orderitems WHERE order_id = '{order.Id}'");
                    for (int j = 0; j < matrix2.GetLength(0); j++)
                    {
                        order.AddProductWithoutPrice(int.Parse(Connection.MatrixToVector(matrix2, j)[0]));
                    }
                    result.Add(order);
                }
            }
            catch (Exception r)
            {
                Console.WriteLine(r.Message);
                return result;
            }
            return result;
        }
        public static Order GetOrderById(int id)
        {
            Order result = new Order();
            try
            {
                string[,] matrix = Connection.Select($"SELECT * FROM orders WHERE id = '{id}';");
                string[] vector = Connection.MatrixToVector(matrix, 0);
                result = new Order(vector);
                string[,] matrix2 = Connection.Select($"SELECT product_id FROM orderitems WHERE order_id = '{result.Id}'");
                for (int j = 0; j < matrix2.GetLength(0); j++)
                {
                    result.AddProductWithoutPrice(int.Parse(Connection.MatrixToVector(matrix2, j)[0]));
                }
                return result;
            }
            catch (Exception r)
            {
                Console.WriteLine(r.Message);
                return result;
            }
        }
        public static bool OrderExistsById(int id)
        {
            try
            {
                for (int i = 0; i < Order.GetAll().Count; i++)
                {
                    if (Order.GetAll()[i].Id == id)
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
        public string GetProductToString()
        {
            string result = "";
            foreach (var item in this.Products)
            {
                result = result + item.Name + ", ";
            }
            return result;
        }
        public string ToStringShort()
        {
            return $"{this.Id}. {this.Customer.Fullname}, {this.FullPrice} Ft, {this.Date}, {this.Products.Count}db termék.";
        }
        public override string ToString()
        {
            string result = $"\tRendelés azonosító: {this.Id}\n\tRendelés ideje: {this.Date}\n\tRendelő adatai:\n\t\t{this.Customer.Fullname}\n\t\t{this.Customer.GetAddress()}\n\t\t{this.Customer.Phone}\n\tRendelt termékek:\n";
            foreach (var item in this.Products)
            {
                result = result + $"\t\t{item.Name} {item.PriceToString()}\n";
            }
            result = result + $"\tFizetendő: {this.FullPrice} Ft\n";
            return result;
        }
    }
}
