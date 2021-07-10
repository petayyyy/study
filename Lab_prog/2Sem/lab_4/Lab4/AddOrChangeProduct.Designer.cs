namespace Lab4
{
    partial class AddOrChangeProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddOrChangeButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ShifrTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CompanyComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Vip1textBox = new System.Windows.Forms.TextBox();
            this.Vip2textBox = new System.Windows.Forms.TextBox();
            this.Vip4textBox = new System.Windows.Forms.TextBox();
            this.Vip3textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.CostTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteButton.Location = new System.Drawing.Point(12, 468);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(660, 63);
            this.DeleteButton.TabIndex = 20;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddOrChangeButton
            // 
            this.AddOrChangeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddOrChangeButton.Location = new System.Drawing.Point(12, 399);
            this.AddOrChangeButton.Name = "AddOrChangeButton";
            this.AddOrChangeButton.Size = new System.Drawing.Size(660, 63);
            this.AddOrChangeButton.TabIndex = 19;
            this.AddOrChangeButton.Text = "Добавить";
            this.AddOrChangeButton.UseVisualStyleBackColor = true;
            this.AddOrChangeButton.Click += new System.EventHandler(this.AddOrChangeButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(9, 212);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(273, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Чтобы изменить запись, нажмите на неё";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Выпуск 1 квартала";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(337, 236);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Шифр изделия";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 236);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Код предприятия";
            // 
            // ShifrTextBox
            // 
            this.ShifrTextBox.Location = new System.Drawing.Point(340, 252);
            this.ShifrTextBox.Name = "ShifrTextBox";
            this.ShifrTextBox.Size = new System.Drawing.Size(332, 20);
            this.ShifrTextBox.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(660, 197);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // CompanyComboBox
            // 
            this.CompanyComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CompanyComboBox.FormattingEnabled = true;
            this.CompanyComboBox.Location = new System.Drawing.Point(12, 252);
            this.CompanyComboBox.Name = "CompanyComboBox";
            this.CompanyComboBox.Size = new System.Drawing.Size(322, 24);
            this.CompanyComboBox.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 279);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Выпуск 2 квартала";
            // 
            // Vip1textBox
            // 
            this.Vip1textBox.Location = new System.Drawing.Point(12, 295);
            this.Vip1textBox.Name = "Vip1textBox";
            this.Vip1textBox.Size = new System.Drawing.Size(322, 20);
            this.Vip1textBox.TabIndex = 23;
            this.Vip1textBox.TextChanged += new System.EventHandler(this.Vip1textBox_TextChanged);
            // 
            // Vip2textBox
            // 
            this.Vip2textBox.Location = new System.Drawing.Point(340, 295);
            this.Vip2textBox.Name = "Vip2textBox";
            this.Vip2textBox.Size = new System.Drawing.Size(332, 20);
            this.Vip2textBox.TabIndex = 24;
            this.Vip2textBox.TextChanged += new System.EventHandler(this.Vip2textBox_TextChanged);
            // 
            // Vip4textBox
            // 
            this.Vip4textBox.Location = new System.Drawing.Point(340, 334);
            this.Vip4textBox.Name = "Vip4textBox";
            this.Vip4textBox.Size = new System.Drawing.Size(332, 20);
            this.Vip4textBox.TabIndex = 28;
            this.Vip4textBox.TextChanged += new System.EventHandler(this.Vip4textBox_TextChanged);
            // 
            // Vip3textBox
            // 
            this.Vip3textBox.Location = new System.Drawing.Point(12, 334);
            this.Vip3textBox.Name = "Vip3textBox";
            this.Vip3textBox.Size = new System.Drawing.Size(322, 20);
            this.Vip3textBox.TabIndex = 27;
            this.Vip3textBox.TextChanged += new System.EventHandler(this.Vip3textBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(337, 318);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 26;
            this.label6.Text = "Выпуск 4 квартала";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 318);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Выпуск 3 квартала";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 357);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(159, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Средняя цена единицы за год";
            // 
            // CostTextBox
            // 
            this.CostTextBox.Location = new System.Drawing.Point(12, 373);
            this.CostTextBox.Name = "CostTextBox";
            this.CostTextBox.Size = new System.Drawing.Size(660, 20);
            this.CostTextBox.TabIndex = 30;
            this.CostTextBox.TextChanged += new System.EventHandler(this.CostTextBox_TextChanged);
            // 
            // AddOrChangeProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 538);
            this.Controls.Add(this.CostTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Vip4textBox);
            this.Controls.Add(this.Vip3textBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Vip2textBox);
            this.Controls.Add(this.Vip1textBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CompanyComboBox);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddOrChangeButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShifrTextBox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "AddOrChangeProduct";
            this.Text = "Добавить или изменить изделия";
            this.Load += new System.EventHandler(this.AddOrChangeProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddOrChangeButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ShifrTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox CompanyComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Vip1textBox;
        private System.Windows.Forms.TextBox Vip2textBox;
        private System.Windows.Forms.TextBox Vip4textBox;
        private System.Windows.Forms.TextBox Vip3textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox CostTextBox;
    }
}