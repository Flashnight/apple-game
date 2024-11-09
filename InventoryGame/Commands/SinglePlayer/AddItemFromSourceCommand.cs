using InventoryGame.Database;
using InventoryGame.Models;
using InventoryGame.ViewModels;
using System.Threading.Tasks;

namespace InventoryGame.Commands.SinglePlayer
{
    public class AddItemFromSourceCommand : ACommand
    {
        private readonly InventoryCellViewModel _inventoryCellViewModel;
        private readonly InventoryCell _inventoryCell;
        private readonly IItemsDbRepository _itemsRepository;
        private readonly IInventoryCellDbRepository _inventoryCellRepository;
        private readonly int _itemId;
        private InventoryCell _cellClone;

        public AddItemFromSourceCommand(
            InventoryCellViewModel inventoryCellViewModel,
            InventoryCell inventoryCell,
            IItemsDbRepository itemsRepository,
            IInventoryCellDbRepository inventoryCellRepository,
            int itemId)
        {
            _inventoryCellViewModel = inventoryCellViewModel;
            _inventoryCell = inventoryCell;
            _itemsRepository = itemsRepository;
            _inventoryCellRepository = inventoryCellRepository;
            _itemId = itemId;
        }

        public override async Task DoAsync()
        {
            _cellClone = (InventoryCell)_inventoryCellViewModel.InventoryCell.Clone();

            var item = await _itemsRepository.GetItemByIdAsync(_itemId);
            _inventoryCell.AddItem(item);

            NotifyOfViewModelChange();

            await _inventoryCellRepository.UpdateCellAsync(_inventoryCell);
        }

        public override async Task UndoAsync()
        {
            var cell = _inventoryCellViewModel.InventoryCell;
            cell.RestoreFromClone(_cellClone);

            NotifyOfViewModelChange();

            await _inventoryCellRepository.UpdateCellAsync(_inventoryCell);
        }

        private void NotifyOfViewModelChange()
        {
            _inventoryCellViewModel.NotifyOfPropertyChange(nameof(_inventoryCellViewModel.Amount));
            _inventoryCellViewModel.NotifyOfPropertyChange(nameof(_inventoryCellViewModel.ImageSource));
        }
    }
}
