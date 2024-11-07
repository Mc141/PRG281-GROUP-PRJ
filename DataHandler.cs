using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282_PRJ
{
    internal class DataHandler
    {
        public List<Student> students; // List to store the student records.

        public bool AddStudent(FileHandler fileHandler, Student newStudent)
        {
            // Check if a student with the same ID already exists in the list/file.
            if (IsDuplicateStudent(newStudent))
            {
                // Display an error message to the user if a duplicate student ID is found.
                MessageBox.Show("A student with this ID already exists.", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; //Exit out of the method
            }

            students.Add(newStudent); // Add the new student to the list if there is no pre-existing id.

            // Add the student to the students file as well
            if (File.Exists(fileHandler.studentsFilePath) == true)
            {
                fileHandler.Append(newStudent); //Append the student to the file.
            }
            else
            {
                // Handle the scenario where the file does not exist.
                MessageBox.Show("The student records file does not exist!", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                fileHandler.Write(students); //If the file doesn't exist, create and write all students to the students file.
            }
            return true;
        }


        // Check for duplicate students based on StudentId
        private bool IsDuplicateStudent(Student student)
        {
            return students.Exists(s => s.StudentId == student.StudentId);
        }


        public void DeleteStudent(FileHandler fileHandler, int studentId)
        {
            // Find and remove the student with the matching ID from the list
            Student studentToDelete = students.Find(s => s.StudentId == studentId);
            if (studentToDelete != null)
            {
                students.Remove(studentToDelete); // Remove the student from the list
                fileHandler.Write(students); // Write the updated list wihtout the deleted student to the student file
                MessageBox.Show("Student deleted successfully.", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Student not found.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void UpdateStudent(FileHandler fileHandler, Student updatedStudent)
        {
            // Find the index of the student with the matching ID.
            int index = students.FindIndex(s => s.StudentId == updatedStudent.StudentId);
            if (index >= 0)
            {
                students[index] = updatedStudent; // Update the student data in the list.
                fileHandler.Write(students); // Write all students including the updated student to the file.  
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


        // Format the name: make lowercase, trim whitespace, and capitalize the first letter.
        private string FormatName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null; // Handle empty or whitespace name gracefully.

            name = name.Trim(); // Remove leading and trailing spaces.

            // Handle single-character strings safely.
            if (name.Length == 1)
            {
                return char.ToUpper(name[0]).ToString();
            }

            // Capitalize the first letter and append the rest of the lowercase string.
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }


        // Method to collect and format student data
        public Student CollectAndFormatStudentData(string studentIdText, string firstNameText, string lastNameText, string ageText, string course, out int studentId, out int age)
        {
            // Assume ValidInputs has already been called to ensure data is valid
            studentId = int.Parse(studentIdText); // parse as correct data type
            age = int.Parse(ageText);   // parse as correct data type

            string formattedFirstName = FormatName(firstNameText); //format properly
            string formattedLastName = FormatName(lastNameText); //format properly

            return new Student(studentId, formattedFirstName, formattedLastName, age, course); // return formatted student
        }


        public bool ValidInputs(string studentIdText, string firstName, string lastName, string ageText, string course, out int studentId, out int age)
        {
            // Initialize output parameters
            studentId = 0;
            age = 0;

            // Validate input fields for null content or white space
            if (string.IsNullOrWhiteSpace(studentIdText) ||
                string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(ageText) ||
                string.IsNullOrWhiteSpace(course))
            {
                MessageBox.Show("Please fill in all fields.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Exit the method if validation fails
            }

            // Parse the Student ID and Age to integers
            if (!int.TryParse(ageText, out age) || age < 15 || age > 100)
            {
                MessageBox.Show("Age must be a valid integer and between between 15 and 100.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Exit the method if parsing fails
            }

            // Validate that First Name and Last Name only contains letters
            if (!IsTextOnly(firstName, "First name") || !IsTextOnly(lastName, "Last name"))
            {
                return false; // Exit if name validation fails
            }

            // Validate the selected course
            if (!IsValidCourse(course))
            {
                return false; // Exit if course validation fails
            }
            return true; // All validations passed
        }


        // Check if the selected course is valid
        private bool IsValidCourse(string course)
        {
            if (!Program.fileHandler.ReadCourses().Contains(course)) // Looks if the chosen course is not the coures file
            {
                MessageBox.Show("The chosen course does not exist.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; //return false if the course does not exist.
            }
            return true; // return true if the selected course is in the course file.
        }


        // Check if the input contains only letters
        private bool IsTextOnly(string input, string fieldName)
        {
            // Define a regular expression pattern that matches strings containing only uppercase or lowercase letters (a-z, A-Z).
            Regex textOnlyPattern = new Regex("^[a-zA-Z]+$");

            // Check if the input matches the regular expression pattern.
            if (!textOnlyPattern.IsMatch(input))
            {
                // If the input does not match, show a message box with an error message specific to the fieldName.
                // Inform the user that the input must contain only letters.
                MessageBox.Show($"{fieldName} must contain only letters.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Return false, indicating that the input is invalid.
                return false;
            }
            // If the input matches the pattern (only letters), return true, indicating that the input is valid.
            return true;
        }

    }
}