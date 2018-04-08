using BashSoft.Exceptions;
using BashSoft.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.IO.Commands 
{
    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;
        //private IContentComparer judge;
       // private IDatabase repository;
        //private IDirectoryManager inputOutputManager;

        public Command(string input, string[] data)// IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.Input = input;
            this.Data = data;
            //this.judge = judge;
            //this.repository = repository;
            //this.inputOutputManager = inputOutputManager;
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

        //protected IContentComparer Judge
        //{
        //    get { return this.judge; }
        //}

        //protected IDatabase Repository
        //{
        //    get { return this.repository; }
        //}

        //protected IDirectoryManager InputOutputManager
        //{
        //    get { return this.inputOutputManager; }
        //}

        public abstract void Execute();
    }
}
