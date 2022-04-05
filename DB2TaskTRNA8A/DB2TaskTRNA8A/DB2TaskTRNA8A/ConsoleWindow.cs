using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB2TaskTRNA8A
{
    static class ConsoleWindow
    {
        public static int ReadMenu(string[] menu)
        {
            Console.WriteLine("Válaszd ki a menüpontot!\n");
            Yellow();
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"\t{i + 1}. {menu[i]}");
            }
            White();
            Console.WriteLine("\nAdd meg a menüpont számát!");
            bool valid;
            int readNumber = 0;
            do
            {
                valid = true;
                try
                {
                    readNumber = int.Parse(ReadLine());
                }
                catch (Exception)
                {
                    valid = false;
                }

                if (!valid)
                {
                    valid = false;
                    ErrorMessage("\tNem számot adtál meg!");
                    Console.WriteLine("Add meg újra!");
                }
                else
                {
                    if (readNumber > menu.Length || readNumber < 1)
                    {
                        valid = false;
                        ErrorMessage("\tIlyen menüpont nincs!");
                        Console.WriteLine("Add meg újra!");
                    }
                }
            } while (!valid);
            Console.Write("\n");
            return readNumber;
        }
        public static string ReadLine()
        {
            Gray();
            Console.Write("#");
            string read = Console.ReadLine();
            White();
            return read;
        }
        public static string ReadLine(string msg)
        {
            Console.Write(msg);
            return ReadLine();
        }
        
        public static void StartScreen()
        {
            Console.Clear();
            string[] menu = new string[] {
                "Online rendelés",
                "Étlap megtekintése",
                "Admin bejelentkezés",
                "Súgó",
                "Adatbázis alaphelyzetbe állítása",
                "Program bezárása"
            };
            switch (ReadMenu(menu))
            {
                case 1:
                    Online();
                    break;
                case 2:
                    ProductsOnlineDisplay();
                    break;
                case 3:
                    LoginScreen();
                    break;
                case 4:
                    Helper();
                    break;
                case 5:
                    RestoreDatabaseMethod();
                    break;
                case 6:
                    Console.Write("Kilépés...");
                    Delay();
                    Environment.Exit(0);
                    break;
            }
        }
        public static void LoginScreen()
        {
            Console.Clear();
            Console.WriteLine("Add meg a bejelentkezési adatokat!\n");
            string username = ReadLine("\tFelhasználónév: ");
            string password = ReadLine("\tJelszó: ");
            Console.Write('\n');
            if (Worker.TryLogin(username,password))
            {
                SuccessMessage("Sikeres bejelentkezés!");
                Delay();
                AdminMainMenu();
            }
            else
            {
                ErrorMessage("Sikertelen bejelentkezés!");
                Delay();
                StartScreen();
            }
            Console.ReadKey();
            
        }
        public static void Online()
        {
            Console.Clear();
            string[] menu = new string[] {
                "Új vásárló",
                "Visszatérő vásárló",
                "Vissza"
            };
            switch (ReadMenu(menu))
            {
                case 1:
                    NewCustomer();
                    break;
                case 2:
                    ExistsCustomer();
                    break;
                case 3:
                    StartScreen();
                    break;
            }
        }
        public static void NewCustomer()
        {
            Console.Clear();
            Console.WriteLine("Add meg a személyes adatokat!\n");
            try
            {
                string lastname = ReadLine("\tVezetéknév: ");
                string firstname = ReadLine("\tKeresztnév: ");
                string email = ReadLine("\tEmail: ");
                string postcode = ReadLine("\tIrányítószám: ");
                string city = ReadLine("\tVáros: ");
                string address = ReadLine("\tCím: ");
                string phone = ReadLine("\tTelefonszám: ");

                Customer customer = new Customer(firstname, lastname, email, address, city, int.Parse(postcode), phone);
                customer.Upload();

                SuccessMessage("\nAdatok bevitele sikeres!");

                Customer.CurrentCustomer = customer;
                Order.CurrentOrder = new Order(Customer.CurrentCustomer);
            }
            catch (Exception)
            {
                ErrorMessage("Sikertelen!");
                Delay();
                Online();
            }
            Delay();
            Cart();
        }
        public static void ExistsCustomer()
        {
            Console.Clear();
            Console.WriteLine("Eddigi vásárlók:\n");
            foreach (var item in Customer.GetAll())
            {
                Console.WriteLine($"\t{item.Id}. - {item.ToStringShort()}");
            }
            Console.WriteLine("\nAdd meg a vásárló azonosítóját!");
            try
            {
                Customer.CurrentCustomer = Customer.GetCustomerById(ReadIntegerToCustomer());
                Order.CurrentOrder = new Order(Customer.CurrentCustomer);
                SuccessMessage("\nSikeres kiválasztás!");
            }
            catch (Exception)
            {
                ErrorMessage("\nHiba történt!");
                Delay();
                Online();
            }
            Delay();
            Cart();
        }
        public static int ReadIntegerToCustomer()
        {
            bool valid;
            int readNumber = 0;
            do
            {
                valid = true;
                try
                {
                    readNumber = int.Parse(ReadLine());
                }
                catch (Exception)
                {
                    valid = false;
                }

                if (!valid)
                {
                    valid = false;
                    ErrorMessage("\tNem számot adtál meg!");
                    Console.WriteLine("Add meg újra!");
                }
                else
                {
                    if (!Customer.CustomerExistsById(readNumber))
                    {
                        valid = false;
                        ErrorMessage("\tIlyen vásárló nincs!");
                        Console.WriteLine("Add meg újra!");
                    }
                }
            } while (!valid);
            return readNumber;
        }
        public static void Cart()
        {
            Console.Clear();
            Console.WriteLine("A vásárló adatai:\n");
            Console.WriteLine(Customer.CurrentCustomer.ToStringUser()+"\n");
            Console.WriteLine("Termékek:\n");

            
            foreach (var item in Product.GetAll())
            {
                if (item.Name.Length < 40)
                {
                    for (int i = 0; i < 40 - item.Name.Length; i++)
                    {
                        item.Name = item.Name + " ";
                    }
                }
                Console.WriteLine($"\t{item.Id}. {item.Name}\t{item.PriceToString()}");
            }

            Console.WriteLine("\nA kosár tartalma: \n");
            if (Order.CurrentOrder.Products.Count == 0)
            {
                Console.WriteLine("\tA kosár üres!\n");
            }
            else
            {
                foreach (var item in Order.CurrentOrder.Products)
                {
                    Console.WriteLine($"\t{item.Name} - {item.PriceToString()}");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\tÖsszesen fizetendő: {Order.CurrentOrder.FullPrice} Ft");
            Console.WriteLine("\nAdd meg a termék azonosítóját! A rendelés véglegesítéséhez add meg a 0-t!");
            int readNumber = ReadIntegerToOrder();
            if (readNumber == 0)
            {
                Console.Clear();
                try
                {
                    Order.CurrentOrder.Upload();
                    SuccessMessage("A rendelés sikeresen leadva!");
                }
                catch (Exception)
                {
                    ErrorMessage("A rendelés leadása sikertelen!");
                }
                Delay();
                Online();
            }
            else
            {
                Order.CurrentOrder.AddProduct(readNumber);
            }
            Cart();
        }
        public static int ReadIntegerToOrder()
        {
            bool valid;
            int readNumber = 0;
            do
            {
                valid = true;
                try
                {
                    readNumber = int.Parse(ReadLine());
                }
                catch (Exception)
                {
                    valid = false;
                }

                if (!valid)
                {
                    valid = false;
                    ErrorMessage("\tNem számot adtál meg!");
                    Console.WriteLine("Add meg újra!");
                }
                else
                {
                    if (readNumber == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        if (!Product.ProductExistsById(readNumber))
                        {
                            valid = false;
                            ErrorMessage("\tIlyen termék nincs!");
                            Console.WriteLine("Add meg újra!");
                        }
                    }
                }
            } while (!valid);
            return readNumber;
        }
        public static void ProductsOnlineDisplay()
        {
            Console.Clear();
            DarkYellow();
            int width = 39;
            for (int i = 0; i < width; i++)
                Console.Write("#");
            Console.WriteLine();
            for (int i = 0; i < width; i++)
                Console.Write("-");
            Console.WriteLine();
            foreach (var item in Product.GetAll())
            {
                if (item.Name.Length < 40)
                {
                    for (int i = 0; i < 40-item.Name.Length; i++)
                    {
                        item.Name = item.Name + " ";
                    }
                }
                Console.WriteLine($"{item.Name}\t{item.PriceToString()}");
                for (int i = 0; i < width; i++)
                    Console.Write("-");
                Console.WriteLine();
            }
            for (int i = 0; i < width; i++)
                Console.Write("#");
            Console.WriteLine();
            White();
            Back();
            StartScreen();
        }
        public static void AdminMainMenu()
        {
            Console.Clear();
            Console.WriteLine(Worker.CurrentUser.Welcome());
            Console.Write("\n");
            string[] menu = new string[] {
                "Rendelések megtekintése",
                "Rendelés törlése",
                "Számla nyomtatása",
                "Termékek",
                "Étlap nyomtatása",
                "Beállítások",
                "Kijelentkezés"
            };
            switch (ReadMenu(menu))
            {
                case 1:
                    OrderDisplay();
                    break;
                case 2:
                    DeleteOrder();
                    break;
                case 3:
                    InvoicePrint();
                    break;
                case 4:
                    Products();
                    break;
                case 5:
                    ProductsPrint();
                    break;
                case 6:
                    AdminSettings();
                    break;
                case 7:
                    Worker.LogOut();
                    SuccessMessage("Sikeres kijelentkezés!");
                    Delay();
                    StartScreen();
                    break;
            }
        }
        public static void InvoicePrint()
        {
            Console.Clear();
            if (Order.GetAll().Count == 0)
            {
                Console.WriteLine("Nincs a rendszerben megrendelés!");
                Back();
            }
            else
            {
                Console.WriteLine("Rendelések:\n");
                foreach (var item in Order.GetAll())
                {
                    Console.WriteLine(item.ToStringShort());
                }
                Console.WriteLine("Add meg a rendelés azonosítóját!");
                try
                {
                    string filename = Print.PrintInvoice(Order.GetOrderById(ReadIntegerToOrderId()));
                    SuccessMessage("A számla kinyomtatása sikeres!");
                    Console.WriteLine($"Elérési útvonal: {filename}");
                }
                catch (Exception)
                {
                    ErrorMessage("\nSikertelen nyomtatás!");
                }
                //Delay();
            }
            Console.ReadKey();
            AdminMainMenu();
        }
        public static void ProductsPrint()
        {
            Console.Clear();
            try
            {
                string filename = Print.PrintProducts();
                SuccessMessage("Az étlap kinyomtatása sikeres!");
                Console.WriteLine($"Elérési útvonal: {filename}");
                Back();
                AdminMainMenu();
            }
            catch (Exception)
            {
                ErrorMessage("Az étlap kinyomtatása sikertelen!");
                Delay();
                AdminMainMenu();
            }
            
        }
        
        public static void OrderDisplay()
        {
            Console.Clear();
            Console.WriteLine("Rendelések:\n");
            foreach (var item in Order.GetAll())
            {
                Console.WriteLine(item);
            }
            Back();
            AdminMainMenu();
        }
        public static void DeleteOrder()
        {
            Console.Clear();
            if (Order.GetAll().Count == 0)
            {
                Console.WriteLine("Nincs a rendszerben megrendelés!");
                Back();
            }
            else
            {
                Console.WriteLine("Rendelések:\n");
                foreach (var item in Order.GetAll())
                {
                    Console.WriteLine(item.ToStringShort());
                }
                Console.WriteLine("Add meg a törölni kívánt rendelés azonosítóját!");
                try
                {
                    Order.GetOrderById(ReadIntegerToOrderId()).DeleteFromDatabase();
                    SuccessMessage("\nSikeres törlés!");
                }
                catch (Exception)
                {
                    ErrorMessage("\nSikertelen törlés!");
                }
                //Delay();
            }
            Console.ReadKey();
            AdminMainMenu();
        }
        public static int ReadIntegerToOrderId()
        {
            bool valid;
            int readNumber = 0;
            do
            {
                valid = true;
                try
                {
                    readNumber = int.Parse(ReadLine());
                }
                catch (Exception)
                {
                    valid = false;
                }

                if (!valid)
                {
                    valid = false;
                    ErrorMessage("\tNem számot adtál meg!");
                    Console.WriteLine("Add meg újra!");
                }
                else
                {
                    if (readNumber == 0)
                    {
                        return 0;
                    }
                    else
                    {
                        if (!Order.OrderExistsById(readNumber))
                        {
                            valid = false;
                            ErrorMessage("\tIlyen rendelés nincs!");
                            Console.WriteLine("Add meg újra!");
                        }
                    }
                }
            } while (!valid);
            return readNumber;
        }
        public static void Products()
        {
            Console.Clear();
            string[] menu = new string[] {
                "Termékek megtekintése",
                "Új termék rögzítése",
                "Termék módosítása",
                "Termék törlése",
                "Vissza"
            };
            switch (ReadMenu(menu))
            {
                case 1:
                    ProductsDisplay();
                    break;
                case 2:
                    NewProduct();
                    break;
                case 3:
                    UpdateProduct();
                    break;
                case 4:
                    DeleteProduct();
                    break;
                case 5:
                    AdminMainMenu();
                    break;
            }
        }
        public static void ProductsDisplay()
        {
            Console.Clear();
            Console.WriteLine("Termékek:\n");
            foreach (var item in Product.GetAll())
            {
                Console.WriteLine(item+"\n");
            }
            Back();
            Products();
        }
        public static void NewProduct()
        {
            Console.Clear();
            Console.WriteLine("Új termék rögzítése:\n");
            try
            {
                Product newProduct = new Product(ReadLine("\tElnevezés: "), ReadLine("\tLeírás: "), int.Parse(ReadLine("\tÁr: ")));
                newProduct.Upload();
                SuccessMessage("\nA feltöltés sikeres!");
            }
            catch (Exception)
            {
                ErrorMessage("\nA feltöltés sikertelen!");
            }
            Delay();
            Products();
        }
        public static void UpdateProduct()
        {
            Console.Clear();
            Console.WriteLine("Válaszd ki a módosítandó terméket!\n");
            for (int i = 0; i < Product.GetAll().Count; i++)
            {
                Console.WriteLine($"{Product.GetAll()[i].Id}. {Product.GetAll()[i].ToStringShort()}\n");
            }
            Console.WriteLine("Add meg a termék azonosítóját!");
            int product_id = ReadIntegerToProduct();
            Console.Clear();
            Console.WriteLine("A módosítandó termék:\n");
            Product product = Product.GetProductById(product_id);
            Console.WriteLine(product+"\n");
            Console.WriteLine("Add meg a termék új adatait!\nHa az adott mezót nem kívánod megváltoztatni, ne írj be semmit!\n");
            try
            {
                product.Update(ReadLine("\tElnevezés: "), ReadLine("\tLeírás: "), ReadLine("\tÁr: "));
                SuccessMessage("\nSikeres módosítás!");
            }
            catch (Exception)
            {
                ErrorMessage("\nSikertelen módosítás!");
            }
            Delay();
            Products();
        }
        public static int ReadIntegerToProduct()
        {
            bool valid;
            int readNumber = 0;
            do
            {
                valid = true;
                try
                {
                    readNumber = int.Parse(ReadLine());
                }
                catch (Exception)
                {
                    valid = false;
                }

                if (!valid)
                {
                    valid = false;
                    ErrorMessage("\tNem számot adtál meg!");
                    Console.WriteLine("Add meg újra!");
                }
                else
                {
                    if (!Product.ProductExistsById(readNumber))
                    {
                        valid = false;
                        ErrorMessage("\tIlyen termék nincs!");
                        Console.WriteLine("Add meg újra!");
                    }
                }
            } while (!valid);
            return readNumber;
        }
        public static void DeleteProduct()
        {
            Console.Clear();
            Console.WriteLine("Termékek:\n");
            for (int i = 0; i < Product.GetAll().Count; i++)
            {
                Console.WriteLine($"{Product.GetAll()[i].Id}. {Product.GetAll()[i].Name}\n");
            }
            Console.WriteLine("Add meg a törölni kívánt termék azonosítóját!");
            try
            {
                Product.GetProductById(ReadIntegerToProduct()).DeleteFromDatabase();
                SuccessMessage("\nSikeres törlés!");
            }
            catch (Exception)
            {
                ErrorMessage("\nSikertelen törlés!");
            }
            Delay();
            Products();
        }
        public static void AdminSettings()
        {
            Console.Clear();
            string[] menu = new string[] {
                "Adataim megtekintése",
                "Jelszó megváltoztatása",
                "Vissza"
            };
            switch (ReadMenu(menu))
            {
                case 1:
                    CurrentUserDisplay();
                    break;
                case 2:
                    ChangePassword();
                    break;
                case 3:
                    AdminMainMenu();
                    break;
            }
        }
        public static void CurrentUserDisplay()
        {
            Console.Clear();
            Console.Write('\n');
            Console.WriteLine(Worker.CurrentUser);
            Back();
            AdminSettings();
        }
        public static void ChangePassword()
        {
            Console.Clear();
            Console.WriteLine("Add meg a régi jelszót kétszer, majd az új jelszót!\n");
            if (Worker.CurrentUser.ChangePassword(ReadLine("\tRégi jelszó: "), ReadLine("\tÚj jelszó: ")))
            {
                SuccessMessage("\nA jelszó megváltoztatása sikeres!");
                Delay();
                AdminSettings();
            }
            else
            {
                ErrorMessage("\nA jelszó megváltoztatása sikertelen!");
                Delay();
                AdminSettings();
            }
        }
        public static void RestoreDatabaseMethod()
        {
            Console.Clear();
            try
            {
                RestoreDatabase.DropTables();
                RestoreDatabase.CreateTables();
                RestoreDatabase.InsertDatas();
                Console.WriteLine("\nAz adatbázis alaphelyzetbe állítva!");
                Delay();
                StartScreen();
            }
            catch (Exception)
            {
                ErrorMessage("\nAz adatbázis alaphelyzetbe állítása sikertelen!");
                Delay();
                StartScreen();
            }
        }
        public static void Back()
        {
            Console.Write("Visszalépéshez nyomj meg egy gombot!");
            Console.ReadKey();
        }
        public static void ErrorMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void SuccessMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Delay()
        {
            System.Threading.Thread.Sleep(1000);
        }
        #region Color
        public static void White()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Yellow()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        public static void Blue()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
        }
        public static void Cyan()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
        }
        public static void Red()
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        public static void Gray()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public static void DarkYellow()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }
        #endregion
        public static void Helper()
        {
            Console.Clear();
            Yellow();
            Console.WriteLine("Súgó!");
            Console.WriteLine("Itt le van írva minden fontos tudni való!");
            White();
            Back();
            StartScreen();
        }
        public static void BreakRow()
        {
            Console.Write("\n");
        }
    }
}
