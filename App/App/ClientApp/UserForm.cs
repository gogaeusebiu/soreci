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
    public partial class UserForm : Form
    {

        public UserForm()
        {
            InitializeComponent();
        }


        private void ClientForm_Load(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                comm.Connection = con;
                comm.CommandText = "select points from AppUsers where username = '" + username.Text + "'";
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();

                userPoints.Text = reader.GetValue(0).ToString();
                reader.Close();
                comm.Cancel();


                comm.CommandText = "select codC, category from Category";
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    comboCat.Items.Add(reader.GetValue(0).ToString() + "-" + reader.GetString(1));
                }


                reader.Close();
                con.Close();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Show(String _username)
        {
            username.Text = _username;
            base.Show();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select points from AppUsers where username = '" + username.Text + "'";
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();

                int uPoints = Convert.ToInt32(reader.GetValue(0));
                reader.Close();
                comm.Cancel();
                comm.CommandText = "select price from Products where  product = '" + listProd.SelectedItem.ToString() + "'";
                reader = comm.ExecuteReader();
                reader.Read();

                int price = Convert.ToInt32(reader.GetValue(0));
                reader.Close();
                comm.Cancel();
                if (uPoints >= price)
                {

                    comm.CommandText = "update Products set points = points + price where product = '" + listProd.SelectedItem.ToString() + "'";
                    comm.ExecuteNonQuery();
                    comm.Cancel();
                    comm.CommandText = "update AppUsers set points = points - " + price + " where username = '" + username.Text + "'";
                    comm.ExecuteNonQuery();
                    comm.Cancel();
                    comm.CommandText = "select points from AppUsers where username = '" + username.Text + "'";
                    SqlDataReader reader1 = comm.ExecuteReader();
                    reader1.Read();

                    userPoints.Text = Convert.ToString(Convert.ToInt32(userPoints.Text) - price);

                    reader.Close();

                }
                else
                {
                    MessageBox.Show("You do not have enough points!");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listProd_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select points from AppUsers where username = '" + username.Text + "'";
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                userPoints.Text = Convert.ToString(reader.GetValue(0));
                reader.Close();
                comm.Cancel();
                comm.Connection = con;
                comm.CommandText = "select price from Products where product = '" + listProd.SelectedItem.ToString() + "'";
                reader = comm.ExecuteReader();
                reader.Read();
                prodPrice.Text = Convert.ToString(reader.GetValue(0));
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                

                listProd.Items.Clear();
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select product from Products where category = "+comboCat.SelectedItem.ToString().Split('-')[0];
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    listProd.Items.Add(reader.GetString(0));
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }





    }
}
