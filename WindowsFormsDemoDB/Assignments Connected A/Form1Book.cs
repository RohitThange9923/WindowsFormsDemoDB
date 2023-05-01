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

namespace WindowsFormsDemoDB.Assignments_Connected_A
{
    public partial class Form1Book : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1Book()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            try
            {
                //step 1
                string qry = "insert into Books values(@BookName,@Author,@Price)";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@BookName", textBN.Text);
                cmd.Parameters.AddWithValue("@Author", textAuthor.Text);
                cmd.Parameters.AddWithValue("@Price", Convert.ToInt32(textPrice.Text));
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
            textBId.Clear();
            textBN.Clear();
            textAuthor.Clear();
            textPrice.Clear();
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "update Books set BookName=@BookName, Author=@Author,Price=@Price where Bookid=@Bookid";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@BookName", textBN.Text);
                cmd.Parameters.AddWithValue("@Author", textAuthor.Text);
                cmd.Parameters.AddWithValue("@Price", Convert.ToDecimal(textPrice.Text));
                cmd.Parameters.AddWithValue("@Bookid", Convert.ToInt32(textBId.Text));
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
                string qry = "select * from Books where Bookid=@Bookid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@Bookid", Convert.ToInt32(textBId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) // if record is present then return true else false
                {
                    while (dr.Read())
                    {
                        textBN.Text = dr["BookName"].ToString();
                        textAuthor.Text = dr["Author"].ToString();
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
                string qry = "delete from Books where Bookid=@Bookid";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@Bookid", Convert.ToInt32(textBId.Text));
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

        private void buttonSAB_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Books";
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
    }
    }

 
    

