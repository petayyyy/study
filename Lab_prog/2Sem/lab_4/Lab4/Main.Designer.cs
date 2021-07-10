namespace Lab4
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.BusinessesDGV = new System.Windows.Forms.DataGridView();
            this.ProductsDGV = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TotalOtputDGV = new System.Windows.Forms.DataGridView();
            this.ProductDataButton = new System.Windows.Forms.Button();
            this.AnnualCostButton = new System.Windows.Forms.Button();
            this.ReplacingSlateButton = new System.Windows.Forms.Button();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.AddOrChangeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BusinessesDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalOtputDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // BusinessesDGV
            // 
            this.BusinessesDGV.AllowUserToAddRows = false;
            this.BusinessesDGV.AllowUserToDeleteRows = false;
            this.BusinessesDGV.AllowUserToResizeColumns = false;
            this.BusinessesDGV.AllowUserToResizeRows = false;
            this.BusinessesDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BusinessesDGV.Location = new System.Drawing.Point(328, 25);
            this.BusinessesDGV.Name = "BusinessesDGV";
            this.BusinessesDGV.ReadOnly = true;
            this.BusinessesDGV.Size = new System.Drawing.Size(445, 179);
            this.BusinessesDGV.TabIndex = 3;
            this.BusinessesDGV.TabStop = false;
            // 
            // ProductsDGV
            // 
            this.ProductsDGV.AllowUserToAddRows = false;
            this.ProductsDGV.AllowUserToDeleteRows = false;
            this.ProductsDGV.AllowUserToResizeColumns = false;
            this.ProductsDGV.AllowUserToResizeRows = false;
            this.ProductsDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProductsDGV.Location = new System.Drawing.Point(779, 25);
            this.ProductsDGV.Name = "ProductsDGV";
            this.ProductsDGV.ReadOnly = true;
            this.ProductsDGV.Size = new System.Drawing.Size(704, 388);
            this.ProductsDGV.TabIndex = 3;
            this.ProductsDGV.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(328, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Предприятия";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(776, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Изделия";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(328, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Суммарная стоимость выпуска";
            // 
            // TotalOtputDGV
            // 
            this.TotalOtputDGV.AllowUserToAddRows = false;
            this.TotalOtputDGV.AllowUserToDeleteRows = false;
            this.TotalOtputDGV.AllowUserToResizeColumns = false;
            this.TotalOtputDGV.AllowUserToResizeRows = false;
            this.TotalOtputDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TotalOtputDGV.Location = new System.Drawing.Point(328, 223);
            this.TotalOtputDGV.Name = "TotalOtputDGV";
            this.TotalOtputDGV.ReadOnly = true;
            this.TotalOtputDGV.Size = new System.Drawing.Size(445, 190);
            this.TotalOtputDGV.TabIndex = 3;
            this.TotalOtputDGV.TabStop = false;
            // 
            // ProductDataButton
            // 
            this.ProductDataButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProductDataButton.Location = new System.Drawing.Point(12, 9);
            this.ProductDataButton.Name = "ProductDataButton";
            this.ProductDataButton.Size = new System.Drawing.Size(310, 76);
            this.ProductDataButton.TabIndex = 0;
            this.ProductDataButton.Text = "Данные об изделиях";
            this.ProductDataButton.UseVisualStyleBackColor = true;
            this.ProductDataButton.Click += new System.EventHandler(this.ProductDataButton_Click);
            // 
            // AnnualCostButton
            // 
            this.AnnualCostButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AnnualCostButton.Location = new System.Drawing.Point(12, 91);
            this.AnnualCostButton.Name = "AnnualCostButton";
            this.AnnualCostButton.Size = new System.Drawing.Size(310, 76);
            this.AnnualCostButton.TabIndex = 1;
            this.AnnualCostButton.Text = "Сведения о годовой стоимости ";
            this.AnnualCostButton.UseVisualStyleBackColor = true;
            this.AnnualCostButton.Click += new System.EventHandler(this.AnnualCostButton_Click);
            // 
            // ReplacingSlateButton
            // 
            this.ReplacingSlateButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ReplacingSlateButton.Location = new System.Drawing.Point(12, 173);
            this.ReplacingSlateButton.Name = "ReplacingSlateButton";
            this.ReplacingSlateButton.Size = new System.Drawing.Size(310, 76);
            this.ReplacingSlateButton.TabIndex = 2;
            this.ReplacingSlateButton.Text = "Замена шифра";
            this.ReplacingSlateButton.UseVisualStyleBackColor = true;
            this.ReplacingSlateButton.Click += new System.EventHandler(this.ReplacingSlateButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.RefreshButton.Location = new System.Drawing.Point(12, 255);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(310, 76);
            this.RefreshButton.TabIndex = 3;
            this.RefreshButton.Text = "Обновить";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // AddOrChangeButton
            // 
            this.AddOrChangeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddOrChangeButton.Location = new System.Drawing.Point(12, 337);
            this.AddOrChangeButton.Name = "AddOrChangeButton";
            this.AddOrChangeButton.Size = new System.Drawing.Size(310, 76);
            this.AddOrChangeButton.TabIndex = 4;
            this.AddOrChangeButton.Text = "Добавить или изменить";
            this.AddOrChangeButton.UseVisualStyleBackColor = true;
            this.AddOrChangeButton.Click += new System.EventHandler(this.AddOrChangeButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1495, 419);
            this.Controls.Add(this.AddOrChangeButton);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.ReplacingSlateButton);
            this.Controls.Add(this.AnnualCostButton);
            this.Controls.Add(this.ProductDataButton);
            this.Controls.Add(this.TotalOtputDGV);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ProductsDGV);
            this.Controls.Add(this.BusinessesDGV);
            this.Name = "Main";
            this.Text = "Предприятия";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BusinessesDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TotalOtputDGV)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView BusinessesDGV;
        private System.Windows.Forms.DataGridView ProductsDGV;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView TotalOtputDGV;
        private System.Windows.Forms.Button ProductDataButton;
        private System.Windows.Forms.Button AnnualCostButton;
        private System.Windows.Forms.Button ReplacingSlateButton;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.Button AddOrChangeButton;
    }
}

