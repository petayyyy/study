namespace Lab4
{
    partial class ChangeCipher
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
            this.CompanyComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NewNameTextBox = new System.Windows.Forms.TextBox();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CompanyComboBox
            // 
            this.CompanyComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CompanyComboBox.FormattingEnabled = true;
            this.CompanyComboBox.Location = new System.Drawing.Point(30, 51);
            this.CompanyComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.CompanyComboBox.Name = "CompanyComboBox";
            this.CompanyComboBox.Size = new System.Drawing.Size(304, 28);
            this.CompanyComboBox.TabIndex = 1;
            this.CompanyComboBox.TabStop = false;
            this.CompanyComboBox.SelectedIndexChanged += new System.EventHandler(this.CompanyComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(333, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выберите номер школы из списка";
            // 
            // NewNameTextBox
            // 
            this.NewNameTextBox.Enabled = false;
            this.NewNameTextBox.Location = new System.Drawing.Point(30, 166);
            this.NewNameTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.NewNameTextBox.Name = "NewNameTextBox";
            this.NewNameTextBox.Size = new System.Drawing.Size(293, 22);
            this.NewNameTextBox.TabIndex = 0;
            this.NewNameTextBox.TextChanged += new System.EventHandler(this.NewNameTextBox_TextChanged_1);
            // 
            // ChangeButton
            // 
            this.ChangeButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ChangeButton.Enabled = false;
            this.ChangeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChangeButton.Location = new System.Drawing.Point(114, 229);
            this.ChangeButton.Margin = new System.Windows.Forms.Padding(4);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(145, 44);
            this.ChangeButton.TabIndex = 1;
            this.ChangeButton.Text = "Изменить";
            this.ChangeButton.UseVisualStyleBackColor = false;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(13, 137);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(357, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Введите новое количество учителей";
            this.label3.Visible = false;
            // 
            // ChangeCipher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(375, 288);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.NewNameTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CompanyComboBox);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChangeCipher";
            this.Text = "Изменение шифра";
            this.Load += new System.EventHandler(this.ChangeCipher_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox CompanyComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox NewNameTextBox;
        private System.Windows.Forms.Button ChangeButton;
        private System.Windows.Forms.Label label3;
    }
}