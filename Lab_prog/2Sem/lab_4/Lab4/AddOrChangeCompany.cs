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
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].HeaderText = "Код района";
            dataGridView1.Columns[1].Width = 50;
            dataGridView1.Columns[2].HeaderText = "Телефон отдела образования";
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Width = 250;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Место]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            int i = 0;
            dataGridView1.RowCount = 0;
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
        string whatcompany = "";
        private string predname = "";
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dataGridView1.RowCount != 1 && e.RowIndex != dataGridView1.RowCount - 1)
                {
                    whatcompany = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
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
                    
                    if (er != 1)
                    {
                        myOleDbCommand.CommandText = "INSERT INTO [Место] ([Район], [Код района], [Телефон отдела образования]) VALUES ('" + name + "', " + code + ", '" + phone + "')";
                        myOleDbCommand.ExecuteNonQuery();

                        CodeTextBox.Text = "";
                        NameTextBox.Text = "";
                        PhoneTextBox.Text = "";

                        myOleDbCommand.CommandText = "SELECT * FROM [Место]";
                        OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                        int i = 0;
                        dataGridView1.RowCount = 0;
                        while (myOleDbDataReader.Read())
                        {
                            dataGridView1.RowCount += 1;
                            dataGridView1.Rows[i].Cells[0].Value = myOleDbDataReader["Район"];
                            dataGridView1.Rows[i].Cells[1].Value = Convert.ToInt32(myOleDbDataReader["Код района"]);
                            dataGridView1.Rows[i].Cells[2].Value = myOleDbDataReader["Телефон отдела образования"];
                            i++;
                        }
                        myOleDbDataReader.Close();
                    }
                }

                if (AddOrChangeButton.Text == "Изменить")
                {
                    //myOleDbCommand.CommandText = "DELETE FROM [Место] WHERE [Район] = " + code;
                    //myOleDbCommand.ExecuteNonQuery();

                    myOleDbCommand.CommandText = "UPDATE  [Место] SET ([Район] = '" + code + "', [Код района] = '" + name + "', [Телефон отдела образования] = '"+ phone + "'";
                    //myOleDbCommand.CommandText = "UPDATE [Место]([Район], [Код района], [Телефон отдела образования]) VALUES ('" + code.ToString() +"', '" + name.ToString()+ "', '"+phone.ToString()+"')";
                    myOleDbCommand.ExecuteNonQuery();

                    AddOrChangeButton.Text = "Добавить";

                    CodeTextBox.Text = "";
                    NameTextBox.Text = "";
                    PhoneTextBox.Text = "";

                    myOleDbCommand.CommandText = "SELECT * FROM [Место]";
                    OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
                    int i = 0;
                    dataGridView1.RowCount = 0;
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
                myConnection.Close();
            }
            DeleteButton.Enabled = false;
        }

        private void CodeTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            DialogResult ret = MessageBox.Show("Если удалить компанию, то все её изделия тоже удалятся.\nВы уверены?", "Внимание!", MessageBoxButtons.YesNo);

            if (ret == DialogResult.Yes)
            {
                OleDbCommand myOleDbCommand = myConnection.CreateCommand();
                myConnection.Open();
                myOleDbCommand.CommandText = "DELETE FROM [Место] WHERE [Район] = '" + whatcompany.ToString()+"'";
                myOleDbCommand.ExecuteNonQuery();
                //myOleDbCommand.CommandText = "DELETE FROM [Ведомость] WHERE [Код района] = " + whatcompany.ToString();
                //myOleDbCommand.ExecuteNonQuery();

                myOleDbCommand.CommandText = "SELECT * FROM [Место]";
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
