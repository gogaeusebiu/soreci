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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            loadListUsers();
            loadListCat();
            try
            {

                con.Open();
                comm.Connection = con;
                comm.CommandText = "select codC, category from Category";
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    categoryComb.Items.Add(reader.GetValue(0).ToString() + "-" + reader.GetString(1));
                }

                reader.Close();
                comm.Cancel();
                comm.CommandText = "select codG, groupName from Groups";
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    groupsN.Items.Add(reader.GetValue(0).ToString() + "-" + reader.GetString(1));
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadListCat()
        {

            try
            {
                listCat.Items.Clear();
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select category from Category";
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    listCat.Items.Add(reader.GetValue(0).ToString());
                }

                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadListProd()
        {
            try
            {
                listProd.Items.Clear();
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select product from Products, Category where Category.codC = Products.category and Category.category = '" + listCat.SelectedItem.ToString() + "'";
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    listProd.Items.Add(reader.GetValue(0).ToString());
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void loadListUsers()
        {
            try
            {
                listUsers.Items.Clear();
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select username from AppUsers";
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    listUsers.Items.Add(reader.GetValue(0).ToString());
                }
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listUsers.SelectedIndex > -1)
            {
                try
                {
                    con.Open();
                    comm.Connection = con;
                    comm.CommandText = "update AppUsers set points = " + points.Text + " where username = '" + listUsers.SelectedItem.ToString() + "'";
                    comm.ExecuteNonQuery();
                    con.Close();
                    clearTextBoxesUser();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select an user!");
            }
        }

        private void clearTextBoxesUser()
        {
            user.Clear();
            points.Clear();
            groupsN.ResetText();
            password.Clear();
        }
        private void clearTextBoxesProduct()
        {

            product.Clear();
            categoryComb.ResetText();
            pPoints.Clear();
            price.Clear();
        }

        private bool testIfExistUser(String _username)
        {
            List<String> list = new List<String>();
            try
            {
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select username from AppUsers";
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader.GetValue(0).ToString());
                }
                reader.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (list.Contains(_username))
                return true;
            return false;
        }

        private bool testIfExistProd(String _prod)
        {
            List<String> list = new List<String>();
            try
            {
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select product from Products";
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader.GetValue(0).ToString());
                }
                reader.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (list.Contains(_prod))
                return true;
            return false;
        }

        private bool testIfExistCat(String _cat)
        {
            List<String> list = new List<String>();
            try
            {
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select category from Category";
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(reader.GetValue(0).ToString());
                }
                reader.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (list.Contains(_cat))
                return true;
            return false;

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (!testIfExistUser(user.Text))
            {
                if (testIfEmptyForUser())
                {
                    try
                    {
                        con.Open();
                        comm.CommandText = "insert into AppUsers values('" + user.Text + "','" + password.Text + "'," + points.Text + ", " + groupsN.SelectedItem.ToString().Split('-')[0] + ")";
                        comm.Connection = con;
                        comm.ExecuteNonQuery();
                        con.Close();
                        clearTextBoxesUser();
                        loadListUsers();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please complete all fields for user!");
                }
            }
            else
            {
                MessageBox.Show("This username: " + user.Text + " is taken!");
            }
        }

        private bool testIfEmptyForProd()
        {

            if (String.IsNullOrEmpty(product.Text))
                return false;
            if (String.IsNullOrEmpty(price.Text))
                return false;
            if (categoryComb.SelectedIndex < 0)
                return false;
            return true;
        }
        private bool testIfEmptyForUser()
        {

            if (String.IsNullOrEmpty(user.Text))
                return false;
            if (String.IsNullOrEmpty(password.Text))
                return false;
            if (String.IsNullOrEmpty(points.Text))
                return false;
            if (groupsN.SelectedIndex < 0)
                return false;
            return true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (!testIfExistProd(product.Text))
            {
                if (testIfEmptyForProd())
                {
                    try
                    {
                        con.Open();
                        comm.Connection = con;
                        comm.CommandText = "insert into Products values('" + product.Text + "', " + categoryComb.SelectedItem.ToString().Split('-')[0] + ", " + price.Text + ", 0)";
                        comm.ExecuteNonQuery();
                        con.Close();
                        clearTextBoxesProduct();
                        loadListProd();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please complete all fields for product!");
                }
            }
            else { MessageBox.Show("This product: " + product.Text + " exists!"); }
        }

        private void listUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                con.Open();
                comm.Connection = con;
                comm.CommandText = "select username, passw, points, groupUser from AppUsers where username ='" + listUsers.SelectedItem.ToString() + "'";
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                user.Text = reader.GetValue(0).ToString();
                password.Text = reader.GetValue(1).ToString();
                points.Text = reader.GetValue(2).ToString();
                String group = reader.GetValue(3).ToString();


                groupsN.SelectedIndex = Convert.ToInt32(group) - 1;
                reader.Close();
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
                comm.CommandText = "select product, category, price, points from Products where product = '" + listProd.SelectedItem.ToString() + "'";
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                product.Text = reader.GetValue(0).ToString();
                price.Text = reader.GetValue(2).ToString();
                pPoints.Text = reader.GetValue(3).ToString();
                String cat = reader.GetValue(1).ToString();
                categoryComb.SelectedIndex = Convert.ToInt32(cat) - 1;
                reader.Close();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listProd.SelectedIndex > -1)
            {
                try
                {

                    con.Open();
                    comm.Connection = con;
                    comm.CommandText = "update Products set price =" + price.Text + " where product = '" + listProd.SelectedItem.ToString() + "'";
                    comm.ExecuteNonQuery();
                    con.Close();
                    clearTextBoxesProduct();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else { MessageBox.Show("Please select a product!"); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!testIfExistCat(category.Text))
            {
                try
                {

                    con.Open();
                    comm.Connection = con;
                    int codCat = listCat.Items.Count + 1;
                    comm.CommandText = "insert into Category values(" + codCat + ", '" + category.Text + "')";
                    comm.ExecuteNonQuery();
                    con.Close();
                    loadListCat();
                    category.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("This category: " + category.Text + " exists!");
            }
        }

        private void listCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadListProd();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DiagramFrom diagram = new DiagramFrom();
            diagram.Show();
        }
    }
}
 //private void button1_Click(object sender, EventArgs e)
 //       {
 //           try
 //           {
 //               System.Data.OleDb.OleDbConnection MyConnection ;
 //               System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
 //               string sql = null;
 //               MyConnection = new System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;Data Source='c:\\csharp.net-informations.xls';Extended Properties=Excel 8.0;");
 //               MyConnection.Open();
 //               myCommand.Connection = MyConnection;
 //               sql = "Insert into [Sheet1$] (id,name) values('5','e')";
 //               myCommand.CommandText = sql;
 //               myCommand.ExecuteNonQuery();
 //               MyConnection.Close();
 //           }
 //           catch (Exception ex)
 //           {
 //               MessageBox.Show (ex.ToString());
 //           }
 //       }