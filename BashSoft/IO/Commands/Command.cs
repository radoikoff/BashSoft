using BashSoft.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.IO.Commands
{
    public abstract class Command
    {
        private string input;
        private string[] data;
        private Tester judge;
        private StudentsRepository repository;
        private IOManager inputOutputManager;

        public Command(string input, string[] data, Tester judge, StudentsRepository repository, IOManager inputOutputManager)
        {
            this.Input = input;
            this.Data = data;
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;
        }

        protected string Input
        {
            get { return input; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                input = value;
            }
        }

        protected string[] Data
        {
            get { return data; }

            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }
                data = value;
            }
        }

        protected Tester Judge
        {
            get { return this.judge; }
        }

        protected StudentsRepository Repository
        {
            get { return this.repository; }
        }

        protected IOManager InputOutputManager
        {
            get { return this.inputOutputManager; }
        }

        public abstract void Execute();
    }
}
