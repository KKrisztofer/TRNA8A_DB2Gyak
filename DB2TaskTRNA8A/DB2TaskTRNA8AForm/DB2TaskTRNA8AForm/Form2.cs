using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB2TaskTRNA8AForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void logoutBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //loginWindow a = new loginWindow();
            //a.Show();
            //this.Hide();
        }
        public async Task RefreshGridAsync()
        {
            while (true)
            {
                ordersGrid.Rows.Clear();
                ordersGrid.ColumnCount = 5;
                ordersGrid.Columns[0].Name = "Név";
                ordersGrid.Columns[1].Name = "Cím";
                ordersGrid.Columns[2].Name = "Telefonszám";
                ordersGrid.Columns[3].Name = "Termékek";
                ordersGrid.Columns[4].Name = "Ár";
                
                foreach (var item in Order.GetAll())
                {
                    string[] row = new string[] { item.Customer.Fullname, item.Customer.City, item.Customer.Phone, item.GetProductToString(), $"{item.FullPrice} Ft" };
                    ordersGrid.Rows.Add(row);
                }
                await Task.Delay(2000);
            }
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Task task = RefreshGridAsync();
        }
    }
}
