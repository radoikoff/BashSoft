using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.IO.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public class CommandInterpreter : IInterpreter
    {
        private IContentComparer judge;
        private IDatabase repository;
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        public void InterpredCommand(string input)
        {
            string[] data = input.Split();
            string commandName = data[0].ToLower();

            try
            {
                IExecutable command = this.ParseCommand(input, commandName, data);
                command.Execute();
            }
            catch (Exception e)
            {
                OutputWriter.DisplayException(e.Message);
            }
        }

        private IExecutable ParseCommand(string input, string command, string[] data)
        {
            switch (command)
            {
                case "open":
                    return new OpenFileCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "mkdir":
                    return new MakeDirectoryCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "ls":
                    return new TraverseFoldersCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "cmp":
                    return new CompareFilesCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "cdrel":
                    return new ChangeRelativePathCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "cdabs":
                    return new ChangeAbsolutePathCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "readdb":
                    return new ReadDatabaseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "dropdb":
                    return new DropDatabaseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "help":
                    return new GetHelpCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "show":
                    return new ShowCourseCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "filter":
                    return new PrintFilteredStudentsCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "order":
                    return new PrintOrderedStudentsCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                case "display":
                    return new DisplayCommand(input, data, this.judge, this.repository, this.inputOutputManager);
                //case "decorder":
                //    //not needed. Order takes both asc/desc.
                //    break;
                //case "download":
                //    break;
                //case "downloadasynch":
                //    break;
                default:
                    throw new InvalidCommandException(input);
            }
        }

        //Old methods used before Command classes
        //private void TryDropDb(string input, string[] data)
        //{
        //    if (data.Length != 1)
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //        return;
        //    }

        //    this.repository.UnloadData();
        //    OutputWriter.WriteMessageOnNewLine(UserMessages.DbSucessfullyUnloaded);
        //}

        //private void TryShowWantedData(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string courseName = data[1];
        //        this.repository.GetAllStudentFromCourse(courseName);
        //    }
        //    else if (data.Length == 3)
        //    {
        //        string courseName = data[1];
        //        string userName = data[2];
        //        this.repository.GetStudentScoresFromCourse(courseName, userName);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private void TryGetHelp(string input, string[] data)
        //{
        //    OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "make directory - mkdir: path "));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "traverse directory - ls: depth "));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "comparing files - cmp: path1 path2"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDirREl:relative path"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "change directory - changeDir:absolute path"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "read students data base - readDb: path"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "filter {courseName} excelent/average/poor  take 2/5/all students - filterExcelent (the output is written on the console)"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "order increasing students - order {courseName} ascending/descending take 20/10/all (the output is written on the console)"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file - download: path of file (saved in current directory)"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "download file asinchronously - downloadAsynch: path of file (save in the current directory)"));
        //    OutputWriter.WriteMessageOnNewLine(string.Format("|{0, -98}|", "get help – help"));
        //    OutputWriter.WriteMessageOnNewLine($"{new string('_', 100)}");
        //    OutputWriter.WriteEmptyLine();
        //}

        //private void TryReadDatabaseFromFile(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string fileName = data[1];
        //        this.repository.LoadData(fileName);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private void TryChangePathAbsolute(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string absolutePath = data[1];
        //        this.inputOutputManager.ChangeCurrentDirectoryAbsolute(absolutePath);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private void TryChangePathRelatively(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string relativePath = data[1];
        //        this.inputOutputManager.ChangeCurrentDirectoryRelative(relativePath);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private void TryCompareFiles(string input, string[] data)
        //{
        //    if (data.Length == 3)
        //    {
        //        string firstPath = data[1];
        //        string secondPath = data[2];
        //        this.judge.CompareContent(firstPath, secondPath);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private void TryTraverseFolder(string input, string[] data)
        //{
        //    if (data.Length == 1)
        //    {
        //        this.inputOutputManager.TraverseDirectory(0);
        //    }
        //    else if (data.Length == 2)
        //    {
        //        int depth;
        //        bool hasParsed = int.TryParse(data[1], out depth);
        //        if (hasParsed)
        //        {
        //            this.inputOutputManager.TraverseDirectory(depth);
        //        }
        //        else
        //        {
        //            OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
        //        }
        //    }
        //}

        //private void TryCreateDirectory(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string folderName = data[1];
        //        this.inputOutputManager.CreateDirectoryInCurrentFolder(folderName);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private void TryOpenFile(string input, string[] data)
        //{
        //    if (data.Length == 2)
        //    {
        //        string fileName = data[1];
        //        Process.Start(SessionData.currentPath + "\\" + fileName);
        //    }
        //    else
        //    {
        //        DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private void DisplayInvalidCommandMessage(string input)
        //{
        //    OutputWriter.WriteMessageOnNewLine($"The command '{input}' is invalid");
        //}

        //private void TryFilterAndTake(string input, string[] data)
        //{
        //    if (data.Length == 5)
        //    {
        //        string courseName = data[1];
        //        string filter = data[2].ToLower();
        //        string takeCommand = data[3].ToLower();
        //        string takeQuantity = data[4].ToLower();

        //        this.TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity, string courseName, string filter)
        //{
        //    if (takeCommand == "take")
        //    {
        //        if (takeQuantity == "all")
        //        {
        //            this.repository.FilterAndTake(courseName, filter);
        //        }
        //        else
        //        {
        //            int studentsToTake;
        //            bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
        //            if (hasParsed)
        //            {
        //                this.repository.FilterAndTake(courseName, filter, studentsToTake);
        //            }
        //            else
        //            {
        //                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //            }
        //        }
        //    }
        //}

        //private void TryOrderAndTake(string input, string[] data)
        //{
        //    if (data.Length == 5)
        //    {
        //        string courseName = data[1];
        //        string order = data[2].ToLower();
        //        string takeCommand = data[3].ToLower();
        //        string takeQuantity = data[4].ToLower();

        //        this.TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, order); //order courseName ascending/descending take 3/26/52/all
        //    }
        //    else
        //    {
        //        this.DisplayInvalidCommandMessage(input);
        //    }
        //}

        //private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity, string courseName, string order)
        //{
        //    if (takeCommand == "take")
        //    {
        //        if (takeQuantity == "all")
        //        {
        //            this.repository.OrderAndTake(courseName, order);
        //        }
        //        else
        //        {
        //            int studentsToTake;
        //            bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
        //            if (hasParsed)
        //            {
        //                this.repository.OrderAndTake(courseName, order, studentsToTake);
        //            }
        //            else
        //            {
        //                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
        //            }
        //        }
        //    }
        //}
    }
}
