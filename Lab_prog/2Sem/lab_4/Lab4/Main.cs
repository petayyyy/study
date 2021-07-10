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
    public partial class Main : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBaseLab4.mdb;";
        private OleDbConnection myConnection;
        public Main()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myConnection.Open();

            int CountOfCompany = 0;

            BusinessesDGV.ColumnCount = 3;
            for (int k = 0; k < BusinessesDGV.ColumnCount; k++)
                BusinessesDGV.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            BusinessesDGV.RowHeadersVisible = false;

            BusinessesDGV.Columns[0].HeaderText = "Район";
            BusinessesDGV.Columns[0].Width = 40;
            BusinessesDGV.Columns[1].HeaderText = "Код района";
            BusinessesDGV.Columns[1].Width = 200;
            BusinessesDGV.Columns[2].HeaderText = "Телефон отдела образования";
            BusinessesDGV.Columns[2].Width = 200;
            BusinessesDGV.Width = 440;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Предприятия]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            int i = 0;
            BusinessesDGV.RowCount = 1;
            while (myOleDbDataReader.Read())
            {
                CountOfCompany++;
                BusinessesDGV.RowCount += 1;
                BusinessesDGV.Rows[i].Cells[0].Value = myOleDbDataReader["Район"];
                BusinessesDGV.Rows[i].Cells[1].Value = myOleDbDataReader["Код района"];
                BusinessesDGV.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон отдела образования"];
                i++;
            }
            myOleDbDataReader.Close();


            ProductsDGV.ColumnCount = 7;
            for (int k = 0; k < ProductsDGV.ColumnCount; k++)
                ProductsDGV.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            ProductsDGV.RowHeadersVisible = false;

            ProductsDGV.Columns[0].HeaderText = "Код района";
            ProductsDGV.Columns[0].Width = 40;
            ProductsDGV.Columns[1].HeaderText = "Номер школы";
            ProductsDGV.Columns[1].Width = 100;
            ProductsDGV.Columns[2].HeaderText = "Телефон";
            ProductsDGV.Columns[2].Width = 100;
            ProductsDGV.Columns[3].HeaderText = "Год открытия";
            ProductsDGV.Columns[3].Width = 100;
            ProductsDGV.Columns[4].HeaderText = "Количество учителей";
            ProductsDGV.Columns[4].Width = 100;
            ProductsDGV.Columns[5].HeaderText = "Количество учеников";
            ProductsDGV.Columns[5].Width = 100;
            ProductsDGV.Width = 660;

            myOleDbCommand.CommandText = "SELECT * FROM [Изделия]";
            OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
            i = 0;
            ProductsDGV.RowCount = 1;
            while (myOleDbDataReader1.Read())
            {
                ProductsDGV.RowCount += 1;
                ProductsDGV.Rows[i].Cells[0].Value = myOleDbDataReader1["Код района"];
                ProductsDGV.Rows[i].Cells[1].Value = myOleDbDataReader1["Номер школы"];
                ProductsDGV.Rows[i].Cells[2].Value = myOleDbDataReader1["Телефон"];
                ProductsDGV.Rows[i].Cells[3].Value = myOleDbDataReader1["Год открытия"];
                ProductsDGV.Rows[i].Cells[4].Value = myOleDbDataReader1["Количество учителей"];
                ProductsDGV.Rows[i].Cells[5].Value = myOleDbDataReader1["Количество учеников"];
                i++;
            }
            myOleDbDataReader1.Close();


            TotalOtputDGV.ColumnCount = 2;
            for (int k = 0; k < TotalOtputDGV.ColumnCount; k++)
                TotalOtputDGV.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            TotalOtputDGV.RowHeadersVisible = false;

            TotalOtputDGV.Columns[0].HeaderText = "Район";
            TotalOtputDGV.Columns[0].Width = 100;
            TotalOtputDGV.Columns[1].HeaderText = "Стоимость";
            TotalOtputDGV.Columns[1].Width = 345;
            TotalOtputDGV.RowCount = CountOfCompany;

            myOleDbCommand.CommandText = "SELECT [Район] FROM [Предприятия]";
            OleDbDataReader myOleDbDataReader5 = myOleDbCommand.ExecuteReader();
            long[,] ArForOut = new long[CountOfCompany, 2];
            i = 0;
            while (myOleDbDataReader5.Read())
            {
                ArForOut[i, 0] = Convert.ToInt32(myOleDbDataReader5[0]);
                ArForOut[i, 1] = 0;
                i++;
            }
            myOleDbDataReader5.Close();

            myOleDbCommand.CommandText = "SELECT * FROM [Изделия]";
            OleDbDataReader myOleDbDataReader2 = myOleDbCommand.ExecuteReader();
            TotalOtputDGV.RowCount = CountOfCompany;

            while (myOleDbDataReader2.Read())
            {
                for (int h = 0; h <= CountOfCompany - 1; h++)
                    if (ArForOut[h, 0] == Convert.ToInt32(myOleDbDataReader2["Код района"]))
                    {
                        ArForOut[h, 0] = Convert.ToInt32(myOleDbDataReader2["Код района"]);
                        ArForOut[h, 1] += Convert.ToInt64(myOleDbDataReader2["Телефон"]);
                        ArForOut[h, 1] += Convert.ToInt64(myOleDbDataReader2["Год открытия"]);
                        ArForOut[h, 1] += Convert.ToInt64(myOleDbDataReader2["Количество учителей"]);
                        ArForOut[h, 1] += Convert.ToInt64(myOleDbDataReader2["Количество учеников"]);
                        break;
                    }
            }
            myOleDbDataReader2.Close();

            for (int h = 0; h <= CountOfCompany - 1; h++)
            {
                TotalOtputDGV.Rows[h].Cells[0].Value = ArForOut[h, 0];
                TotalOtputDGV.Rows[h].Cells[1].Value = ArForOut[h, 1];
            }

            myConnection.Close();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            myConnection.Open();

            int CountOfCompany = 0;

            BusinessesDGV.ColumnCount = 3;
            for (int k = 0; k < BusinessesDGV.ColumnCount; k++)
                BusinessesDGV.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            BusinessesDGV.RowHeadersVisible = false;

            BusinessesDGV.Columns[0].HeaderText = "Район";
            BusinessesDGV.Columns[0].Width = 40;
            BusinessesDGV.Columns[1].HeaderText = "Код района";
            BusinessesDGV.Columns[1].Width = 200;
            BusinessesDGV.Columns[2].HeaderText = "Телефон отдела образования";
            BusinessesDGV.Columns[2].Width = 200;
            BusinessesDGV.Width = 440;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Предприятия]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            int i = 0;
            BusinessesDGV.RowCount = 1;
            while (myOleDbDataReader.Read())
            {
                CountOfCompany++;
                BusinessesDGV.RowCount += 1;
                BusinessesDGV.Rows[i].Cells[0].Value = myOleDbDataReader["Район"];
                BusinessesDGV.Rows[i].Cells[1].Value = myOleDbDataReader["Код района"];
                BusinessesDGV.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон отдела образования"];
                i++;
            }
            myOleDbDataReader.Close();

            ProductsDGV.ColumnCount = 7;
            for (int k = 0; k < BusinessesDGV.ColumnCount; k++)
                ProductsDGV.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            ProductsDGV.RowHeadersVisible = false;

            ProductsDGV.Columns[0].HeaderText = "Код района";
            ProductsDGV.Columns[0].Width = 40;
            ProductsDGV.Columns[1].HeaderText = "Номер школы";
            ProductsDGV.Columns[1].Width = 100;
            ProductsDGV.Columns[2].HeaderText = "Телефон";
            ProductsDGV.Columns[2].Width = 100;
            ProductsDGV.Columns[3].HeaderText = "Год открытия";
            ProductsDGV.Columns[3].Width = 100;
            ProductsDGV.Columns[4].HeaderText = "Количество учителей";
            ProductsDGV.Columns[4].Width = 100;
            ProductsDGV.Columns[5].HeaderText = "Количество учеников";
            ProductsDGV.Columns[5].Width = 100;
            ProductsDGV.Width = 660;

            myOleDbCommand.CommandText = "SELECT * FROM [Изделия]";
            OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
            i = 0;
            ProductsDGV.RowCount = 1;
            while (myOleDbDataReader1.Read())
            {
                ProductsDGV.RowCount += 1;
                ProductsDGV.Rows[i].Cells[0].Value = myOleDbDataReader1["Код района"];
                ProductsDGV.Rows[i].Cells[1].Value = myOleDbDataReader1["Номер школы"];
                ProductsDGV.Rows[i].Cells[2].Value = myOleDbDataReader1["Телефон"];
                ProductsDGV.Rows[i].Cells[3].Value = myOleDbDataReader1["Год открытия"];
                ProductsDGV.Rows[i].Cells[4].Value = myOleDbDataReader1["Количество учителей"];
                ProductsDGV.Rows[i].Cells[5].Value = myOleDbDataReader1["Количество учеников"];
                i++;
            }
            myOleDbDataReader1.Close();


            TotalOtputDGV.ColumnCount = 2;
            for (int k = 0; k < TotalOtputDGV.ColumnCount; k++)
                TotalOtputDGV.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            TotalOtputDGV.RowHeadersVisible = false;

            TotalOtputDGV.Columns[0].HeaderText = "Район";
            TotalOtputDGV.Columns[0].Width = 100;
            TotalOtputDGV.Columns[1].HeaderText = "Стоимость";
            TotalOtputDGV.Columns[1].Width = 345;

            myOleDbCommand.CommandText = "SELECT [Район] FROM [Предприятия]";
            OleDbDataReader myOleDbDataReader5 = myOleDbCommand.ExecuteReader();
            long[,] ArForOut = new long[CountOfCompany, 2];
            i = 0;
            while (myOleDbDataReader5.Read())
            {
                ArForOut[i, 0] = Convert.ToInt32(myOleDbDataReader5[0]);
                ArForOut[i, 1] = 0;
                i++;
            }
            myOleDbDataReader5.Close();

            myOleDbCommand.CommandText = "SELECT * FROM [Изделия]";
            OleDbDataReader myOleDbDataReader2 = myOleDbCommand.ExecuteReader();
            TotalOtputDGV.RowCount = CountOfCompany;

            while (myOleDbDataReader2.Read())
            {
                for (int h = 0; h <= CountOfCompany - 1; h++)
                    if (ArForOut[h, 0] == Convert.ToInt32(myOleDbDataReader2["Код района"]))
                    {
                        ArForOut[h, 0] = Convert.ToInt32(myOleDbDataReader2["Код района"]);
                        ArForOut[h, 1] += Convert.ToInt64(myOleDbDataReader2["Телефон"]);
                        ArForOut[h, 1] += Convert.ToInt64(myOleDbDataReader2["Год открытия"]);
                        ArForOut[h, 1] += Convert.ToInt64(myOleDbDataReader2["Количество учителей"]);
                        ArForOut[h, 1] += Convert.ToInt64(myOleDbDataReader2["Количество учеников"]);
                        break;
                    }
            }
            myOleDbDataReader2.Close();

            for (int h = 0; h <= CountOfCompany - 1; h++)
            {
                TotalOtputDGV.Rows[h].Cells[0].Value = ArForOut[h, 0];
                TotalOtputDGV.Rows[h].Cells[1].Value = ArForOut[h, 1];
            }

            myConnection.Close();
        }

        private void ReplacingSlateButton_Click(object sender, EventArgs e)
        {
            ChangeCipher f = new ChangeCipher();
            f.ShowDialog();
        }

        private void AnnualCostButton_Click(object sender, EventArgs e)
        {
            AnnualCost a = new AnnualCost();
            a.ShowDialog();
        }

        private void ProductDataButton_Click(object sender, EventArgs e)
        {
            ProductData U = new ProductData();
            U.ShowDialog();
        }

        private void AddOrChangeButton_Click(object sender, EventArgs e)
        {
            AddOrChangeForm IKF = new AddOrChangeForm();
            IKF.ShowDialog();
        }
    }
}
