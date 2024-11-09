using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282_PRJ
{
    public partial class AddCourse : Form
    {
        FileHandler fileHandler = Program.fileHandler;
        DataHandler dataHandler = Program.dataHandler;

        // Event to trigger a method to refresh course comboBox when a new course is added
        public event Action UpdateCoursesEvent;

        public AddCourse()
        {
            InitializeComponent();
        }


        private void btnAddCourse_Click(object sender, EventArgs e)
        {
            string course = txtAddCourse.Text; // Collect the course name

            if (dataHandler.IsValidCourse(course)) // Validate the course before adding
            {
                fileHandler.AddCourse(course); // Add the valid course to the file
                MessageBox.Show("Course added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); // Show success message.

                // Trigger the event to refresh the courses comboBox
                UpdateCoursesEvent?.Invoke();

                this.FindForm().Close(); // Close the form after successful addition
            }
        }
    }
}