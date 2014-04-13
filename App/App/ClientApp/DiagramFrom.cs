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
    public partial class DiagramFrom : Form
    {
        public DiagramFrom()
        {
            InitializeComponent();
        }

        private void DiagramFrom_Load(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                comm.Connection = con;
                comm.CommandText = "select product, points, price from Products";
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {

                    table.Rows.Add(reader.GetValue(0).ToString(), reader.GetValue(1).ToString(), reader.GetValue(2).ToString());

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
