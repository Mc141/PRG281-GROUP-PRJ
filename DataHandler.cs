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

            if (fileHandler.FileExists() == true)
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

        public Student SearchId(int Id, List<Student> students)
        {
            foreach (Student student in students)
            {
                if (Id == student.StudentId)
                {
                    return student;
                }
            }
            return null;
        }
    }
}