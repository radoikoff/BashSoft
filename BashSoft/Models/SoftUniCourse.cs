﻿using BashSoft.Contracts;
using BashSoft.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Models
{
    public class SoftUniCourse : ICourse
    {
        public const int NumberOfTasksOnExam = 5;
        public const int MaxScoreOnExamTask = 100;

        private string name;
        private Dictionary<string, IStudent> studentsByName;

        public SoftUniCourse(string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, IStudent>();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                name = value;
            }
        }

        public IReadOnlyDictionary<string, IStudent> StudentsByName
        {
            get
            {
                return studentsByName;
            }
        }

        public void EnrollStudent(IStudent student)
        {
            if (this.studentsByName.ContainsKey(student.UserName))
            {
                throw new DuplicateEntryInStructureException(student.UserName, this.name);
            }

            this.studentsByName.Add(student.UserName, student);
        }

        public int CompareTo(ICourse other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
