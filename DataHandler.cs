using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282_PRJ
{
    internal class DataHandler
    {
        private FileHandler fileHandler; // Instance of FileHandler to handle file operations.
        private List<Student> students; // List to store the student records.

        // Constructor
        public DataHandler()
        {
            fileHandler = new FileHandler();
            students = fileHandler.Read(); // Load existing students from file.
        }

        public List<Student> GetStudents()
        {
            return students; // Return the list of students.
        }

        public void AddStudent(Student newStudent)
        {
            // Check if a student with the same ID already exists in the list/file.
            if (students.Exists(s => s.StudentId == newStudent.StudentId))
            {
                // Display an error message to the user if a duplicate student ID is found.
                MessageBox.Show("A student with this ID already exists.", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            students.Add(newStudent); // Add the new student to the list.

            if (File.Exists(Program.fileHandler.filePath))
            {
                List<Student> newStudents = new List<Student>(); // Initialize a new list for the new student to append.
                newStudents.Add(newStudent); // Add the newStudent to the newStudents list.
                fileHandler.Append(newStudents); //Append the student to the file.
            }
            else
            {
                fileHandler.Write(students); //If the file doesn't exist, write all students to the file.
            }
        }

        public void UpdateStudent(Student updatedStudent)
        {
            // Find the index of the student with the matching ID.
            int index = students.FindIndex(s => s.StudentId == updatedStudent.StudentId);
            if (index >= 0)
            {
                students[index] = updatedStudent; // Update the student data in the list.
                fileHandler.Write(students); // Write all students to the file.
                MessageBox.Show("Student information updated successfully.", "Update Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Student not found.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Student SearchId(int Id, List<Student> students)
        {
            // Iterate through the list of students to find a match for the given Id.
            foreach (Student student in students)
            {
                // Check if the current student's ID matches the provided Id.
                if (Id == student.StudentId)
                {
                    // If a match is found, return the corresponding student object.
                    return student;
                }
            }
            // If no matching student is found, return null.
            return null;
        }

        public void DeleteStudent(int studentId)
        {
            // Remove the student with the matching ID from the list.
            Student studentToDelete = students.Find(s => s.StudentId == studentId);
            if (studentToDelete != null)
            {
                students.Remove(studentToDelete); // Remove the student from the list.
                fileHandler.Write(students); // Write the updated list to the file.
                MessageBox.Show("Student deleted successfully.", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Student not found.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public string[] CalculateSummary(List<Student> students)
        {
            // Variable to hold the total age of all students.
            int totalAge = 0;
            double averageAge;
            // Get the total number of students.
            int totalStudents = students.Count;

            // Iterate over each student to accumulate their ages.
            foreach (Student student in students)
            {
                totalAge += student.Age; // Add each student's age to totalAge.
            }

            // Calculate the average age; cast to double for accurate division.
            // Round the result to 2 decimal places for better readability.
            averageAge = Math.Round(((double)totalAge / totalStudents), 2);

            // Return an array containing the total number of students and the average age.
            return new string[] { totalStudents.ToString(), averageAge.ToString() };
        }
    }
}