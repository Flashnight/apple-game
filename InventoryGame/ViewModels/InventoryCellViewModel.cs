using InventoryGame.Database;
using InventoryGame.Misc;
using InventoryGame.Models;
using Caliburn.Micro;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using InventoryGame.Commands;
using InventoryGame.Commands.SinglePlayer;
using System.Security.Cryptography.X509Certificates;

namespace InventoryGame.ViewModels
{
    /// <summary>
    /// ViewModel for an invetory cell.
    /// </summary>
    public class InventoryCellViewModel : Screen
    {
        public IExecuteInvoker _invoker;

        /// <summary>
        /// Media player for sounds.
        /// </summary>
        private IMediaPlayerWrapper _mediaPlayerWrapper;

        /// <summary>
        /// Hides DB operations for the inventory cells.
        /// </summary>
        private IInventoryCellDbRepository _inventoryCellRepository;

        /// <summary>
        /// Hides DB operations for items.
        /// </summary>
        private IItemsDbRepository _itemsRepository;

        /// <summary>
        /// Model of an inventory cell.
        /// </summary>
        private InventoryCell _inventoryCell;

        /// <summary>
        /// Model of an inventory cell.
        /// </summary>
        public InventoryCell InventoryCell => _inventoryCell;

        /// <summary>
        /// Item in the cell.
        /// </summary>
        public Item Item => _inventoryCell.Item;

        /// <summary>
        /// Amount of items in the cell.
        /// </summary>
        public int Amount => _inventoryCell.Amount;

        /// <summary>
        /// Destination to item's image in file system.
        /// </summary>
        public string ImageSource => _inventoryCell?.Item?.ImageSource;

        /// <summary>
        /// Row index in the inventory grid.
        /// </summary>
        public int RowIndex => _inventoryCell.Row;

        /// <summary>
        /// Column index in the inventory grid.
        /// </summary>
        public int ColumnIndex => _inventoryCell.Column;

        /// <summary>
        /// ViewModel for an invetory cell.
        /// </summary>
        /// <param name="mediaPlayerWrapper">Media player for sounds.</param>
        public InventoryCellViewModel(
            IExecuteInvoker invoker,
            IMediaPlayerWrapper mediaPlayerWrapper,
            IInventoryCellDbRepository inventoryCellRepository,
            IItemsDbRepository itemsRepository,
            InventoryCell inventoryCell)
        {
            _invoker = invoker;

            _mediaPlayerWrapper = mediaPlayerWrapper;
            
            _inventoryCellRepository = inventoryCellRepository;
            _itemsRepository = itemsRepository;

            _inventoryCell = inventoryCell;
        }

        /// <summary>
        /// Handler for DragOver event. It describes conditions for dropping.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Drag&Drop arguments.</param>
        public void HandleDragOver(InventoryCellViewModel sender, DragEventArgs args)
        {
            if (args.Data.GetDataPresent(typeof(ItemsSourceViewModel)))
            {
                args.Effects = DragDropEffects.Copy;
            }
            else
            {
                args.Effects = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Handler for Drop event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Drag&Drop arguments.</param>
        public async Task HandleDropAsync(Image sender, DragEventArgs args)
        {
            if (args.Data == null)
                return;

            ACommand command = null;
            if (args.Data.GetDataPresent(typeof(ItemsSourceViewModel)))
            {
                ItemsSourceViewModel data = (ItemsSourceViewModel)args.Data.GetData(typeof(ItemsSourceViewModel));
                int itemId = data.Item.Id;

                command = new AddItemFromSourceCommand(this, _inventoryCell, _itemsRepository, _inventoryCellRepository, itemId);
            }
            else if (args.Data.GetDataPresent(typeof(InventoryCellViewModel)))
            {
                InventoryCellViewModel data = (InventoryCellViewModel)args.Data.GetData(typeof(InventoryCellViewModel));

                if (data == this || data?.Item == null)
                {
                    return;
                }

                command = new MoveItemsCommand(data, this, data.InventoryCell, _inventoryCell, _inventoryCellRepository);
            }
            else
            {
                return;
            }

            await _invoker.ExecuteAsync(command);
        }

        /// <summary>
        /// Handler for MouseRightButtonUp. It removes item from the cell.
        /// </summary>
        public async Task HandleMouseRightButtonUpAsync()
        {
            RemoveItemCommand command = new(this, _inventoryCell, _inventoryCellRepository);
            await _invoker.ExecuteAsync(command);
        }

        /// <summary>
        /// Handler for event. It makes drag from the cell.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Mouse event arguments.</param>
        public void HandleMouseLeftButtonDown(InventoryCellViewModel sender, MouseEventArgs args)
        {
            if (Item != null)
            {
                DependencyObject dragSource = args.Source as DependencyObject;

                DragDrop.DoDragDrop(dragSource, this, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// Remove all items from the cell after Drag&Drop to other cell.
        /// </summary>
        public async Task ClearCellAsync()
        {
            ClearCellCommand command = new(this);
            await _invoker.ExecuteAsync(command);
        }
    }
}
