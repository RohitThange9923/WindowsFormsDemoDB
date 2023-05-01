using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WindowsFormsDemoDB
{
    public partial class Form2Product : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2Product()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);
        }

        

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "insert into Product values(@productName,@Company,@Price)";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@productName", textName.Text);
                cmd.Parameters.AddWithValue("@Company", textCompany.Text);
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(textPrice.Text));
                con.Open();
                // step 5 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void ClearForm()
        {
            textID.Clear();
            textName.Clear();
            textCompany.Clear();
            textPrice.Clear();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "update Product set productName=@productName, Company=@Company,Price=@Price where productID=@productID";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@productName", textName.Text);
                cmd.Parameters.AddWithValue("@Company", textCompany.Text);
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(textPrice.Text));
                cmd.Parameters.AddWithValue("@productID", Convert.ToInt32(textID.Text));
                con.Open();
                // step 5 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Record updated");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

            try
            {
                string qry = "select * from Product where productID=@productID";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@productID", Convert.ToInt32(textID.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) // if record is present then return true else false
                {
                    while (dr.Read())
                    {
                        textName.Text = dr["productName"].ToString();
                        textCompany.Text = dr["Company"].ToString();
                        textPrice.Text = dr["Price"].ToString();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "delete from Product where productID=@productID";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@productID", Convert.ToInt32(textID.Text));
                con.Open();
                // step 5 
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {
                    MessageBox.Show("Record deleted");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void buttonShowALLProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Product";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) // if record is present then return true else false
                {
                    DataTable table = new DataTable();
                    table.Load(dr);// Load() convert object in to table format
                    dataGridView1.DataSource = table;
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally  
            {
                con.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

    
    
   

