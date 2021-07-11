using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4
{
    public partial class ProductData : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBaseLab4.mdb;";
        private OleDbConnection myConnection;
        private int min;
        private bool er1 = false;
        private bool er2 = false;
        public ProductData()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);
            MinTextBox.Enabled = false;
        }

        private void ProductData_Load(object sender, EventArgs e)
        {
            myConnection.Open();

            comboBox1.Items.Clear();
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Место] WHERE [Район]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            while (myOleDbDataReader.Read())
            {
                comboBox1.Items.Add(myOleDbDataReader[0]);
            }
            myOleDbDataReader.Close();

            myConnection.Close();
            MinTextBox.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (MinTextBox.Text != "")
            {
                bool res = int.TryParse(MinTextBox.Text, out min);
                if (res != true)
                {
                    MessageBox.Show("Введено не число или не целое");
                    MinTextBox.Text = MinTextBox.Text.Substring(0, MinTextBox.Text.Length - 1);
                    FoundButton.Enabled = false;
                    er1 = false;
                }
                else
                {
                    if (min < 0)
                    {
                        MessageBox.Show("Введено отрицательное число");
                        MinTextBox.Text = "";
                        FoundButton.Enabled = false;
                        er1 = false;
                    }
                    er1 = true;
                    if (er2 != false)
                        FoundButton.Enabled = true;
                }
            }
        }

        private void FoundButton_Click(object sender, EventArgs e)
        {
            
            myConnection.Open();

            dataGridView1.ColumnCount = 6;
            for (int k = 0; k < dataGridView1.ColumnCount; k++)
                dataGridView1.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns[0].HeaderText = "Код района";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].HeaderText = "Номер школы";
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].HeaderText = "Телефон";
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].HeaderText = "Год открытия";
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].HeaderText = "Количество учителей";
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].HeaderText = "Количество учеников";
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Width = 540;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Ведомость] WHERE "+ idcom.ToString() +" = [Код района]";
            OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
            int i = 0;
            dataGridView1.RowCount = 0;
            while (myOleDbDataReader1.Read())
            {
                int x = Convert.ToInt32(myOleDbDataReader1["Количество учеников"]);
                int y = Convert.ToInt32(myOleDbDataReader1["Количество учителей"]);
                if (x / y > min)
                {
                    dataGridView1.RowCount += 1;
                    dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader1["Код района"];
                    dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader1["Номер школы"].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader1["Телефон"];
                    dataGridView1.Rows[i].Cells[3].Value = myOleDbDataReader1["Год открытия"];
                    dataGridView1.Rows[i].Cells[4].Value = y.ToString();
                    dataGridView1.Rows[i].Cells[5].Value = x.ToString();
                    i++;
                }
            }
            myOleDbDataReader1.Close();

            myConnection.Close();
            MinTextBox.Text = "";
            MinTextBox.Enabled = false;
            comboBox1.SelectedItem = "";

        }
        int idcom = 0;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com = comboBox1.SelectedItem.ToString();
            myConnection.Open();
            MinTextBox.Enabled = true;
            FoundButton.Enabled = true;
            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Место]";
            OleDbDataReader myOleDbDataReader3 = myOleDbCommand.ExecuteReader();
            while (myOleDbDataReader3.Read())
            {
                if (myOleDbDataReader3["Район"].ToString() == com)
                {
                    idcom = Convert.ToInt32(myOleDbDataReader3["Код района"]);
                }
            }
            myOleDbDataReader3.Close();
            myConnection.Close();
        }
    }
}

