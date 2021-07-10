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
    public partial class AnnualCost : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBaseLab4.mdb;";
        private OleDbConnection myConnection;
        public AnnualCost()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);
        }

        private void CompanyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com = CompanyComboBox.SelectedItem.ToString();
            int idcom = -1;

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

        private void AnnualCost_Load(object sender, EventArgs e)
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.RowCount != 1 && e.RowIndex != dataGridView1.RowCount - 1)
                {
                    myConnection.Open();

                    int[] mys = new int[5];

                    OleDbCommand myOleDbCommand = myConnection.CreateCommand();
                    myOleDbCommand.CommandText = "SELECT * FROM [Изделия] WHERE [Шифр изделия] = '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                    OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        mys[0] = Convert.ToInt32(myOleDbDataReader["Выпуск 1 квартала"]);
                        mys[1] = Convert.ToInt32(myOleDbDataReader["Выпуск 2 квартала"]);
                        mys[2] = Convert.ToInt32(myOleDbDataReader["Выпуск 3 квартала"]);
                        mys[3] = Convert.ToInt32(myOleDbDataReader["Выпуск 4 квартала"]);
                        mys[4] = Convert.ToInt32(myOleDbDataReader["Средняя цена единицы за год"]);
                    }
                    myOleDbDataReader.Close();

                    myConnection.Close();

                    AnnualCostTextBox.Text = ((mys[0] + mys[1] + mys[2] + mys[3]) * mys[4]).ToString();
                }
            }
        }
    }
}
