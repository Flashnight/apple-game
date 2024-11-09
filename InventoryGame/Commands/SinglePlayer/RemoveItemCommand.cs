using InventoryGame.Misc;
using InventoryGame.Models;
using InventoryGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGame.Commands.SinglePlayer
{
    public class RemoveItemCommand : ACommand
    {
        private readonly InventoryCellViewModel _inventoryCellViewModel;
        private readonly InventoryCell _inventoryCell;
        private readonly IInventoryCellDbRepository _inventoryCellRepository;
        private InventoryCell _cellClone;

        public RemoveItemCommand(
            InventoryCellViewModel inventoryCellViewModel,
            InventoryCell inventoryCell,
            IInventoryCellDbRepository inventoryCellRepository)
        {
            _inventoryCellViewModel = inventoryCellViewModel;
            _inventoryCell = inventoryCell;
            _inventoryCellRepository = inventoryCellRepository;
        }

        public override async Task DoAsync()
        {
            _cellClone = (InventoryCell)_inventoryCellViewModel.InventoryCell.Clone();

            var isItemRemovedSuccessfully = _inventoryCell.TryRemoveItem();
            if (isItemRemovedSuccessfully)
            {
                NotifyOfViewModelChange();
            }

            MediaPlayerWrapper player = new();
            player.PlayEatingAppleCrunch();

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
