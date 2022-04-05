using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DB2TaskTRNA8A
{
    static class Print
    {
        static Random rnd = new Random();
        public static string PrintProducts()
        {
            string filename = $"prints/products/products{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{rnd.Next(1000, 9999)}.txt";
            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            int width = 39;
            for (int i = 0; i < width; i++)
                sw.Write("#");
            sw.WriteLine();
            for (int i = 0; i < width; i++)
                sw.Write("-");
            sw.WriteLine();
            foreach (var item in Product.GetAll())
            {
                if (item.Name.Length < 40)
                {
                    for (int i = 0; i < 40 - item.Name.Length; i++)
                    {
                        item.Name = item.Name + " ";
                    }
                }
                sw.WriteLine($"{item.Name}\t{item.PriceToString()}");
                for (int i = 0; i < width; i++)
                    sw.Write("-");
                sw.WriteLine();
            }
            for (int i = 0; i < width; i++)
                sw.Write("#");

            sw.Close();
            fs.Close();

            return filename;
        }
        public static string PrintInvoice(Order order)
        {
            string filename = $"prints/invoice/invoice{DateTime.Now.Month}{DateTime.Now.Day}{DateTime.Now.Hour}{DateTime.Now.Minute}{rnd.Next(1000, 9999)}.txt";
            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine($"Személyes adatok:");
            sw.WriteLine($"\t{order.Customer.Fullname}");
            sw.WriteLine($"\t{order.Customer.GetAddress()}");
            sw.WriteLine($"\t{order.Customer.Phone}");
            sw.WriteLine($"\nMegrendelt termékek:");

            foreach (var item in order.Products)
            {
                sw.WriteLine($"\t{item.Name} - {item.PriceToString()}");
            }

            sw.WriteLine($"\nÖsszesen: {order.FullPrice} Ft");

            sw.Close();
            fs.Close();

            return filename;
        }
    }
}
