namespace NP.Services.Commands
{
    public interface ICommand
    {
        string Name { get; }

        object Execute(object parameter);
    }
}