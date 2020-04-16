namespace Real_Estate_Managment_Software___GUI
{
    partial class ListItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl_Main = new System.Windows.Forms.TableLayoutPanel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.btn_Add = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel10 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.pnl_Main.SuspendLayout();
            this.panel16.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_Main
            // 
            this.pnl_Main.ColumnCount = 5;
            this.pnl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.pnl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.pnl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26F));
            this.pnl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37F));
            this.pnl_Main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pnl_Main.Controls.Add(this.panel16, 4, 0);
            this.pnl_Main.Controls.Add(this.panel11, 3, 0);
            this.pnl_Main.Controls.Add(this.panel10, 2, 0);
            this.pnl_Main.Controls.Add(this.panel9, 1, 0);
            this.pnl_Main.Controls.Add(this.panel8, 0, 0);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Main.Location = new System.Drawing.Point(0, 0);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.RowCount = 1;
            this.pnl_Main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnl_Main.Size = new System.Drawing.Size(1112, 56);
            this.pnl_Main.TabIndex = 2;
            this.pnl_Main.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.pnl_Main.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // panel16
            // 
            this.panel16.BackColor = System.Drawing.Color.White;
            this.panel16.Controls.Add(this.btn_Add);
            this.panel16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel16.Location = new System.Drawing.Point(1000, 1);
            this.panel16.Margin = new System.Windows.Forms.Padding(1);
            this.panel16.Name = "panel16";
            this.panel16.Size = new System.Drawing.Size(111, 54);
            this.panel16.TabIndex = 8;
            this.panel16.Enter += new System.EventHandler(this.panel16_Enter);
            this.panel16.Leave += new System.EventHandler(this.panel16_Leave);
            this.panel16.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.panel16.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Add.FlatAppearance.BorderSize = 0;
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Add.Image = global::Real_Estate_Managment_Software___GUI.Properties.Resources.AddBtnGreen;
            this.btn_Add.Location = new System.Drawing.Point(36, 11);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(37, 30);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            this.btn_Add.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.btn_Add.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.White;
            this.panel11.Controls.Add(this.label5);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(589, 1);
            this.panel11.Margin = new System.Windows.Forms.Padding(1);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(409, 54);
            this.panel11.TabIndex = 3;
            this.panel11.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.panel11.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label5.Location = new System.Drawing.Point(15, 17);
            this.label5.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 22);
            this.label5.TabIndex = 2;
            this.label5.Text = "Created Date";
            this.label5.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.label5.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.White;
            this.panel10.Controls.Add(this.label4);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(300, 1);
            this.panel10.Margin = new System.Windows.Forms.Padding(1);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(287, 54);
            this.panel10.TabIndex = 2;
            this.panel10.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.panel10.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Location = new System.Drawing.Point(15, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 22);
            this.label4.TabIndex = 1;
            this.label4.Text = "Job Name";
            this.label4.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.label4.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.White;
            this.panel9.Controls.Add(this.label3);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(78, 1);
            this.panel9.Margin = new System.Windows.Forms.Padding(1);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(220, 54);
            this.panel9.TabIndex = 1;
            this.panel9.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.panel9.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Montserrat Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(15, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(15, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 22);
            this.label3.TabIndex = 0;
            this.label3.Text = "Job ID";
            this.label3.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.label3.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.White;
            this.panel8.Controls.Add(this.btn_Delete);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(1, 1);
            this.panel8.Margin = new System.Windows.Forms.Padding(1);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(75, 54);
            this.panel8.TabIndex = 0;
            this.panel8.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.panel8.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_Delete.BackColor = System.Drawing.Color.White;
            this.btn_Delete.FlatAppearance.BorderSize = 0;
            this.btn_Delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Delete.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Delete.Image = global::Real_Estate_Managment_Software___GUI.Properties.Resources.DeleteIconRed;
            this.btn_Delete.Location = new System.Drawing.Point(20, 12);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(37, 30);
            this.btn_Delete.TabIndex = 1;
            this.btn_Delete.UseVisualStyleBackColor = false;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            this.btn_Delete.MouseEnter += new System.EventHandler(this.panel10_MouseEnter);
            this.btn_Delete.MouseLeave += new System.EventHandler(this.btn_Delete_MouseLeave);
            // 
            // ListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(237)))), ((int)(((byte)(248)))));
            this.Controls.Add(this.pnl_Main);
            this.Name = "ListItem";
            this.Size = new System.Drawing.Size(1112, 56);
            this.pnl_Main.ResumeLayout(false);
            this.panel16.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel pnl_Main;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btn_Delete;
    }
}
