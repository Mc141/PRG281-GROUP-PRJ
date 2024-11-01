namespace PRG282_PRJ
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
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.btnGenerateSummary = new System.Windows.Forms.Button();
            this.btn = new System.Windows.Forms.Button();
            this.btnUpdateStudent = new System.Windows.Forms.Button();
            this.lblStudentId = new System.Windows.Forms.Label();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblCourse = new System.Windows.Forms.Label();
            this.txtStudentID = new System.Windows.Forms.TextBox();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtIdSearch = new System.Windows.Forms.TextBox();
            this.lblSearchId = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cmbCourses = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStudents
            // 
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Location = new System.Drawing.Point(312, 23);
            this.dgvStudents.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.Size = new System.Drawing.Size(566, 299);
            this.dgvStudents.TabIndex = 0;
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.Location = new System.Drawing.Point(312, 353);
            this.btnAddStudent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(123, 27);
            this.btnAddStudent.TabIndex = 5;
            this.btnAddStudent.Text = "Add Student";
            this.btnAddStudent.UseVisualStyleBackColor = true;
            // 
            // btnGenerateSummary
            // 
            this.btnGenerateSummary.Location = new System.Drawing.Point(755, 353);
            this.btnGenerateSummary.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnGenerateSummary.Name = "btnGenerateSummary";
            this.btnGenerateSummary.Size = new System.Drawing.Size(123, 27);
            this.btnGenerateSummary.TabIndex = 6;
            this.btnGenerateSummary.Text = "Generate Summary";
            this.btnGenerateSummary.UseVisualStyleBackColor = true;
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(574, 353);
            this.btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(123, 27);
            this.btn.TabIndex = 7;
            this.btn.Text = "Delete Student";
            this.btn.UseVisualStyleBackColor = true;
            // 
            // btnUpdateStudent
            // 
            this.btnUpdateStudent.Location = new System.Drawing.Point(443, 353);
            this.btnUpdateStudent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnUpdateStudent.Name = "btnUpdateStudent";
            this.btnUpdateStudent.Size = new System.Drawing.Size(123, 27);
            this.btnUpdateStudent.TabIndex = 8;
            this.btnUpdateStudent.Text = "Update Student";
            this.btnUpdateStudent.UseVisualStyleBackColor = true;
            // 
            // lblStudentId
            // 
            this.lblStudentId.AutoSize = true;
            this.lblStudentId.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblStudentId.Location = new System.Drawing.Point(90, 49);
            this.lblStudentId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStudentId.Name = "lblStudentId";
            this.lblStudentId.Size = new System.Drawing.Size(64, 15);
            this.lblStudentId.TabIndex = 1;
            this.lblStudentId.Text = "Student ID";
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblFirstName.Location = new System.Drawing.Point(86, 87);
            this.lblFirstName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(67, 15);
            this.lblFirstName.TabIndex = 9;
            this.lblFirstName.Text = "First Name";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblLastName.Location = new System.Drawing.Point(87, 129);
            this.lblLastName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(67, 15);
            this.lblLastName.TabIndex = 10;
            this.lblLastName.Text = "Last Name";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblAge.Location = new System.Drawing.Point(124, 169);
            this.lblAge.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(28, 15);
            this.lblAge.TabIndex = 11;
            this.lblAge.Text = "Age";
            // 
            // lblCourse
            // 
            this.lblCourse.AutoSize = true;
            this.lblCourse.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblCourse.Location = new System.Drawing.Point(106, 209);
            this.lblCourse.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(46, 15);
            this.lblCourse.TabIndex = 12;
            this.lblCourse.Text = "Course";
            // 
            // txtStudentID
            // 
            this.txtStudentID.Location = new System.Drawing.Point(161, 43);
            this.txtStudentID.Name = "txtStudentID";
            this.txtStudentID.Size = new System.Drawing.Size(120, 21);
            this.txtStudentID.TabIndex = 13;
            // 
            // txtAge
            // 
            this.txtAge.Location = new System.Drawing.Point(159, 163);
            this.txtAge.Name = "txtAge";
            this.txtAge.Size = new System.Drawing.Size(121, 21);
            this.txtAge.TabIndex = 15;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(160, 123);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(121, 21);
            this.txtLastName.TabIndex = 16;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(160, 81);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(120, 21);
            this.txtFirstName.TabIndex = 17;
            // 
            // txtIdSearch
            // 
            this.txtIdSearch.Location = new System.Drawing.Point(80, 310);
            this.txtIdSearch.Name = "txtIdSearch";
            this.txtIdSearch.Size = new System.Drawing.Size(120, 21);
            this.txtIdSearch.TabIndex = 19;
            // 
            // lblSearchId
            // 
            this.lblSearchId.AutoSize = true;
            this.lblSearchId.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblSearchId.Location = new System.Drawing.Point(12, 316);
            this.lblSearchId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearchId.Name = "lblSearchId";
            this.lblSearchId.Size = new System.Drawing.Size(61, 15);
            this.lblSearchId.TabIndex = 18;
            this.lblSearchId.Text = "Search ID";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(113, 353);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(87, 27);
            this.btnSearch.TabIndex = 20;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // cmbCourses
            // 
            this.cmbCourses.FormattingEnabled = true;
            this.cmbCourses.Location = new System.Drawing.Point(159, 201);
            this.cmbCourses.Name = "cmbCourses";
            this.cmbCourses.Size = new System.Drawing.Size(121, 23);
            this.cmbCourses.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(903, 402);
            this.Controls.Add(this.cmbCourses);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtIdSearch);
            this.Controls.Add(this.lblSearchId);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.txtStudentID);
            this.Controls.Add(this.lblCourse);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblLastName);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.btnUpdateStudent);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.btnGenerateSummary);
            this.Controls.Add(this.btnAddStudent);
            this.Controls.Add(this.lblStudentId);
            this.Controls.Add(this.dgvStudents);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Student Management System";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Button btnAddStudent;
        private System.Windows.Forms.Button btnGenerateSummary;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button btnUpdateStudent;
        private System.Windows.Forms.Label lblStudentId;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblCourse;
        private System.Windows.Forms.TextBox txtStudentID;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtIdSearch;
        private System.Windows.Forms.Label lblSearchId;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbCourses;
    }
}

