using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282_PRJ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void RenameHeaders()
        {
            dgvStudents.Columns[0].HeaderText = "Student ID";
            dgvStudents.Columns[1].HeaderText = "First Name";
            dgvStudents.Columns[2].HeaderText = "Last Name";
        }

        private void LoadData()
        {
            BindingSource bindingSource = new BindingSource();
            DataHandler handler = new DataHandler();

            bindingSource.DataSource = handler.GetStudents();
            dgvStudents.DataSource = bindingSource;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            RenameHeaders();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(txtStudentID.Text) ||
                string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                string.IsNullOrWhiteSpace(txtLastName.Text) ||
                string.IsNullOrWhiteSpace(txtAge.Text) ||
                string.IsNullOrWhiteSpace(cmbCourses.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if validation fails
            }

            // Parse the Student ID and Age to integers
            if (!int.TryParse(txtStudentID.Text, out int studentId) || !int.TryParse(txtAge.Text, out int age) ||
                age < 18 || age > 90)
            {
                MessageBox.Show("Student ID and Age must be valid integers. Age must be between 18 and 90.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if parsing fails
            }

            // Validate that FirstName and LastName are only text
            Regex textOnlyPattern = new Regex("^[a-zA-Z]+$");
            if (!textOnlyPattern.IsMatch(txtFirstName.Text))
            {
                MessageBox.Show("First name must contain only letters.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!textOnlyPattern.IsMatch(txtLastName.Text))
            {
                MessageBox.Show("Last name must contain only letters.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Format FirstName and LastName: make lowercase and capitalize the first letter
            string formattedFirstName = char.ToUpper(txtFirstName.Text.ToLower()[0]) + txtFirstName.Text.ToLower().Substring(1);
            string formattedLastName = char.ToUpper(txtLastName.Text.ToLower()[0]) + txtLastName.Text.ToLower().Substring(1);

            // Create a new Student object with the formatted input values
            Student newStudent = new Student(studentId, formattedFirstName, formattedLastName, age, cmbCourses.Text);

            // Create an instance of DataHandler
            DataHandler handler = new DataHandler();

            // Call the AddStudent method to add the new student
            handler.AddStudent(newStudent);

            // Refresh the data in the DataGridView to show the new student
            LoadData();

            // Clear input fields after adding
            txtStudentID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cmbCourses.ResetText();
        }
    }
}