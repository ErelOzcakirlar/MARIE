namespace MARIE
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.IRvalue = new System.Windows.Forms.Label();
            this.PCvalue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MARvalue = new System.Windows.Forms.Label();
            this.MBRvalue = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ACvalue = new System.Windows.Forms.Label();
            this.CodeBox = new System.Windows.Forms.RichTextBox();
            this.Clear = new System.Windows.Forms.Button();
            this.Load = new System.Windows.Forms.Button();
            this.Run = new System.Windows.Forms.Button();
            this.MemoryView = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.LabelView = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.OutRegValue = new System.Windows.Forms.Label();
            this.InRegValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.MemoryView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LabelView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 539);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "PC :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 518);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "IR :";
            // 
            // IRvalue
            // 
            this.IRvalue.AutoSize = true;
            this.IRvalue.Location = new System.Drawing.Point(72, 518);
            this.IRvalue.Name = "IRvalue";
            this.IRvalue.Size = new System.Drawing.Size(31, 13);
            this.IRvalue.TabIndex = 2;
            this.IRvalue.Text = "0000";
            // 
            // PCvalue
            // 
            this.PCvalue.AutoSize = true;
            this.PCvalue.Location = new System.Drawing.Point(72, 539);
            this.PCvalue.Name = "PCvalue";
            this.PCvalue.Size = new System.Drawing.Size(25, 13);
            this.PCvalue.TabIndex = 2;
            this.PCvalue.Text = "000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 498);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "MBR :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 477);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "MAR :";
            // 
            // MARvalue
            // 
            this.MARvalue.AutoSize = true;
            this.MARvalue.Location = new System.Drawing.Point(72, 477);
            this.MARvalue.Name = "MARvalue";
            this.MARvalue.Size = new System.Drawing.Size(25, 13);
            this.MARvalue.TabIndex = 2;
            this.MARvalue.Text = "000";
            // 
            // MBRvalue
            // 
            this.MBRvalue.AutoSize = true;
            this.MBRvalue.Location = new System.Drawing.Point(72, 498);
            this.MBRvalue.Name = "MBRvalue";
            this.MBRvalue.Size = new System.Drawing.Size(31, 13);
            this.MBRvalue.TabIndex = 2;
            this.MBRvalue.Text = "0000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 455);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "AC :";
            // 
            // ACvalue
            // 
            this.ACvalue.AutoSize = true;
            this.ACvalue.Location = new System.Drawing.Point(72, 455);
            this.ACvalue.Name = "ACvalue";
            this.ACvalue.Size = new System.Drawing.Size(31, 13);
            this.ACvalue.TabIndex = 2;
            this.ACvalue.Text = "0000";
            // 
            // CodeBox
            // 
            this.CodeBox.Location = new System.Drawing.Point(12, 29);
            this.CodeBox.Name = "CodeBox";
            this.CodeBox.Size = new System.Drawing.Size(350, 423);
            this.CodeBox.TabIndex = 3;
            this.CodeBox.Text = "";
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(125, 458);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(75, 23);
            this.Clear.TabIndex = 4;
            this.Clear.Text = "Temizle";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(206, 458);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(75, 23);
            this.Load.TabIndex = 4;
            this.Load.Text = "Yükle";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(287, 458);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 4;
            this.Run.Text = "Çalıştır";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // MemoryView
            // 
            this.MemoryView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MemoryView.Location = new System.Drawing.Point(369, 29);
            this.MemoryView.Name = "MemoryView";
            this.MemoryView.Size = new System.Drawing.Size(203, 300);
            this.MemoryView.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(369, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Bellek :";
            // 
            // LabelView
            // 
            this.LabelView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.LabelView.Location = new System.Drawing.Point(369, 348);
            this.LabelView.Name = "LabelView";
            this.LabelView.RowHeadersVisible = false;
            this.LabelView.Size = new System.Drawing.Size(203, 201);
            this.LabelView.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(369, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Etiketler :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Program :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(122, 538);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "OutReg :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(122, 518);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(42, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "InReg :";
            // 
            // OutRegValue
            // 
            this.OutRegValue.AutoSize = true;
            this.OutRegValue.Location = new System.Drawing.Point(182, 538);
            this.OutRegValue.Name = "OutRegValue";
            this.OutRegValue.Size = new System.Drawing.Size(31, 13);
            this.OutRegValue.TabIndex = 2;
            this.OutRegValue.Text = "0000";
            // 
            // InRegValue
            // 
            this.InRegValue.AutoSize = true;
            this.InRegValue.Location = new System.Drawing.Point(182, 518);
            this.InRegValue.Name = "InRegValue";
            this.InRegValue.Size = new System.Drawing.Size(31, 13);
            this.InRegValue.TabIndex = 2;
            this.InRegValue.Text = "0000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 561);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LabelView);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.MemoryView);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.Load);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.CodeBox);
            this.Controls.Add(this.InRegValue);
            this.Controls.Add(this.MBRvalue);
            this.Controls.Add(this.PCvalue);
            this.Controls.Add(this.ACvalue);
            this.Controls.Add(this.MARvalue);
            this.Controls.Add(this.OutRegValue);
            this.Controls.Add(this.IRvalue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Marie Simulator";
            ((System.ComponentModel.ISupportInitialize)(this.MemoryView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LabelView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label IRvalue;
        private System.Windows.Forms.Label PCvalue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label MARvalue;
        private System.Windows.Forms.Label MBRvalue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label ACvalue;
        private System.Windows.Forms.RichTextBox CodeBox;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button Load;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.DataGridView MemoryView;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView LabelView;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label OutRegValue;
        private System.Windows.Forms.Label InRegValue;
    }
}

