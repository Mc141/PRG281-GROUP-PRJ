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
        private string filePath = "students.txt"; // Path of the file where student records will be stored.

        //Checks if the specified file exists at the given file path.
        public bool FileExists()
        {
            if (File.Exists(filePath))
            {
                return true; // Return true if the file is found.
            }
            else
            {
                return false; // Return false if the file is not found.
            }
        }

        // Writes the details from the list of students to the specified file in comma-separated format.
        public void Write(List<Student> students)
        {
            // Attempt to open or create the specified file for writing.
            try
            {
                // Open or create the specified file for writing.
                using (StreamWriter writer = new StreamWriter(filePath))
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
            if (!File.Exists(filePath))
            {
                // If the file does not exist, create it.
                using (File.Create(filePath))
                {
                    // This will create an empty file.
                }
                return students; // Return an empty list if the file is not found.
            }
            // Attempts to read student records from the specified file and save them in the students list.
            try
            {
                // Open the file for reading.
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string record; // Variable to hold each line read from the file.

                    // Loop through the file until all lines are read.
                    while ((record = reader.ReadLine()) != null)
                    {
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
                            MessageBox.Show($"Invalid record format: {record}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        public void Append(List<Student> students)
        {
            // Attempt to append student records to the specified file.
            try
            {
                // Open the file for appending text at the end.
                using (FileStream fs = new FileStream(filePath, FileMode.Append))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    string record; // Variable to hold each student's string representation.
                    // Iterate over each student in the list.
                    foreach (Student student in students)
                    {
                        record = student.ToString(); // Convert student to a string representation.
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