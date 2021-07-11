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
            NewNameTextBox.Enabled = false;
            ChangeButton.Enabled = false;
        }
        private int idcom = -1;
        private void CompanyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string com = CompanyComboBox.SelectedItem.ToString();
            idcom = -1;
            myConnection.Open();
            NewNameTextBox.Enabled = true;
            ChangeButton.Enabled = true;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT [Количество учителей] FROM [Ведомость] WHERE [Номер школы] = " + com;
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            while (myOleDbDataReader.Read())
            {
                idcom = Convert.ToInt32(myOleDbDataReader[0]);
            }
            myOleDbDataReader.Close();

            NewNameTextBox.Text = idcom.ToString();
            myConnection.Close();
        }

        private void ChangeCipher_Load(object sender, EventArgs e)
        {
            myConnection.Open();

            CompanyComboBox.Items.Clear();
            CompanyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            OleDbCommand myOleDbCommand = myConnection.CreateCommand();
            myOleDbCommand.CommandText = "SELECT * FROM [Ведомость] WHERE [Номер школы]";
            OleDbDataReader myOleDbDataReader = myOleDbCommand.ExecuteReader();
            while (myOleDbDataReader.Read())
            {
                CompanyComboBox.Items.Add(myOleDbDataReader[1]);
            }
            myOleDbDataReader.Close();

            myConnection.Close();
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            string newname = NewNameTextBox.Text;

            if (NewNameTextBox.Text == "")
            {
                MessageBox.Show("Вы не ввели новое количество учителей");
            }
            else
            {
                myConnection.Open();

                OleDbCommand myOleDbCommand = myConnection.CreateCommand();

                myOleDbCommand.CommandText = "UPDATE [Ведомость] SET [Количество учителей] = '" + NewNameTextBox.Text + "' WHERE [Номер школы] = " + CompanyComboBox.SelectedItem.ToString();
                myOleDbCommand.ExecuteNonQuery();
                myConnection.Close();

                NewNameTextBox.Text = "";
            }
        }

        private void NewNameTextBox_TextChanged_1(object sender, EventArgs e)
        {
            /*
                myConnection.Open();

                OleDbCommand myOleDbCommand = myConnection.CreateCommand();

                myOleDbCommand.CommandText = "UPDATE [Ведомость] SET [Количество учителей] = '" + NewNameTextBox.Text + "' WHERE [Номер школы] = " + CompanyComboBox.SelectedItem.ToString();
                myOleDbCommand.ExecuteNonQuery();
                myConnection.Close();*/
            }
        }
}
