namespace BashSoft.Contracts
{
    public interface IRequester
    {
        void GetStudentScoresFromCourse(string courseName, string userName);

        void GetAllStudentFromCourse(string courseName);
    }
}