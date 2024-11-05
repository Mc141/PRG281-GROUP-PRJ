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
        private DataHandler dataHandler;
        public Form1()
        {
            InitializeComponent();
            dataHandler = new DataHandler();
        }

        BindingSource bindingSource = new BindingSource();


        private void RenameHeaders() // Rename all the headers properly
        {
            dgvStudents.Columns[0].HeaderText = "Student ID";
            dgvStudents.Columns[1].HeaderText = "First Name";
            dgvStudents.Columns[2].HeaderText = "Last Name";
        }

        private void LoadData(List<Student> dataList)
        {
            // Set the binding source to the provided student data list.
            bindingSource.DataSource = dataList;

            // Bind the data source to the DataGridView for display.
            dgvStudents.DataSource = bindingSource;

            // Update the summary display after loading the data.
            DisplaySummary();
        }

        private void LoadStudents()
        {
            dgvStudents.DataSource = null;
            dgvStudents.DataSource = dataHandler.GetStudents(); // Reload the students list into the DataGridView.
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // Populate DGV and renames headers.
            LoadData(Program.dataHandler.GetStudents());
            RenameHeaders();

            // Populate course comboBox
            cmbCourses.Items.AddRange(Program.fileHandler.ReadCourses().ToArray());

            dgvStudents.Columns[4].Width = 155;

        }

        private void DisplaySummary()
        {
            // Update the label to display the generated summary.
            lblSummary.Text = Program.fileHandler.GenerateSummary(Program.dataHandler.CalculateSummary(Program.fileHandler.Read()));
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
            Program.dataHandler.GetStudents();

            // Clear input fields after adding
            txtStudentID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cmbCourses.SelectedIndex = -1;

            // Refresh the dataGridView and Summary Label
            bindingSource.DataSource = Program.fileHandler.Read();

            // Update the summary text label
            DisplaySummary();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Declare a variable to store the search ID.
            int searchId;

            // Try to parse the input text from txtIdSearch as an integer.
            if (int.TryParse(txtIdSearch.Text, out searchId))
            {
                // Search for the student with the given ID in the list of students.
                Student searchedStudent = Program.dataHandler.SearchId(searchId, Program.dataHandler.GetStudents());

                // Check if the student was found.
                if (searchedStudent != null)
                {
                    // If found, create a list with the searched student (to display the result) and load the data.
                    List<Student> results = new List<Student> { searchedStudent };
                    LoadData(results); // Load the search results into the DataGridView.
                }
                else
                {
                    // If the student is not found, display a message to inform the user.
                    MessageBox.Show($"There is no student with the ID: {searchId}", "Student Not Found!");
                }
            }
            else
            {
                // If the input is not a valid integer, display an error message.
                MessageBox.Show("The entered ID is not valid.", "Invalid ID!");
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            // Reset the Search result and text box.
            LoadData(Program.dataHandler.GetStudents());
            txtIdSearch.Text = "";
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex; //Catch seleted row index

            try
            {
                // Write the selected row's values to the corresponding text boxes.
                DataGridViewRow row = dgvStudents.Rows[indexRow];

                txtStudentID.Text = row.Cells[0].Value.ToString();
                txtFirstName.Text = row.Cells[1].Value.ToString();
                txtLastName.Text = row.Cells[2].Value.ToString();
                txtAge.Text = row.Cells[3].Value.ToString();
                cmbCourses.Text = row.Cells[4].Value.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
                // Prevents the selection of the head row. Selecting it, throws an exception that will be caught here, and ignored as it doesn't affect the program execution.
            }
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            int studentId;
            int age;

            // Ensure ID and Age fields are valid integers.
            if (!int.TryParse(txtStudentID.Text, out studentId) || !int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Please enter valid ID and Age values.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Collect updated data for the selected student.
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string course = cmbCourses.SelectedItem?.ToString() ?? string.Empty; // Get selected course.

            // Create the updated Student object.
            Student updatedStudent = new Student(studentId, firstName, lastName, age, course);

            // Update the student record.
            dataHandler.UpdateStudent(updatedStudent);
            LoadStudents(); // Refresh the DataGridView to show updated data.

            // Clear input fields after updating.
            ClearInputFields();
        }

        private void ClearInputFields()
        {
            txtStudentID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cmbCourses.SelectedIndex = -1; // Reset combo box selection.
        }

        private void btn_Click(object sender, EventArgs e)
        {
            int studentId;

            // Ensure that the ID field contains a valid integer.
            if (!int.TryParse(txtStudentID.Text, out studentId))
            {
                MessageBox.Show("Please enter a valid ID value.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Delete the student record.
            dataHandler.DeleteStudent(studentId);
            LoadStudents(); // Refresh the DataGridView to show updated data.

            // Clear input fields after deleting.
            ClearInputFields();
        }
    }
}