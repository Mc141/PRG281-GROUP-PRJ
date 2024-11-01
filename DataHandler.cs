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
    }
}