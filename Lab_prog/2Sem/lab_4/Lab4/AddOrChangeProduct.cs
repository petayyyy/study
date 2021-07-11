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
    public partial class AddOrChangeProduct : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBaseLab4.mdb;";
        private OleDbConnection myConnection;
        public AddOrChangeProduct()
        {
            InitializeComponent();
            myConnection = new OleDbConnection(connectString);
        }
        private void AddOrChangeProduct_Load(object sender, EventArgs e)
        {
            myConnection.Open();
            OleDbCommand myOleDbCommand = myConnection.CreateCommand();

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
            dataGridView1.Width = 560;

            myOleDbCommand.CommandText = "SELECT * FROM [Ведомость]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            int i = 0;
            dataGridView1.RowCount = 1;
            while (myOleDbDataReader.Read())
            {
                dataGridView1.RowCount += 1;
                dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader["Код района"];
                dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader["Номер школы"];
                dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон"];
                dataGridView1.Rows[i].Cells[3].Value = myOleDbDataReader["Год открытия"];
                dataGridView1.Rows[i].Cells[4].Value = myOleDbDataReader["Количество учителей"];
                dataGridView1.Rows[i].Cells[5].Value = myOleDbDataReader["Количество учеников"];
                i++;
            }
            myOleDbDataReader.Close();

            myConnection.Close();
        }
        private string predshif = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.RowCount != 1 && e.RowIndex != dataGridView1.RowCount - 1)
                {
                    predshif = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    ShifrTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    Vip1textBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    Vip2textBox.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    Vip3textBox.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    Vip4textBox.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    AddOrChangeButton.Text = "Изменить";
                    DeleteButton.Enabled = true;
                    predshif = ShifrTextBox.Text;

                }
            }
        }

        private void AddOrChangeButton_Click(object sender, EventArgs e)
        {
            string shifr = ShifrTextBox.Text;
            string num = textBox1.Text;
            string v1 = Vip1textBox.Text;
            string v2 = Vip2textBox.Text;
            string v3 = Vip3textBox.Text;
            string v4 = Vip4textBox.Text;

            if (shifr.Trim() == "" || v1.Trim() == "" || v2.Trim() == "" || v3.Trim() == "" || v4.Trim() == "")
            {
                MessageBox.Show("Одно или несколько полей пустые");
            }
            else
            {
                myConnection.Open();

                OleDbCommand myOleDbCommand = myConnection.CreateCommand();
                if (AddOrChangeButton.Text == "Добавить")
                    {
                        int er = 0;
                        myOleDbCommand.CommandText = "SELECT [Номер школы] FROM [Ведомость] WHERE [Номер школы] = "+ shifr ;
                        OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
                        while (myOleDbDataReader1.Read())
                        {
                            MessageBox.Show("Шифр '" + myOleDbDataReader1[0].ToString() + "' уже существует");
                            er = 1;
                        }
                        myOleDbDataReader1.Close();

                        if (er != 1)
                        {
                            myOleDbCommand.CommandText = "INSERT INTO [Ведомость] ([Код района], [Номер школы], [Телефон], [Год открытия], [Количество учителей], [Количество учеников]) VALUES (" + num + ", " + shifr + ", " + v1 + ", " + v2 + ", " + v3 + ", " + v4 + ")";
                            myOleDbCommand.ExecuteNonQuery();
                            
                            ShifrTextBox.Text = "";
                            textBox1.Text = "";
                            Vip1textBox.Text = "";
                            Vip2textBox.Text = "";
                            Vip3textBox.Text = "";
                            Vip4textBox.Text = "";

                            myOleDbCommand.CommandText = "SELECT * FROM [Ведомость]";
                            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                            int i = 0;
                            dataGridView1.RowCount = 1;
                            while (myOleDbDataReader.Read())
                            {
                                dataGridView1.RowCount += 1;
                                dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader["Код района"];
                                dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader["Номер школы"];
                                dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон"];
                                dataGridView1.Rows[i].Cells[3].Value = myOleDbDataReader["Год открытия"];
                                dataGridView1.Rows[i].Cells[4].Value = myOleDbDataReader["Количество учителей"];
                                dataGridView1.Rows[i].Cells[5].Value = myOleDbDataReader["Количество учеников"];
                                i++;
                            }
                            myOleDbDataReader.Close();
                        }
                    }

                    if (AddOrChangeButton.Text == "Изменить")
                    {
                        int er = 0;
                        myOleDbCommand.CommandText = "SELECT [Номер школы] FROM [Ведомость] WHERE [Номер школы] = " + shifr;
                        OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
                        while (myOleDbDataReader1.Read())
                        {
                            if (myOleDbDataReader1[0].ToString() != predshif)
                            {
                                MessageBox.Show("Школа '" + myOleDbDataReader1[0].ToString() + "' уже существует");
                                er = 1;
                            }
                        }
                        myOleDbDataReader1.Close();

                        if (er != 1)
                        {
                            myOleDbCommand.CommandText = "DELETE FROM [Ведомость] WHERE [Номер школы] = "+predshif;
                            myOleDbCommand.ExecuteNonQuery();

                            myOleDbCommand.CommandText = "INSERT INTO [Ведомость] ([Код района], [Номер школы], [Телефон], [Год открытия], [Количество учителей], [Количество учеников]) VALUES (" + num + ", " + shifr + ", '" + v1 + "', " + v2 + ", " + v3 + ", " + v4 + ")";
                            myOleDbCommand.ExecuteNonQuery();

                            AddOrChangeButton.Text = "Добавить";

                            ShifrTextBox.Text = "";
                            Vip1textBox.Text = "";
                            Vip2textBox.Text = "";
                            textBox1.Text = "";
                            Vip3textBox.Text = "";
                            Vip4textBox.Text = "";

                            myOleDbCommand.CommandText = "SELECT * FROM [Ведомость]";
                            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                            int i = 0;
                            dataGridView1.RowCount = 1;
                            while (myOleDbDataReader.Read())
                            {
                                dataGridView1.RowCount += 1;
                                dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader["Код района"];
                                dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader["Номер школы"];
                                dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон"];
                                dataGridView1.Rows[i].Cells[3].Value = myOleDbDataReader["Год открытия"];
                                dataGridView1.Rows[i].Cells[4].Value = myOleDbDataReader["Количество учителей"];
                                dataGridView1.Rows[i].Cells[5].Value = myOleDbDataReader["Количество учеников"];
                                i++;
                            }
                            myOleDbDataReader.Close();
                    }
                    myConnection.Close();
                }
            }
            DeleteButton.Enabled = false;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Вы уверены, что хотите удалить данные школы номер '" + predshif + "' ?", "Внимание!", MessageBoxButtons.YesNo);

            if (ret == DialogResult.Yes)
            {
                OleDbCommand myOleDbCommand = myConnection.CreateCommand();
                myConnection.Open();
                myOleDbCommand.CommandText = "DELETE FROM [Ведомость] WHERE [Номер школы] = '" + predshif + "'";
                myOleDbCommand.ExecuteNonQuery();

                myOleDbCommand.CommandText = "SELECT * FROM [Ведомость]";
                OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int i = 0;
                dataGridView1.RowCount = 1;
                while (myOleDbDataReader.Read())
                {
                    dataGridView1.RowCount += 1;
                    dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader["Код района"];
                    dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader["Номер школы"];
                    dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон"];
                    dataGridView1.Rows[i].Cells[3].Value = myOleDbDataReader["Год открытия"];
                    dataGridView1.Rows[i].Cells[4].Value = myOleDbDataReader["Количество учителей"];
                    dataGridView1.Rows[i].Cells[5].Value = myOleDbDataReader["Количество учеников"];
                    i++;
                }
                myOleDbDataReader.Close();
                myConnection.Close();
            }

            AddOrChangeButton.Text = "Добавить";
            ShifrTextBox.Text = "";
            Vip1textBox.Text = "";
            Vip2textBox.Text = "";
            Vip3textBox.Text = "";
            textBox1.Text = "";
            Vip4textBox.Text = "";
            DeleteButton.Enabled = false;
        }

        private void Vip1textBox_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void Vip2textBox_TextChanged(object sender, EventArgs e)
        {
            if (Vip2textBox.Text != "")
            {
                bool res = int.TryParse(Vip2textBox.Text, out int x);
                if (res != true)
                {
                    MessageBox.Show("Введено не число или не целое");
                    Vip2textBox.Text = Vip2textBox.Text.Substring(0, Vip2textBox.Text.Length - 1);
                }
                else
                {
                    if (x < 0)
                    {
                        Vip2textBox.Text = "";
                        MessageBox.Show("Введено отрицательное число");
                    }
                }
            }
        }

        private void Vip3textBox_TextChanged(object sender, EventArgs e)
        {
            if (Vip3textBox.Text != "")
            {
                bool res = int.TryParse(Vip3textBox.Text, out int x);
                if (res != true)
                {
                    MessageBox.Show("Введено не число или не целое");
                    Vip3textBox.Text = Vip3textBox.Text.Substring(0, Vip3textBox.Text.Length - 1);
                }
                else
                {
                    if (x < 0)
                    {
                        Vip3textBox.Text = "";
                        MessageBox.Show("Введено отрицательное число");
                    }
                }
            }
        }

        private void Vip4textBox_TextChanged(object sender, EventArgs e)
        {
            if (Vip4textBox.Text != "")
            {
                bool res = int.TryParse(Vip4textBox.Text, out int x);
                if (res != true)
                {
                    MessageBox.Show("Введено не число или не целое");
                    Vip4textBox.Text = Vip4textBox.Text.Substring(0, Vip4textBox.Text.Length - 1);
                }
                else
                {
                    if (x < 0)
                    {
                        Vip4textBox.Text = "";
                        MessageBox.Show("Введено отрицательное число");
                    }
                }
            }
        }

    }
}
