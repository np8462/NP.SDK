using System;
using System.Collections.Generic;

namespace NP.Services.Commands
{
    public class CommandRegistry
    {
        private readonly Dictionary<string, ICommand> _commands =
            new Dictionary<string, ICommand>();

        public void Register(ICommand command)
        {
            if (command == null)
                throw new ArgumentNullException("command");

            _commands[command.Name] = command;
        }

        public object Execute(string name, object parameter)
        {
            ICommand command;

            if (!_commands.TryGetValue(name, out command))
                return null;

            return command.Execute(parameter);
        }
    }
}