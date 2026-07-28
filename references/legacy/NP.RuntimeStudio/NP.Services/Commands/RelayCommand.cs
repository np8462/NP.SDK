using System;

namespace NP.Services.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Func<object, object> _execute;

        public string Name { get; private set; }

        public RelayCommand(string name,
                            Func<object, object> execute)
        {
            Name = name;
            _execute = execute;
        }

        public object Execute(object parameter)
        {
            return _execute(parameter);
        }
    }
}