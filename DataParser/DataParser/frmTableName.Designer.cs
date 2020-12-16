namespace DataParser
{
    partial class frmTableName
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
            this.tableName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.OKbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tableName
            // 
            this.tableName.Location = new System.Drawing.Point(13, 32);
            this.tableName.Name = "tableName";
            this.tableName.Size = new System.Drawing.Size(241, 22);
            this.tableName.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Enter data table name:";
            // 
            // OKbtn
            // 
            this.OKbtn.Location = new System.Drawing.Point(179, 69);
            this.OKbtn.Name = "OKbtn";
            this.OKbtn.Size = new System.Drawing.Size(75, 23);
            this.OKbtn.TabIndex = 9;
            this.OKbtn.Text = "OK";
            this.OKbtn.UseVisualStyleBackColor = true;
            // 
            // frmTableName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 109);
            this.Controls.Add(this.OKbtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tableName);
            this.Name = "frmTableName";
            this.Text = "frmTableName";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox tableName;
        public System.Windows.Forms.Button OKbtn;
    }
}