namespace DataParser
{
    partial class MainForm
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
            this.defaultDataType = new System.Windows.Forms.RadioButton();
            this.sqlDataType = new System.Windows.Forms.RadioButton();
            this.group = new System.Windows.Forms.GroupBox();
            this.selectTableBtn = new System.Windows.Forms.Button();
            this.selectFileBtn = new System.Windows.Forms.Button();
            this.parseBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.group.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultDataType
            // 
            this.defaultDataType.AutoSize = true;
            this.defaultDataType.Checked = true;
            this.defaultDataType.Location = new System.Drawing.Point(6, 32);
            this.defaultDataType.Name = "defaultDataType";
            this.defaultDataType.Size = new System.Drawing.Size(71, 21);
            this.defaultDataType.TabIndex = 5;
            this.defaultDataType.TabStop = true;
            this.defaultDataType.Text = "Defaut";
            this.defaultDataType.UseVisualStyleBackColor = true;
            // 
            // sqlDataType
            // 
            this.sqlDataType.AutoSize = true;
            this.sqlDataType.Location = new System.Drawing.Point(6, 59);
            this.sqlDataType.Name = "sqlDataType";
            this.sqlDataType.Size = new System.Drawing.Size(57, 21);
            this.sqlDataType.TabIndex = 6;
            this.sqlDataType.Text = "SQL";
            this.sqlDataType.UseVisualStyleBackColor = true;
            // 
            // group
            // 
            this.group.Controls.Add(this.parseBtn);
            this.group.Controls.Add(this.selectFileBtn);
            this.group.Controls.Add(this.selectTableBtn);
            this.group.Controls.Add(this.exitBtn);
            this.group.Controls.Add(this.defaultDataType);
            this.group.Controls.Add(this.sqlDataType);
            this.group.Location = new System.Drawing.Point(2, 5);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(397, 137);
            this.group.TabIndex = 8;
            this.group.TabStop = false;
            this.group.Text = "Use data types format:";
            // 
            // selectTableBtn
            // 
            this.selectTableBtn.Location = new System.Drawing.Point(108, 104);
            this.selectTableBtn.Name = "selectTableBtn";
            this.selectTableBtn.Size = new System.Drawing.Size(153, 23);
            this.selectTableBtn.TabIndex = 11;
            this.selectTableBtn.Text = "Select another table";
            this.selectTableBtn.UseVisualStyleBackColor = true;
            this.selectTableBtn.Click += new System.EventHandler(this.selectTableBtn_Click);
            // 
            // selectFileBtn
            // 
            this.selectFileBtn.Location = new System.Drawing.Point(10, 104);
            this.selectFileBtn.Name = "selectFileBtn";
            this.selectFileBtn.Size = new System.Drawing.Size(92, 23);
            this.selectFileBtn.TabIndex = 13;
            this.selectFileBtn.Text = "Select file";
            this.selectFileBtn.UseVisualStyleBackColor = true;
            this.selectFileBtn.Click += new System.EventHandler(this.selectFileBtn_Click);
            // 
            // parseBtn
            // 
            this.parseBtn.Location = new System.Drawing.Point(267, 104);
            this.parseBtn.Name = "parseBtn";
            this.parseBtn.Size = new System.Drawing.Size(119, 23);
            this.parseBtn.TabIndex = 14;
            this.parseBtn.Text = "Parse and Save";
            this.parseBtn.UseVisualStyleBackColor = true;
            this.parseBtn.Click += new System.EventHandler(this.parseBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(311, 21);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(75, 23);
            this.exitBtn.TabIndex = 10;
            this.exitBtn.Text = "Exit";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(400, 147);
            this.Controls.Add(this.group);
            this.Name = "MainForm";
            this.Text = "DataParser";
            this.group.ResumeLayout(false);
            this.group.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RadioButton defaultDataType;
        private System.Windows.Forms.RadioButton sqlDataType;
        private System.Windows.Forms.GroupBox group;
        private System.Windows.Forms.Button selectFileBtn;
        private System.Windows.Forms.Button selectTableBtn;
        private System.Windows.Forms.Button parseBtn;
        private System.Windows.Forms.Button exitBtn;
    }
}

