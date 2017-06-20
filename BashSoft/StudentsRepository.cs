using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public static class StudentsRepository
    {
        public static bool isDataInitialized = false;

        private static Dictionary<string, Dictionary<string, List<int>>> studentsByCourse; //courseName, userName, ScoresOnTasks


        public static void InitializeData(string fileName)
        {
            if (!isDataInitialized)
            {
                OutputWriter.WriteMessageOnNewLine("Rading Data...");
                studentsByCourse = new Dictionary<string, Dictionary<string, List<int>>>();
                ReadData(fileName);
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataAlreadyInitialisedException);
            }
        }

        private static void ReadData(string fileName)
        {
            string path = SessionData.currentPath + "\\" + fileName;
            if (File.Exists(path))
            {
                var allInputLines = File.ReadAllLines(path);
                for (int i = 0; i < allInputLines.Length; i++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[i]))
                    {
                        var tokens = allInputLines[i].Split();
                        string course = tokens[0];
                        string student = tokens[1];
                        int mark = int.Parse(tokens[2]);

                        if (!studentsByCourse.ContainsKey(course))
                        {
                            studentsByCourse.Add(course, new Dictionary<string, List<int>>());
                        }

                        if (!studentsByCourse[course].ContainsKey(student))
                        {
                            studentsByCourse[course].Add(student, new List<int>());
                        }

                        studentsByCourse[course][student].Add(mark);
                    }
                }
                isDataInitialized = true;
                OutputWriter.WriteMessageOnNewLine("Data read!");
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
        }

        private static bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (studentsByCourse.ContainsKey(courseName))
                {
                    return true;
                }
                else
                {
                    OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingCourseInDataBase);
                }

            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.DataNotInitializedExceptionMessage);
            }

            return false;
        }

        private static bool IsQueryForStudentPossiblе(string courseName, string studentUserName)
        {
            if (IsQueryForCoursePossible(courseName) && studentsByCourse[courseName].ContainsKey(studentUserName))
            {
                return true;
            }
            else
            {
                OutputWriter.WriteMessageOnNewLine(ExceptionMessages.InexistingStudentInDataBase);
            }
            return false;
        }

        public static void GetStudentScoresFromCourse(string courseName, string userName)
        {
            if (IsQueryForStudentPossiblе(courseName, userName))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, List<int>>(userName, studentsByCourse[courseName][userName]));
            }
        }

        public static void GetAllStudentFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine($"{courseName}:");
                foreach (var studentMarksEntry in studentsByCourse[courseName])
                {
                    OutputWriter.PrintStudent(studentMarksEntry);
                }
            }
        }

    }
}
