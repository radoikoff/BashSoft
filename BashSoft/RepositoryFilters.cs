using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft
{
    public class RepositoryFilters
    {
        public static void FilterAndTake(Dictionary<string, List<int>> wantedData, string wantedFilter, int studentsToTake)
        {
            wantedFilter = wantedFilter.ToLower();

            if (wantedFilter == "excellent")
            {
                FilterAndTake(wantedData, x => x >= 5, studentsToTake);
            }
            else if (wantedFilter == "average")
            {
                FilterAndTake(wantedData, x => x < 5 && x >= 3.5, studentsToTake);
            }
            else if (wantedFilter == "poor")
            {
                FilterAndTake(wantedData, X => X < 3.5, studentsToTake);
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidStudentFilter);
            }
        }

        private static void FilterAndTake(Dictionary<string, List<int>> wantedData, Predicate<double> givenFilter, int studentsToTake)
        {
            int counterForPrinted = 0;
            foreach (var userNamePoints in wantedData)
            {
                if (counterForPrinted == studentsToTake)
                {
                    break;
                }

                double averageScore = userNamePoints.Value.Average();
                double percentageOfFullfilment = averageScore / 100;
                double mark = percentageOfFullfilment * 4 + 2;

                if (givenFilter(mark))
                {
                    OutputWriter.PrintStudent(userNamePoints);
                    counterForPrinted++;
                }
            }
        }

        private static double Average(List<int> scoresOnTasks)
        {
            double totalScore = 0;
            foreach (var score in scoresOnTasks)
            {
                totalScore += score;
            }

            double precentageOfAll = totalScore / (scoresOnTasks.Count * 100);
            double mark = precentageOfAll * 4 + 2;

            return mark;
        }
    }
}
