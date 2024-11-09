using System.Threading.Tasks;

namespace InventoryGame.Commands
{
    public abstract class ACommand
    {
        public abstract Task DoAsync();

        public abstract Task UndoAsync();
    }
}
