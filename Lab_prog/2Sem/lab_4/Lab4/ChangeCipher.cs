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
    public partial class ChangeCipher : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBaseLab4.mdb;";
        private OleDbConnection myConnection;
        public ChangeCipher()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);
        }
        private int idcom = -1;
        private void CompanyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com = CompanyComboBox.SelectedItem.ToString();
            idcom = -1;

            myConnection.Open();

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT [Код предприятия] FROM [Предприятия] WHERE [Название] = '" + com + "'";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            while (myOleDbDataReader.Read())
            {
               idcom = Convert.ToInt32(myOleDbDataReader[0]);
            }
            myOleDbDataReader.Close();

            dataGridView1.ColumnCount = 1;
            for (int k = 0; k < dataGridView1.ColumnCount; k++)
                dataGridView1.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns[0].HeaderText = "Шифр изделия";
            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Width = 400;

            myOleDbCommand.CommandText = "SELECT [Шифр изделия] FROM [Изделия] WHERE [Код предприятия] = " + idcom.ToString();
            OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
            dataGridView1.RowCount = 1;
            int i = 0;
            while (myOleDbDataReader1.Read())
            {
                dataGridView1.RowCount += 1;
                dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader1[0];
                i++;
            }
            myOleDbDataReader1.Close();

            myConnection.Close();
        }

        private void ChangeCipher_Load(object sender, EventArgs e)
        {
            myConnection.Open();

            CompanyComboBox.Items.Clear();
            CompanyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Предприятия] WHERE [Название]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            while (myOleDbDataReader.Read())
            {
                CompanyComboBox.Items.Add(myOleDbDataReader[1]);
            }
            myOleDbDataReader.Close();

            myConnection.Close();
        }
        private string pred = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.RowCount != 1 && e.RowIndex != dataGridView1.RowCount - 1)
                {
                    NewNameTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    pred = NewNameTextBox.Text;
                    NewNameTextBox.Enabled = true;
                    ChangeButton.Enabled = true;
                    label3.Visible = true;
                }
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            string newname = NewNameTextBox.Text;

            if (pred == newname || newname.Trim() == "")
            {
                if (pred == newname)
                    MessageBox.Show("Вы не ввели новый шифр");
                else
                    MessageBox.Show("Вы ничего не ввели или одни пробелы");
            }
            else
            {
                int er = 0;
                NewNameTextBox.Enabled = false;
                NewNameTextBox.Text = "";
                ChangeButton.Enabled = false;
                label3.Visible = false;

                myConnection.Open();

                OleDbCommand myOleDbCommand = myConnection.CreateCommand();

                myOleDbCommand.CommandText = "SELECT [Шифр изделия] FROM [Изделия] WHERE [Шифр изделия] = '" + newname + "'";
                OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
                while (myOleDbDataReader1.Read())
                {
                    MessageBox.Show("'" + myOleDbDataReader1[0].ToString() + "' уже существует");
                    er = 1;
                }
                myOleDbDataReader1.Close();

                if (er != 1)
                {
                    myOleDbCommand.CommandText = "UPDATE [Изделия] SET [Шифр изделия] = '" + newname + "' WHERE [Шифр изделия] = '" + pred + "'";
                    myOleDbCommand.ExecuteNonQuery();

                    myOleDbCommand.CommandText = "SELECT [Шифр изделия] FROM [Изделия] WHERE [Код предприятия] = " + idcom.ToString();
                    OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    dataGridView1.RowCount = 1;
                    int i = 0;
                    while (myOleDbDataReader.Read())
                    {
                        dataGridView1.RowCount += 1;
                        dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader[0];
                        i++;
                    }
                    myOleDbDataReader.Close();
                }
                myConnection.Close();
            }
        }
    }
}
