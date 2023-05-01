using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;

namespace WindowsFormsDemoDB.Assignment_Diconnected_A
{
    public partial class Form2Student : Form
    {

        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form2Student()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);

        }

        private DataSet GetAllStudent()
        {
            string qry = "select * from Student1";
            da = new SqlDataAdapter(qry, con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//aad pkto col which is in dataset
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Stud");//prod is table given to the dataset table
            return ds;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                DataRow row = ds.Tables["Stud"].NewRow();
                row["StudentName"] = textSName.Text;
                row["Branch"] = textBranch.Text;
                row["Percentage"] = textPercentage.Text;
                // add new row in the DataTable
                ds.Tables["Stud"].Rows.Add(row);
                int res = da.Update(ds.Tables["Stud"]); // reflect the changes from DataSet to DB
                if (res > 0)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                //Find() will only work if it is PK col
                DataRow row = ds.Tables["Stud"].Rows.Find(textRollNo.Text);
                if (row != null)
                {
                    textSName.Text = row["StudentName"].ToString();
                    textBranch.Text = row["Branch"].ToString();
                    textPercentage.Text = row["Percentage"].ToString();

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
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                //Find() will only work if it is PK col
                DataRow row = ds.Tables["Stud"].Rows.Find(textRollNo.Text);
                if (row != null)
                {
                    row["StudentName"] = textSName.Text;
                    row["Branch"] = textBranch.Text;
                    row["Percentage"] = textPercentage.Text;
                    int res = da.Update(ds.Tables["Stud"]); // reflect the changes from DataSet to DB
                    if (res > 0)
                    {
                        MessageBox.Show("Record updated");
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

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                //Find() will only work if it is PK col
                DataRow row = ds.Tables["Stud"].Rows.Find(textRollNo.Text);
                if (row != null)
                {
                    row.Delete();// removes from DataSet
                    int res = da.Update(ds.Tables["Stud"]); // reflect the changes from DataSet to DB
                    if (res > 0)
                    {
                        MessageBox.Show("Record deleted");
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
        }

        private void buttonShowAllStudent_Click(object sender, EventArgs e)
        {
            ds = GetAllStudent();
            dataGridView1.DataSource = ds.Tables["Stud"];
        }
    }
}
    
    

