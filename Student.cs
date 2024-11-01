﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_PRJ
{
    internal class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Course { get; set; }

        public Student(int studentId, string firstName, string lastName, int age, string course)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Course = course;
        }

        public override string ToString()
        {
            return $"{StudentId},{FirstName},{LastName},{Age},{Course}";
        }
    }
}
