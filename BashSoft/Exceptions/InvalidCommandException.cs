using System;

namespace BashSoft.Exceptions
{

    public class InvalidCommandException : Exception
    {
        private const string InvalidCommandMessage = "The command '{0}' is invalid";

        public InvalidCommandException()
        {
        }

        public InvalidCommandException(string command)
            : base(string.Format(InvalidCommandMessage, command))
        {
        }
    }
}