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
        static DataHandler dataHandler = Program.dataHandler;
        static FileHandler fileHandler = Program.fileHandler;
        AddCourse addCourseForm;

        public Form1()
        {
            InitializeComponent();
        }

        // binding source for the DataGridView as a source
        BindingSource bindingSource = new BindingSource();

        private void Form1_Load(object sender, EventArgs e)
        {
            // Populate DGV and renames headers.
            dataHandler.students = fileHandler.Read(); // Load existing students from the file.
            RefreshData(dataHandler.students);
            RenameHeaders();

            // Populate course comboBox
            cmbCourses.Items.AddRange(fileHandler.ReadCourses().ToArray());

            dgvStudents.Columns[4].Width = 155; // Set column width to make enough space to display courses in full.
        }


        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int indexRow = e.RowIndex; //Catch seleted row index

            try
            {
                PopulateInputs(indexRow); //Populat the input fields based on the selected record
            }
            catch (ArgumentOutOfRangeException)
            {
                // Prevents the selection of the head row. Selecting it, throws an exception that will be caught here, and ignored as it doesn't affect the program execution.
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            string newStudentId;
            // If the file is empty generate a predefined id for the first student, else auto  generate a new id based on the last entered record.
            if (fileHandler.IsFileEmpty(fileHandler.studentsFilePath))
            {
                newStudentId = "0";
            }
            else
            {
                newStudentId = (dataHandler.students[dataHandler.students.Count - 1].StudentId + 1).ToString();
            }

            // Validate inputs and exit if any validation fails
            if (!dataHandler.ValidInputs(newStudentId, txtFirstName.Text, txtLastName.Text, txtAge.Text, cmbCourses.Text, out int studentId, out int age))
            {
                return; // Exit if inputs are not valid
            }

            // Collect and format student data after the validation
            Student newStudent = dataHandler.CollectAndFormatStudentData(newStudentId, txtFirstName.Text, txtLastName.Text, txtAge.Text, cmbCourses.Text, out studentId, out age);

            // Show a confirmation dialog before adding the new student
            if (!ShowConfirmationDialog("add", studentId, newStudent.FirstName, newStudent.LastName, age, cmbCourses.Text))
            {
                return; // Exit if the user does not confirm
            }

            // If confirmed, call the AddStudent method to add the new student
            bool success = dataHandler.AddStudent(fileHandler, newStudent);

            // Refresh the data in the DataGridView to show the new student as well
            RefreshData(dataHandler.students);

            // Clear input fields after adding
            ClearInputFields();

            // Reset the Search result and text box.
            ClearSearch();

            // If the adding was successful, show a message box indicating success.
            if (success)
            {
                MessageBox.Show("Student record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Try to parse the input text from txtIdSearch as an integer, and continue with the search if success.
            if (int.TryParse(txtIdSearch.Text, out int searchId))
            {
                // Search for the student with the given ID in the list of students.
                Student searchedStudent = dataHandler.SearchId(searchId, dataHandler.students);

                // Check if the student was found.
                if (searchedStudent != null)
                {
                    // If found, create a list with the searched student (to display the result) and load the data.
                    List<Student> results = new List<Student> { searchedStudent }; // RefreshData method requires a list as an input.
                    RefreshData(results); // Load the search results into the DataGridView.

                    // To allow for immediate editing, select the first row in the DataGridView and pre-fill the input fields
                    if (dgvStudents.Rows.Count > 0)
                    {
                        dgvStudents.Rows[0].Selected = true; // Select the first row (aka the only row)

                        // Pre-fill input fields with the student's data
                        PopulateInputs(0);
                    }
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
            ClearSearch();
        }


        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            // Validate field inputs and exit if any validation fails
            if (!dataHandler.ValidInputs(txtStudentID.Text, txtFirstName.Text, txtLastName.Text, txtAge.Text, cmbCourses.Text, out int studentId, out int age))
            {
                return; // Exit if inputs are not valid
            }

            // Collect and format student data after successful validations
            Student updatedStudent = dataHandler.CollectAndFormatStudentData(txtStudentID.Text, txtFirstName.Text, txtLastName.Text, txtAge.Text, cmbCourses.Text, out studentId, out age);

            // Show a confirmation dialog before updating
            if (!ShowConfirmationDialog("update", studentId, updatedStudent.FirstName, updatedStudent.LastName, age, cmbCourses.Text))
            {
                return; // Exit if the user does not confirm the operation
            }

            // When confirmed, update the student record.
            dataHandler.UpdateStudent(fileHandler, updatedStudent);

            // Refresh the data in the DataGridView
            RefreshData(dataHandler.students);

            // Clear input fields after updating
            ClearInputFields();
            // Reset the Search result and text box.
            ClearSearch();

            // Show success message if update was successful.
            MessageBox.Show("Student information updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Validate that the ID field contains a valid integer, ensuring that a student has been selected.
            if (!int.TryParse(txtStudentID.Text, out int studentId))
            {
                //if a student was not selected, inform the user to select a student.
                MessageBox.Show("Please click on a student to delete.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Find the student details to display in the confirmation dialog before deleting
            Student studentToDelete = dataHandler.students.Find(s => s.StudentId == studentId);
            if (studentToDelete == null)
            {   // If the chosed student does not exist, stop the operation and inform user.
                MessageBox.Show("Student not found.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If successful, use the ShowConfirmationDialog method to confirm deletion
            bool userConfirmed = ShowConfirmationDialog("delete", studentToDelete.StudentId, studentToDelete.FirstName, studentToDelete.LastName, studentToDelete.Age, studentToDelete.Course);

            if (!userConfirmed)
            {
                return; // Exit if the user does not confirm the dialog box
            }

            // If the used confirmed the dialog, delete the student record based on the chosen student ID.
            dataHandler.DeleteStudent(fileHandler, studentId);

            // Clear input fields after deleting.
            ClearInputFields();

            // Reset the Search result and text box.
            ClearSearch();

            // Refresh the DataGridView to show updated data
            RefreshData(dataHandler.students);
        }


        private void btnAddCourse_Click(object  ender, EventArgs e)
        {
            addCourseForm = new AddCourse();

            addCourseForm.UpdateCoursesEvent += UpdateCourseComboBox; // Subscribe a method (updates the course comboBox) to an event that triggers when a new course is added.

            addCourseForm.ShowDialog(); // Opens the AddCourse form as a modal dialog to add a new course
        }


        /// <summary>
        /// All helper methods used in programming the form components.
        /// </summary>

        private void RenameHeaders() // Rename the headers with appropiate titles
        {
            dgvStudents.Columns[0].HeaderText = "Student ID";
            dgvStudents.Columns[1].HeaderText = "First Name";
            dgvStudents.Columns[2].HeaderText = "Last Name";
        }


        // Method to update ComboBox
        public void UpdateCourseComboBox()
        {
            cmbCourses.Items.Clear();  // Clear existing items before adding new one
            cmbCourses.Items.AddRange(fileHandler.ReadCourses().ToArray()); // Add new item
        }


        private void ClearSearch()
        {
            // Reset the Search result and text box.
            RefreshData(dataHandler.students);
            txtIdSearch.Text = "";
        }


        private void RefreshData(List<Student> dataList)
        {
            // Set the binding source to the provided student data list.
            bindingSource.DataSource = dataList;

            // Bind the data source to the DataGridView for display.
            dgvStudents.DataSource = bindingSource;
            dataHandler.students = fileHandler.Read();

            // Update the summary display after loading the data.
            DisplaySummary();
        }


        private void PopulateInputs(int selectedIndexRow)
        {
            // Write the selected row's values to the corresponding text boxes.
            DataGridViewRow row = dgvStudents.Rows[selectedIndexRow];

            txtStudentID.Text = row.Cells[0].Value.ToString();
            txtFirstName.Text = row.Cells[1].Value.ToString();
            txtLastName.Text = row.Cells[2].Value.ToString();
            txtAge.Text = row.Cells[3].Value.ToString();
            cmbCourses.Text = row.Cells[4].Value.ToString();
        }


        private void ClearInputFields()
        {

            txtStudentID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtAge.Clear();
            cmbCourses.SelectedIndex = -1; // Reset combo box selection.
        }


        private void DisplaySummary()
        {
            // if the students file is empty, don't display a summary, as it's not possible.
            if (fileHandler.IsFileEmpty(fileHandler.studentsFilePath))
            {
                lblSummary.Text = $"";
            }
            else
            {
                // Update the label to display the generated summary.
                lblSummary.Text = Program.fileHandler.GenerateSummary(Program.dataHandler.CalculateSummary(Program.fileHandler.Read()));
            }
        }


        // Method to show a confirmation dialog with student details when adding/updating/deleting a student.
        private bool ShowConfirmationDialog(string action, int studentId, string firstName, string lastName, int age, string course)
        {
            string message = $"Are you sure you want to {action} the following student record?\n\n" +
                             $"ID: {studentId}\n" +
                             $"First Name: {firstName}\n" +
                             $"Last Name: {lastName}\n" +
                             $"Age: {age}\n" +
                             $"Course: {course}";

            DialogResult result = MessageBox.Show(message, $"Confirm {action}", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }
    }
}