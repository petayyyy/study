﻿using System;
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
        private int max;
        private bool er1 = false;
        private bool er2 = false;
        public ProductData()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);
        }

        private void ProductData_Load(object sender, EventArgs e)
        {

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
                    if(min < 0)
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

        private void MaxTextBox_TextChanged(object sender, EventArgs e)
        {
            if (MaxTextBox.Text != "")
            {
                bool res = int.TryParse(MaxTextBox.Text, out max);
                if (res != true)
                {
                    MessageBox.Show("Введено не число или не целое");
                    MaxTextBox.Text = MaxTextBox.Text.Substring(0, MaxTextBox.Text.Length - 1);
                    FoundButton.Enabled = false;
                    er2 = false;
                }
                else
                {
                    er2 = true;
                    if (er1 != false)
                        FoundButton.Enabled = true;
                }
            }
        }

        private void FoundButton_Click(object sender, EventArgs e)
        {
            if(max < min)
            {
                MessageBox.Show("Максимальное не может быть меньше минимального");
                FoundButton.Enabled = false;
            }
            else
            {
                myConnection.Open();

                dataGridView1.ColumnCount = 7;
                for (int k = 0; k < dataGridView1.ColumnCount; k++)
                    dataGridView1.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView1.RowHeadersVisible = false;

                dataGridView1.Columns[0].HeaderText = "Код предприятия";
                dataGridView1.Columns[0].Width = 40;
                dataGridView1.Columns[1].HeaderText = "Шифр изделия";
                dataGridView1.Columns[1].Width = 100;
                dataGridView1.Columns[2].HeaderText = "Выпуск 1 квартала";
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].HeaderText = "Выпуск 2 квартала";
                dataGridView1.Columns[3].Width = 100;
                dataGridView1.Columns[4].HeaderText = "Выпуск 3 квартала";
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].HeaderText = "Выпуск 4 квартала";
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].HeaderText = "Средняя цена единицы за год";
                dataGridView1.Columns[6].Width = 100;
                dataGridView1.Width = 660;

                OleDbCommand myOleDbCommand = myConnection.CreateCommand();
                myOleDbCommand.CommandText = "SELECT * FROM [Изделия] WHERE ([Выпуск 1 квартала] <= [Выпуск 2 квартала] AND [Выпуск 2 квартала] <= [Выпуск 3 квартала] AND [Выпуск 3 квартала] <= [Выпуск 4 квартала] AND ([Выпуск 4 квартала] * [Средняя цена единицы за год]) >=" + min.ToString() + " AND ([Выпуск 4 квартала] * [Средняя цена единицы за год]) <=" + max.ToString() + ")";
                OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
                int i = 0;
                dataGridView1.RowCount = 1;
                while (myOleDbDataReader1.Read())
                {
                    dataGridView1.RowCount += 1;
                    dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader1["Код предприятия"];
                    dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader1["Шифр изделия"].ToString();
                    dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader1["Выпуск 1 квартала"];
                    dataGridView1.Rows[i].Cells[3].Value = myOleDbDataReader1["Выпуск 2 квартала"];
                    dataGridView1.Rows[i].Cells[4].Value = myOleDbDataReader1["Выпуск 3 квартала"];
                    dataGridView1.Rows[i].Cells[5].Value = myOleDbDataReader1["Выпуск 4 квартала"];
                    dataGridView1.Rows[i].Cells[6].Value = myOleDbDataReader1["Средняя цена единицы за год"];
                    i++;
                }
                myOleDbDataReader1.Close();

                myConnection.Close();
            }
        }
    }
}