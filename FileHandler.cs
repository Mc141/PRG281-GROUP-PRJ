using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282_PRJ
{
    internal class FileHandler
    {
        // Paths to all data files
        public readonly string studentsFilePath = @"students.txt";
        public readonly string coursesFilePath = @"courses.txt";
        public readonly string summaryFilePath = @"summary.txt";


        // Writes the details from the list of students to the specified file in comma-separated format.
        public void Write(List<Student> students)
        {
            // Attempt to open or create the specified file for writing.
            try
            {
                // Open or create the specified file for writing.
                using (StreamWriter writer = new StreamWriter(studentsFilePath, false, Encoding.UTF8))
                {
                    // Iterate over each student in the list.
                    foreach (Student student in students)
                    {
                        // Convert each student to a string and write it to the file.
                        string record = student.ToString(); // Convert student to a string representation.
                        writer.WriteLine(record); // Write the record to the file.
                    }
                }
            }
            // Handles the appropriate exception using the HandleExeption method.
            catch (Exception ex) // Catch the appropriate exception.
            {
                HandleException(ex); // Call the helper method to handle the exception.
            }
        }


        // Reads student records from the file and returns them as a list of Student objects.
        public List<Student> Read()
        {
            // Initialize an empty list to hold student objects.
            List<Student> students = new List<Student>();

            // Check if the file exists before trying to read.
            if (!File.Exists(studentsFilePath))
            {
                // If the file does not exist, create it.
                using (File.Create(studentsFilePath)) { }

                return students; // Return an empty list if the file is not found.
            }
            // Attempts to read student records from the specified file and save them in the students list.
            try
            {
                // Open the file for reading.
                using (StreamReader reader = new StreamReader(studentsFilePath))
                {
                    string record; // Variable to hold each line read from the file.
                    int lineNumber = 0; // Track the line number for debugging purposes.

                    // Loop through the file until all lines are read.
                    while ((record = reader.ReadLine()) != null)
                    {
                        lineNumber++; // Increment line number for each record.
                        // Split each line into fields based on commas.
                        string[] fields = record.Split(',');

                        // Ensure the record has exactly 5 fields and checks that studentId and Age fields are integer.
                        if (fields.Length == 5 &&
                            int.TryParse(fields[0], out int studentId) && // Parse Student ID.
                            int.TryParse(fields[3], out int age)) // Parse Age.
                        {
                            // Create a new Student object with parsed values.
                            Student newStudent = new Student(studentId, fields[1], fields[2], age, fields[4]);
                            students.Add(newStudent); // Add the student to the list.
                        }
                        else
                        {
                            // Notify user if the record format is invalid.
                            MessageBox.Show($"Invalid record format at line {lineNumber}: {record}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            // Handles the appropriate exception using the HandleExeption method.
            catch (Exception ex) // Catch the appropriate exception.
            {
                HandleException(ex); // Call the helper method to handle the exception.
            }
            // Return the list of students read from the file.
            return students;
        }


        // Appends student details to the specified file without overwriting existing content.
        public void Append(Student student)
        {
            // Attempt to append student record to the specified file.
            try
            {
                // Open the file for appending text at the end.
                using (FileStream fs = new FileStream(studentsFilePath, FileMode.Append))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    writer.WriteLine(student.ToString()); // Write the record to the file.
                }
            }
            // Handles the appropriate exception using the HandleExeption method.
            catch (Exception ex) // Catch the appropriate exception.
            {
                HandleException(ex); // Call the helper method to handle the exception.
            }
        }


        // Check if file is empty, to help with intial values if it is empty.
        public bool IsFileEmpty(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // If the file doesn't exist, it can't contain content, so return true
                return true;
            }

            // Check if the file size is greater than zero
            if (new FileInfo(filePath).Length > 0)
            {
                return false; // File contains content
            }
            return true; // File is empty
        }


        // Generate the age and student count summary
        public string GenerateSummary(string[] summaryData)
        {
            // Construct the lines to write to the summary file.
            string totalStudentLine = $"Number of Students: {summaryData[0]}"; // number of students is stored as the first item in the list
            string averageAgeLine = $"Average Student Age: {summaryData[1]}"; // average age is stored as the second item in the list

            try
            {
                // Write the summary data to the specified file path.
                File.WriteAllLines(summaryFilePath, new string[] { totalStudentLine, averageAgeLine });
            }
            catch (Exception ex) // Catch any exceptions that occur during file operations.
            {
                // Handle the exception using the private helper method.
                HandleException(ex);
            }

            return $"{totalStudentLine}{Environment.NewLine}{averageAgeLine}";
        }


        // method for adding a course
        public void AddCourse(string course)
        {
            // Open the file for appending and write the course, ensuring proper disposal of the stream.
            using (StreamWriter writer = new StreamWriter(coursesFilePath, append: true))
            {
                writer.WriteLine(course);
            }
        }


        // Method to read course data from a file and return it as a list of course names.
        public List<string> ReadCourses()
        {
            // Check if the courses file exists before attempting to read.
            if (!File.Exists(coursesFilePath))
            {
                // If the file does not exist, display an error message to the user.
                MessageBox.Show("Courses file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Return an empty list to indicate that no data could be read.
                return new List<string>();
            }

            // Read all lines from the courses file and convert them to a list of strings.
            // Each line in the file represents a course, and they are added to the list.
            return File.ReadAllLines(coursesFilePath).ToList();
        }


        // Private helper method to handle exceptions and display appropriate messages.
        private void HandleException(Exception ex)
        {
            // Determine the type of exception and show a relevant message.
            if (ex is FileNotFoundException) // Handle file not found errors specifically.
            {
                MessageBox.Show("The file was not found: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ex is UnauthorizedAccessException) // Handles errors when access to the file is denied (Access Control).
            {
                MessageBox.Show("Access to the file is denied: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (ex is IOException)// Handles errors that occur during file write,read or append operations.
            {
                MessageBox.Show("An I/O error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // Catches any unexpected errors that occur during file operations.
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}