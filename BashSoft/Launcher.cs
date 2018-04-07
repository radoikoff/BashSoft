using BashSoft.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public class Launcher
    {
        public static void Main()
        {
            //IOManager.TraverseDirectory(10);

            //StudentsRepository.InitializeData();
            //StudentsRepository.GetAllStudentFromCourse("Unity");
            //StudentsRepository.GetStudentScoresFromCourse("Unity", "Ivan");

            //Tester.CompareContent(@"C:\Users\vradoyko\Desktop\user\test2.txt", @"C:\Users\vradoyko\Desktop\user\test3.txt");

            //IOManager.CreateDirectoryInCurrentFolder("vasko");
            //IOManager.ChangeCurrentDirectoryAbsolute(@"C:\windows");
            //IOManager.ChangeCurrentDirectoryRelative("..");
            //IOManager.TraverseDirectory(50);

            IContentComparer tester = new Tester();
            IDirectoryManager ioManager = new IOManager();
            IDatabase repo = new StudentsRepository(new RepositoryFilter(), new RepositorySorter());

            IInterpreter cmdInterpreter = new CommandInterpreter(tester, repo, ioManager);
            IReader reader = new InputReader(cmdInterpreter);

            reader.StartReadingCommands();

        }
    }
}
