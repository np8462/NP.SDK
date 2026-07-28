using System;
using System.Collections.Generic;
using NP.Core.Models;

namespace NP.Services.Commands
{
    public class CommandManager
    {
        private List<CommandModel> _commands;

        public event Action<CommandModel> CommandAdded;

        public CommandManager()
        {
            _commands =
                new List<CommandModel>();
        }

        public List<CommandModel> Commands
        {
            get
            {
                return _commands;
            }
        }

        public void Add(
            string source,
            string command,
            string details)
        {
            CommandModel item =
                new CommandModel();

            item.Time = DateTime.Now;
            item.Source = source;
            item.Command = command;
            item.Details = details;

            _commands.Add(item);

            if (CommandAdded != null)
            {
                CommandAdded(item);
            }
        }

        public void Clear()
        {
            _commands.Clear();
        }
    }
}