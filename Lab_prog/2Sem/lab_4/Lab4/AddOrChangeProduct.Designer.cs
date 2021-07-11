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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ShifrTextBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.Vip1textBox = new System.Windows.Forms.TextBox();
            this.Vip2textBox = new System.Windows.Forms.TextBox();
            this.Vip4textBox = new System.Windows.Forms.TextBox();
            this.Vip3textBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // DeleteButton
            // 
            this.DeleteButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DeleteButton.Location = new System.Drawing.Point(347, 358);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(4);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(186, 73);
            this.DeleteButton.TabIndex = 20;
            this.DeleteButton.Text = "Удалить";
            this.DeleteButton.UseVisualStyleBackColor = false;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddOrChangeButton
            // 
            this.AddOrChangeButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AddOrChangeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddOrChangeButton.Location = new System.Drawing.Point(347, 286);
            this.AddOrChangeButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddOrChangeButton.Name = "AddOrChangeButton";
            this.AddOrChangeButton.Size = new System.Drawing.Size(186, 64);
            this.AddOrChangeButton.TabIndex = 19;
            this.AddOrChangeButton.Text = "Добавить";
            this.AddOrChangeButton.UseVisualStyleBackColor = false;
            this.AddOrChangeButton.Click += new System.EventHandler(this.AddOrChangeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 343);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Номер школы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(661, 388);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Количество учеников";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 290);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Код района";
            // 
            // ShifrTextBox
            // 
            this.ShifrTextBox.Location = new System.Drawing.Point(55, 364);
            this.ShifrTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.ShifrTextBox.Name = "ShifrTextBox";
            this.ShifrTextBox.Size = new System.Drawing.Size(183, 22);
            this.ShifrTextBox.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(16, 15);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(859, 242);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 389);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 22;
            this.label5.Text = "Телефон";
            // 
            // Vip1textBox
            // 
            this.Vip1textBox.Location = new System.Drawing.Point(55, 410);
            this.Vip1textBox.Margin = new System.Windows.Forms.Padding(4);
            this.Vip1textBox.Name = "Vip1textBox";
            this.Vip1textBox.Size = new System.Drawing.Size(185, 22);
            this.Vip1textBox.TabIndex = 23;
            this.Vip1textBox.TextChanged += new System.EventHandler(this.Vip1textBox_TextChanged);
            // 
            // Vip2textBox
            // 
            this.Vip2textBox.Location = new System.Drawing.Point(664, 307);
            this.Vip2textBox.Margin = new System.Windows.Forms.Padding(4);
            this.Vip2textBox.Name = "Vip2textBox";
            this.Vip2textBox.Size = new System.Drawing.Size(183, 22);
            this.Vip2textBox.TabIndex = 24;
            this.Vip2textBox.TextChanged += new System.EventHandler(this.Vip2textBox_TextChanged);
            // 
            // Vip4textBox
            // 
            this.Vip4textBox.Location = new System.Drawing.Point(666, 409);
            this.Vip4textBox.Margin = new System.Windows.Forms.Padding(4);
            this.Vip4textBox.Name = "Vip4textBox";
            this.Vip4textBox.Size = new System.Drawing.Size(183, 22);
            this.Vip4textBox.TabIndex = 28;
            this.Vip4textBox.TextChanged += new System.EventHandler(this.Vip4textBox_TextChanged);
            // 
            // Vip3textBox
            // 
            this.Vip3textBox.Location = new System.Drawing.Point(664, 358);
            this.Vip3textBox.Margin = new System.Windows.Forms.Padding(4);
            this.Vip3textBox.Name = "Vip3textBox";
            this.Vip3textBox.Size = new System.Drawing.Size(185, 22);
            this.Vip3textBox.TabIndex = 27;
            this.Vip3textBox.TextChanged += new System.EventHandler(this.Vip3textBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(661, 333);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(152, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Количество учителей";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(661, 286);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 17);
            this.label7.TabIndex = 25;
            this.label7.Text = "Год открытия";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(54, 318);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(183, 22);
            this.textBox1.TabIndex = 29;
            // 
            // AddOrChangeProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(884, 452);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Vip4textBox);
            this.Controls.Add(this.Vip3textBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Vip2textBox);
            this.Controls.Add(this.Vip1textBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddOrChangeButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ShifrTextBox);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ShifrTextBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Vip1textBox;
        private System.Windows.Forms.TextBox Vip2textBox;
        private System.Windows.Forms.TextBox Vip4textBox;
        private System.Windows.Forms.TextBox Vip3textBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
    }
}