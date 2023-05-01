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
    public partial class Form3Book : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;

        public Form3Book()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString);

        }

        private DataSet GetAllProduct()
        {
            string qry = "select * from Product1";
            da = new SqlDataAdapter(qry, con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;//aad pkto col which is in dataset
            scb= new SqlCommandBuilder(da);
            ds= new DataSet();
            da.Fill(ds, "Prod");//prod is table given to the dataset table
            return ds;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProduct();
                DataRow row = ds.Tables["Prod"].NewRow();
                row["productName"]=textName.Text;
                row["Company"]=textCompany.Text;
                row["Price"] =textPrice.Text;
                // add new row in the DataTable
                ds.Tables["Prod"].Rows.Add(row);
                int res = da.Update(ds.Tables["Prod"]); // reflect the changes from DataSet to DB
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
                ds = GetAllProduct();
                //Find() will only work if it is PK col
                DataRow row = ds.Tables["Prod"].Rows.Find(textID.Text);
                if (row != null)
                {
                    textName.Text = row["productName"].ToString();
                    textCompany.Text = row["Company"].ToString();
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



        

    


    private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProduct();
                //Find() will only work if it is PK col
                DataRow row = ds.Tables["Prod"].Rows.Find(textID.Text);
                if (row != null)
                {
                    row.Delete();// removes from DataSet
                    int res = da.Update(ds.Tables["Prod"]); // reflect the changes from DataSet to DB
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

        private void buttonShowALLProduct_Click(object sender, EventArgs e)
        {

            ds = GetAllProduct();
            dataGridView1.DataSource = ds.Tables["Prod"];

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllProduct();
                //Find() will only work if it is PK col
                DataRow row = ds.Tables["Prod"].Rows.Find(textID.Text);
                if (row != null)
                {
                    row["productName"] = textName.Text;
                    row["Company"] = textCompany.Text;
                    row["Price"] = textPrice.Text;
                    int res = da.Update(ds.Tables["Prod"]); // reflect the changes from DataSet to DB
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

    }
}



 