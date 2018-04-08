using BashSoft.Attributes;
using BashSoft.Contracts;
using BashSoft.Exceptions;
using BashSoft.IO.Commands;
using System;
using System.Linq;
using System.Reflection;

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

            Type commandType = Assembly.GetExecutingAssembly()
                                         .GetTypes()
                                         .FirstOrDefault(t => t.GetCustomAttributes(typeof(AliasAttribute))
                                                .Where(at => at.Equals(command)).ToArray().Length > 0);
            if (commandType == null)
            {
                throw new InvalidCommandException(input);
            }

            if (!typeof(IExecutable).IsAssignableFrom(commandType))
            {
                throw new InvalidCommandException(input);
            }

            Type interpreterType = typeof(CommandInterpreter);
            object[] parametersForConstruction = new object[] { input, data };

            Command cmd = (Command)Activator.CreateInstance(commandType, parametersForConstruction);

            FieldInfo[] commandFields = commandType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            FieldInfo[] fieldsOfInterpreter = interpreterType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var commandField in commandFields)
            {
                Attribute attribute = commandField.GetCustomAttribute(typeof(InjectAttribute));
                if (attribute != null)
                {
                    if (fieldsOfInterpreter.Any(f => f.FieldType == commandField.FieldType))
                    {
                        commandField.SetValue(cmd, fieldsOfInterpreter.First(f => f.FieldType == commandField.FieldType).GetValue(this));
                    }
                }
            }

            return cmd;
        }
    }
}
