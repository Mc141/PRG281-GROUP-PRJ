using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace PRG282_PRJ
{
    internal class DataHandler
    {
        public List<Student> students; // List to store the student records.
        Logger logger = new Logger();
        string logResult; // Variable to hold the result of the transaction.
        string details; // Variable to hold the details of the transaction.
        string action; // Variable to hold the type of transaction.

        public bool AddStudent(FileHandler fileHandler, Student newStudent)
        {
            // Check if a student with the same ID already exists in the list/file.
            if (IsDuplicateStudent(newStudent))
            {
                // Display an error message to the user if a duplicate student ID is found.
                MessageBox.Show("A student with this ID already exists.", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logResult = "Failed: A student with this ID already exists"; // Log the result as failed if a student with a duplicate Id exists.
                return false; //Exit out of the method
            }

            students.Add(newStudent); // Add the new student to the list if there is no pre-existing id.

            // Add the student to the students file as well
            if (File.Exists(fileHandler.studentsFilePath) == true)
            {
                fileHandler.Append(newStudent); //Append the student to the file.
                logResult = "Student added successfully"; // Log that the result of the transaction was successful.
            }
            else
            {
                // Handle the scenario where the file does not exist.
                MessageBox.Show("The student records file does not exist!", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                logResult = "Failed: The student records file does not exist"; // Log the result of the transaction is failed, because there is no students file.
                fileHandler.Write(students); //If the file doesn't exist, create and write all students to the students file.
            }

            // Get student details for logging the transaction.
            details = BuildAddDetails(newStudent);

            //Get the action for logging the transaction.
            action = "Attempt to Add a new Student";

            // Log the addition.
            logger.LogTransaction(action, details, logResult);

            return true;
        }


        public void DeleteStudent(FileHandler fileHandler, int studentId)
        {
            action = "Attempt to delete a Student record"; //Get the action for logging the transaction.

            // Find and remove the student with the matching ID from the list
            Student studentToDelete = students.Find(s => s.StudentId == studentId);

            // Get student details for logging the transaction.
            details = BuildDeleteDetails(studentToDelete);

            if (studentToDelete != null)
            {
                students.Remove(studentToDelete); // Remove the student from the list
                fileHandler.Write(students); // Write the updated list wihtout the deleted student to the student file
                MessageBox.Show("Student deleted successfully.", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                logResult = "Student deleted successfully"; // Log that the result of the transaction was successful.
            }
            else
            {
                MessageBox.Show("Student not found.", "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logResult = "Failed: Student not found"; // Log that the result of the transaction was failure, because the student was not found.
            }

            // Log the addition.
            logger.LogTransaction(action, details, logResult);
        }


        public void UpdateStudent(FileHandler fileHandler, Student updatedStudent)
        {
            action = "Attempt to update Student details"; //Get the action for logging the transaction.

            // Find the index of the student with the matching ID.
            int index = students.FindIndex(s => s.StudentId == updatedStudent.StudentId);

            Student originalStudent = students[index]; // Get the original student data before updating.

            details = BuildUpdateDetails(originalStudent, updatedStudent);

            if (index >= 0)
            {
                students[index] = updatedStudent; // Update the student data in the list.
                fileHandler.Write(students); // Write all students including the updated student to the file.
                logResult = "Student updated successfully";
            }
            else
            {
                MessageBox.Show("Student not found.", "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                logResult = "Failed: Student not found";
            }

            // Log the addition.
            logger.LogTransaction(action, details, logResult);
        }


        //Method to generate string for logging details wehn updating or deleteing a student.
        private string BuildUpdateDetails(Student original, Student updated)
        {
            // Initialize with the student ID for context.
            var changes = new List<string> { $"Student Id = {original.StudentId}" };

            // Add changes if fields differ between original and updated.
            if (original.FirstName != updated.FirstName) changes.Add($"First Name: {original.FirstName} -> {updated.FirstName}");
            if (original.LastName != updated.LastName) changes.Add($"Last Name: {original.LastName} -> {updated.LastName}");
            if (original.Age != updated.Age) changes.Add($"Age: {original.Age} -> {updated.Age}");
            if (original.Course != updated.Course) changes.Add($"Course: {original.Course} -> {updated.Course}");

            // Return all changes as a comma-separated string.
            return string.Join(", ", changes);
        }


        private string BuildDeleteDetails(Student student)
        {
            return $"Student Id: {student.StudentId}, " +
                   $"First Name: {student.FirstName}, " +
                   $"Last Name: {student.LastName}, " +
                   $"Age: {student.Age}, " +
                   $"Course: {student.Course}";
        }


        ////Method to generate string for logging details when adding a student.
        private string BuildAddDetails(Student student)
        {
            // Format and return the student's details.
            return $"Student Id = {student.StudentId}, FirstName: {student.FirstName}, LastName: {student.LastName}, Age: {student.Age}, Course: {student.Course}";
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


        // Check for duplicate students based on StudentId
        private bool IsDuplicateStudent(Student student)
        {
            return students.Exists(s => s.StudentId == student.StudentId);
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

            return true; // All validations passed
        } 


        // Check if the selected course is valid
        public bool IsValidCourse(string course)
        {
            // Trim the input course to remove any leading or trailing whitespace
            course = course.Trim();

            // Check if the course name is empty
            if (string.IsNullOrEmpty(course))
            {
                MessageBox.Show("Course name cannot be empty. Please enter a valid course name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Return false if the course is empty
            }

            // Check if the course already exists in the courses file
            if (Program.fileHandler.ReadCourses().Contains(course)) // Looks if the chosen course already exists in the course file
            {
                MessageBox.Show("The course already exists. Please enter a new course name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Return false if the course already exists
            }

            // If the course is valid (non-empty and does not exist in the file), return true
            return true;
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