using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml.Linq;


namespace WindowsFormsDemoDB
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void butSave_Click(object sender, EventArgs e)
        {
            try
            {
                //step 1
                string qry = "insert into Emp1 values(@name,@sal)";
                //step 2
                cmd = new SqlCommand(qry, con);
                //step 3
                cmd.Parameters.AddWithValue("@name", textname.Text);
                cmd.Parameters.AddWithValue("@sal", Convert.ToDecimal(textsalary.Text));
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
            textid.Clear();
            textname.Clear();
            textsalary.Clear();
        }
            private void butUpdate_Click(object sender, EventArgs e)
            {
                try
                {
                    //step 1
                    string qry = "update Emp1 set name=@name, salary=@sal where id=@id";
                    //step 2
                    cmd = new SqlCommand(qry, con);
                    //step 3
                    cmd.Parameters.AddWithValue("@name", textname.Text);
                    cmd.Parameters.AddWithValue("@sal", Convert.ToDecimal(textsalary.Text));
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textid.Text));
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


        

        private void butSearch_Click(object sender, EventArgs e)
        {
            
                try
                {
                    string qry = "select * from Emp1 where id=@id";
                    cmd = new SqlCommand(qry, con);
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textid.Text));
                    con.Open();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows) // if record is present then return true else false
                    {
                        while (dr.Read())
                        {
                            textname.Text = dr["name"].ToString();
                            textsalary.Text = dr["salary"].ToString();
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





            private void butDelete_Click(object sender, EventArgs e)
            {
                try
                {
                    //step 1
                    string qry = "delete from Emp1 where id=@id";
                    //step 2
                    cmd = new SqlCommand(qry, con);
                    //step 3
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt32(textid.Text));
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

        

        private void butshowallemp_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Emp1";
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



   
