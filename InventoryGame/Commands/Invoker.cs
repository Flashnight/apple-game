using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryGame.Commands
{
    public interface IExecuteInvoker
    {
        Task ExecuteAsync(ACommand command);
    }

    public interface IUndoRedoInvoker
    {
        Task UndoAsync();

        Task RedoAsync();
    }

    internal class Invoker : IExecuteInvoker, IUndoRedoInvoker
    {
        private readonly Queue<ACommand> _awaitingCommand;
        private readonly Stack<ACommand> _doneCommands;

        public Invoker()
        {
            _awaitingCommand = new Queue<ACommand>();
            _doneCommands = new Stack<ACommand>();
        }

        public async Task ExecuteAsync(ACommand command)
        {
            if (_awaitingCommand.Count > 0)
                _awaitingCommand.Clear();

            await command.DoAsync();
            _doneCommands.Push(command);
        }

        public async Task UndoAsync()
        {
            var command = _doneCommands.Pop();
            await command.UndoAsync();
            _awaitingCommand.Enqueue(command);
        }

        public async Task RedoAsync() 
        {
            var command = _awaitingCommand.Dequeue();
            await command.DoAsync();
            _doneCommands.Push(command);
        }
    }
}
