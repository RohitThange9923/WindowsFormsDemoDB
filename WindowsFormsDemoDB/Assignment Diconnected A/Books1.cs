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
    public partial class Form3BookDisconnected : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;
        public Form3BookDisconnected()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);

        }

        private DataSet GetAllBook()
        {
            string qry = "select * from Book1";
            da = new SqlDataAdapter(qry, con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//aad pkto col which is in dataset
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Book");//book is table given to the dataset table
            return ds;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllBook();
                DataRow row = ds.Tables["Book"].NewRow();
                row["BookName"] = textBN.Text;
                row["Author"] = textAuthor.Text;
                row["Price"] = textPrice.Text;
                // add new row in the DataTable
                ds.Tables["Book"].Rows.Add(row);
                int res = da.Update(ds.Tables["Book"]); // reflect the changes from DataSet to DB
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
                ds = GetAllBook();
                //Find() will only work if it is PK col
                DataRow row = ds.Tables["Book"].Rows.Find(textBId.Text);
                if (row != null)
                {
                    textBN.Text = row["BookName"].ToString();
                    textAuthor.Text = row["Author"].ToString();
                    textPrice.Text = row["Price"].ToString();

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
                ds = GetAllBook();
                //Find() will only work if it is PK col
                DataRow row = ds.Tables["Book"].Rows.Find(textBId.Text);
                if (row != null)
                {
                    row["BookName"] = textBN.Text;
                    row["Author"] = textAuthor.Text;
                    row["Price"] = textPrice.Text;
                    int res = da.Update(ds.Tables["Book"]); // reflect the changes from DataSet to DB
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
                ds = GetAllBook();
                //Find() will only work if it is PK col
                DataRow row = ds.Tables["Book"].Rows.Find(textBId.Text);
                if (row != null)
                {
                    row.Delete();// removes from DataSet
                    int res = da.Update(ds.Tables["Book"]); // reflect the changes from DataSet to DB
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

        private void buttonSAB_Click(object sender, EventArgs e)
        {
            ds = GetAllBook();
            dataGridView1.DataSource = ds.Tables["Book"];
        }
    }
}

