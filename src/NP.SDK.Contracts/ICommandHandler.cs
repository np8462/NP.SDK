using NP.SDK.Contracts.Messages;

namespace NP.SDK.Contracts
{
    /// <summary>
    /// Handles a runtime command.
    /// </summary>
    public interface ICommandHandler
    {
        /// <summary>
        /// Command name.
        /// </summary>
        string Command
        {
            get;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        void Execute(
            RuntimeMessage message);
    }
}