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
    public partial class AddOrChangeCompany : Form
    {
        public static string connectString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=DataBaseLab4.mdb;";
        private OleDbConnection myConnection;
        public AddOrChangeCompany()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(connectString);
        }

        private void AddOrChangeCompany_Load(object sender, EventArgs e)
        {
            myConnection.Open();

            int CountOfCompany = 0;

            dataGridView1.ColumnCount = 3;
            for (int k = 0; k < dataGridView1.ColumnCount; k++)
                dataGridView1.Columns[k].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.RowHeadersVisible = false;

            dataGridView1.Columns[0].HeaderText = "Район";
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].HeaderText = "Код района";
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].HeaderText = "Телефон отдела образования";
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Width = 440;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Предприятия]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            int i = 0;
            dataGridView1.RowCount = 1;
            while (myOleDbDataReader.Read())
            {
                CountOfCompany++;
                dataGridView1.RowCount += 1;
                dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader["Район"];
                dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader["Код района"];
                dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон отдела образования"];
                i++;
            }
            myOleDbDataReader.Close();

            myConnection.Close();
        }
        private int whatcompany = -1;
        private string predname = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.RowCount != 1 && e.RowIndex != dataGridView1.RowCount - 1)
                {
                    whatcompany = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                    predname = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    AddOrChangeButton.Text = "Изменить";
                    DeleteButton.Enabled = true;
                    CodeTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    NameTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    PhoneTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                }
            }
        }

        private void AddOrChangeButton_Click(object sender, EventArgs e)
        {
            if (PhoneTextBox.Text.Length != 15)
                MessageBox.Show("Вы не до конца ввели номер Телефон отдела образованияа");
            else
            if (CodeTextBox.Text.Trim() == "" || NameTextBox.Text.Trim() == "" || PhoneTextBox.Text.Trim() == "")
            {
                MessageBox.Show("Одно или несколько полей пустые");
            }
            else
            {
                string code = CodeTextBox.Text;
                string name = NameTextBox.Text;
                string phone = PhoneTextBox.Text;

                myConnection.Open();

                OleDbCommand myOleDbCommand = myConnection.CreateCommand();

                if (AddOrChangeButton.Text == "Добавить")
                {
                    int er = 0;
                    myOleDbCommand.CommandText = "SELECT [Район] FROM [Предприятия] WHERE [Район] = " + code;
                    OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader1.Read())
                    {
                        MessageBox.Show("Код '" + myOleDbDataReader1[0].ToString() + "' уже существует");
                        er = 1;
                    }
                    myOleDbDataReader1.Close();

                    myOleDbCommand.CommandText = "SELECT [Код района] FROM [Предприятия] WHERE [Код района] = '" + name + "'";
                    OleDbDataReader myOleDbDataReader2 = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader2.Read())
                    {
                        MessageBox.Show("Предприятие '" + myOleDbDataReader2[0].ToString() + "' уже существует");
                        er = 1;
                    }
                    myOleDbDataReader2.Close();

                    if (er != 1)
                    {
                        myOleDbCommand.CommandText = "INSERT INTO [Предприятия] ([Район], [Код района], [Телефон отдела образования]) VALUES (" + code + ", '" + name + "', '" + phone + "')";
                        myOleDbCommand.ExecuteNonQuery();

                        CodeTextBox.Text = "";
                        NameTextBox.Text = "";
                        PhoneTextBox.Text = "";

                        myOleDbCommand.CommandText = "SELECT * FROM [Предприятия]";
                        OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                        int i = 0;
                        dataGridView1.RowCount = 1;
                        while (myOleDbDataReader.Read())
                        {
                            dataGridView1.RowCount += 1;
                            dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader["Район"];
                            dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader["Код района"];
                            dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон отдела образования"];
                            i++;
                        }
                        myOleDbDataReader.Close();
                    }
                }

                if (AddOrChangeButton.Text == "Изменить")
                {
                    int er = 0;
                    myOleDbCommand.CommandText = "SELECT [Район] FROM [Предприятия] WHERE [Район] = " + code;
                    OleDbDataReader myOleDbDataReader1 = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader1.Read())
                    {
                        if (myOleDbDataReader1[0].ToString() != whatcompany.ToString())
                        {
                            MessageBox.Show("Код '" + myOleDbDataReader1[0].ToString() + "' уже существует");
                            er = 1;
                        }
                    }
                    myOleDbDataReader1.Close();

                    myOleDbCommand.CommandText = "SELECT [Код района] FROM [Предприятия] WHERE [Код района] = '" + name + "'";
                    OleDbDataReader myOleDbDataReader2 = myOleDbCommand.ExecuteReader();
                    while (myOleDbDataReader2.Read())
                    {
                        if (predname != myOleDbDataReader2[0].ToString())
                        {
                            MessageBox.Show("Предприятие '" + myOleDbDataReader2[0].ToString() + "' уже существует");
                            er = 1;
                        }
                    }
                    myOleDbDataReader2.Close();

                    if (er != 1)
                    {
                        myOleDbCommand.CommandText = "DELETE FROM [Предприятия] WHERE [Район] = " + code;
                        myOleDbCommand.ExecuteNonQuery();

                        myOleDbCommand.CommandText = "INSERT INTO [Предприятия] ([Район], [Код района], [Телефон отдела образования]) VALUES (" + code + ", '" + name + "', '" + phone + "')";
                        myOleDbCommand.ExecuteNonQuery();

                        AddOrChangeButton.Text = "Добавить";

                        CodeTextBox.Text = "";
                        NameTextBox.Text = "";
                        PhoneTextBox.Text = "";

                        myOleDbCommand.CommandText = "SELECT * FROM [Предприятия]";
                        OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                        int i = 0;
                        dataGridView1.RowCount = 1;
                        while (myOleDbDataReader.Read())
                        {
                            dataGridView1.RowCount += 1;
                            dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader["Район"];
                            dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader["Код района"];
                            dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон отдела образования"];
                            i++;
                        }
                        myOleDbDataReader.Close();
                    }
                }
                myConnection.Close();
            }
            DeleteButton.Enabled = false;
        }

        private void CodeTextBox_TextChanged(object sender, EventArgs e)
        {
            if (CodeTextBox.Text != "")
            {
                bool res = int.TryParse(CodeTextBox.Text, out int x);
                if (res != true)
                {
                    MessageBox.Show("Введено не число или не целое");
                    CodeTextBox.Text = CodeTextBox.Text.Substring(0, CodeTextBox.Text.Length - 1);
                }
                else
                {
                    if (x < 0)
                    {
                        CodeTextBox.Text = "";
                        MessageBox.Show("Введено отрицательное число");
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Если удалить компанию, то все её изделия тоже удалятся.\nВы уверены?", "Внимание!", MessageBoxButtons.YesNo);

            if (ret == DialogResult.Yes)
            {
                OleDbCommand myOleDbCommand = myConnection.CreateCommand();
                myConnection.Open();
                myOleDbCommand.CommandText = "DELETE FROM [Предприятия] WHERE [Район] = " + whatcompany.ToString();
                myOleDbCommand.ExecuteNonQuery();
                myOleDbCommand.CommandText = "DELETE FROM [Изделия] WHERE [Код района] = " + whatcompany.ToString();
                myOleDbCommand.ExecuteNonQuery();

                myOleDbCommand.CommandText = "SELECT * FROM [Предприятия]";
                OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                int i = 0;
                dataGridView1.RowCount = 1;
                while (myOleDbDataReader.Read())
                {
                    dataGridView1.RowCount += 1;
                    dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader["Район"];
                    dataGridView1.Rows[i].Cells[1].Value = myOleDbDataReader["Код района"];
                    dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон отдела образования"];
                    i++;
                }
                myOleDbDataReader.Close();
                myConnection.Close();
            }

            AddOrChangeButton.Text = "Добавить";
            CodeTextBox.Text = "";
            NameTextBox.Text = "";
            PhoneTextBox.Text = "";
            DeleteButton.Enabled = false;
        }
    }
}
