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
            myOleDbCommand.CommandText = "SELECT [Район] FROM [Предприятия] WHERE [Код района] = '" + com + "'";
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

            dataGridView1.Columns[0].HeaderText = "Номер школы";
            dataGridView1.Columns[0].Width = 300;
            dataGridView1.Width = 400;

            myOleDbCommand.CommandText = "SELECT [Номер школы] FROM [Изделия] WHERE [Район] = " + idcom.ToString();
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
            myOleDbCommand.CommandText = "SELECT * FROM [Предприятия] WHERE [Код района]";
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

                    int[] mys = new int[4];

                    OleDbCommand myOleDbCommand = myConnection.CreateCommand();
                    myOleDbCommand.CommandText = "SELECT * FROM [Изделия] WHERE [Номер школы] = '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
                    OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader.Read())
                    {
                        mys[0] = Convert.ToInt32(myOleDbDataReader["Телефон"]);
                        mys[1] = Convert.ToInt32(myOleDbDataReader["Год открытия"]);
                        mys[2] = Convert.ToInt32(myOleDbDataReader["Количество учителей"]);
                        mys[3] = Convert.ToInt32(myOleDbDataReader["Количество учеников"]);
                    }
                    myOleDbDataReader.Close();

                    myConnection.Close();

                    AnnualCostTextBox.Text = ((mys[0] + mys[1] + mys[2] + mys[3]) * mys[4]).ToString();
                }
            }
        }
    }
}
