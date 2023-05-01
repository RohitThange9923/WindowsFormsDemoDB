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
    public partial class Form3Student : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form3Student()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);
        }

        

        private void textRollNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "insert into Student values(@StudentName,@Branch,@Percentage)";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@StudentName", textSName.Text);
                cmd.Parameters.AddWithValue("@Branch", textBranch.Text);
                cmd.Parameters.AddWithValue("@Percentage", Convert.ToDecimal(textPercentage.Text));
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
            textRollNo.Clear();
            textSName.Clear();
            textBranch.Clear();
            textPercentage.Clear();
        }
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "update Student set StudentName=@StudentName, Branch=@Branch,Percentage=@Percentage where RollNo=@RollNo";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
            
                cmd.Parameters.AddWithValue("@StudentName", textSName.Text);
                cmd.Parameters.AddWithValue("@Branch", textBranch.Text);
                cmd.Parameters.AddWithValue("@Percentage", Convert.ToDecimal(textPercentage.Text));
                cmd.Parameters.AddWithValue("@RollNo", Convert.ToInt32(textRollNo.Text));
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
                string qry = "select * from Student where RollNo=@RollNo";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@RollNo", Convert.ToInt32(textRollNo.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows) // if record is present then return true else false
                {
                    while (dr.Read())
                    {
                        textSName.Text = dr["StudentName"].ToString();
                        textBranch.Text = dr["Branch"].ToString();
                        textPercentage.Text = dr["Percentage"].ToString();
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

        

        private void buttonShowAllStudent_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Student";
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

        private void buttonDelete_Click_1(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "delete from Student where RollNo=@RollNo";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@RollNo", Convert.ToInt32(textRollNo.Text));
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

    }
    }
    

    
