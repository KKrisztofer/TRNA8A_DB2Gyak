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
    public partial class loginWindow : Form
    {
        public loginWindow()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (Worker.TryLogin(username.Text, password.Text))
            {
                Form2 f2 = new Form2();
                f2.Show();
                //this.Hide();
                Visible = false;
            }
            else
            {
                MessageBox.Show("Rossz felhasználónév vagy jelszó!");
            }
        }

        private void loginWindow_Load(object sender, EventArgs e)
        {
            if (!Connection.CheckConnection())
            {
                MessageBox.Show("Az adatbázis offline!");
                this.Close();
            }
        }
    }
}
