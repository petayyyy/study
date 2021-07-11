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

            myConnection.Open();

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT [Номер школы] FROM [Ведомость]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            int i = 0;
            while (myOleDbDataReader.Read())
            {
                string g = myOleDbDataReader[0].ToString();
                char[] d = g.ToCharArray();
                if (d[0] == '7')
                {
                    i++; }
            }
            myOleDbDataReader.Close();

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

            myOleDbCommand.CommandText = "SELECT * FROM [Ведомость]";
            OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
            i = 0;
            dataGridView1.RowCount = 0;
            while (myOleDbDataReader1.Read())
            {
                string g = myOleDbDataReader1["Номер школы"].ToString();
                char[] d = g.ToCharArray();
                if (d[0] == '7')
                {
                    dataGridView1.RowCount += 1;
                    dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader1["Код района"];
                    dataGridView1.Rows[i].Cells[1].Value = g;
                    dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader1["Телефон"];
                    dataGridView1.Rows[i].Cells[3].Value = myOleDbDataReader1["Год открытия"];
                    dataGridView1.Rows[i].Cells[4].Value = myOleDbDataReader1["Количество учителей"];
                    dataGridView1.Rows[i].Cells[5].Value = myOleDbDataReader1["Количество учеников"];
                    i++;
                }
                
            }
            myOleDbDataReader1.Close();
            myConnection.Close();
        }

        private void AnnualCost_Load(object sender, EventArgs e)
        {
            /*
            myConnection.Open();

            CompanyComboBox.Items.Clear();
            CompanyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Место] WHERE [Код района]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            while (myOleDbDataReader.Read())
            {
                CompanyComboBox.Items.Add(myOleDbDataReader[1]);
            }
            myOleDbDataReader.Close();

            myConnection.Close();
            */
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
                    myOleDbCommand.CommandText = "SELECT * FROM [Ведомость] WHERE [Номер школы] = '" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'";
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

                }
            }
        }
    }
}