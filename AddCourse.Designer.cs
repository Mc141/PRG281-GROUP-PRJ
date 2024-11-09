namespace PRG282_PRJ
{
    partial class AddCourse
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
            this.txtAddCourse = new System.Windows.Forms.TextBox();
            this.btnAddCourse = new System.Windows.Forms.Button();
            this.lblAddCourse = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtAddCourse
            // 
            this.txtAddCourse.Location = new System.Drawing.Point(92, 24);
            this.txtAddCourse.Name = "txtAddCourse";
            this.txtAddCourse.Size = new System.Drawing.Size(139, 20);
            this.txtAddCourse.TabIndex = 0;
            // 
            // btnAddCourse
            // 
            this.btnAddCourse.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnAddCourse.Location = new System.Drawing.Point(109, 63);
            this.btnAddCourse.Name = "btnAddCourse";
            this.btnAddCourse.Size = new System.Drawing.Size(75, 23);
            this.btnAddCourse.TabIndex = 1;
            this.btnAddCourse.Text = "Add";
            this.btnAddCourse.UseVisualStyleBackColor = true;
            this.btnAddCourse.Click += new System.EventHandler(this.btnAddCourse_Click);
            // 
            // lblAddCourse
            // 
            this.lblAddCourse.AutoSize = true;
            this.lblAddCourse.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblAddCourse.Location = new System.Drawing.Point(15, 27);
            this.lblAddCourse.Name = "lblAddCourse";
            this.lblAddCourse.Size = new System.Drawing.Size(71, 13);
            this.lblAddCourse.TabIndex = 2;
            this.lblAddCourse.Text = "Course Name";
            // 
            // AddCourse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(279, 102);
            this.Controls.Add(this.lblAddCourse);
            this.Controls.Add(this.btnAddCourse);
            this.Controls.Add(this.txtAddCourse);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AddCourse";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Course";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAddCourse;
        private System.Windows.Forms.Button btnAddCourse;
        private System.Windows.Forms.Label lblAddCourse;
    }
}