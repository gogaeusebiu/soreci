using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (username.Text.CompareTo("admin") + password.Text.CompareTo("admin") == 0)
            {
                AdminForm admin = new AdminForm();
                admin.Show();
                username.Text = "";
                password.Text = "";
            }else
                try
                {
                    int k = 0;
                    con.Open();
                    comm.Connection = con;
                    comm.CommandText = "select username, passw from AppUsers";
                    SqlDataReader reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        if (reader.GetString(0).CompareTo(username.Text) + reader.GetString(1).CompareTo(password.Text) == 0)
                            k = 1;
                    }

                    if (k == 1)
                    {
                        UserForm client = new UserForm();

                        client.Show(username.Text);

                    }
                    else
                    {
                        MessageBox.Show("Username or password incorrect!");
                    }
                    reader.Close();
                    con.Close();
                    username.Text = "";
                    password.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
