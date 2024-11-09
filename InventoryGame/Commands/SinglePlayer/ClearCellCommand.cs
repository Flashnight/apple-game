using InventoryGame.Models;
using InventoryGame.ViewModels;
using System;
using System.Threading.Tasks;

namespace InventoryGame.Commands.SinglePlayer
{
    internal class ClearCellCommand : ACommand
    {
        private readonly InventoryCellViewModel _inventoryCellViewModel;
        private InventoryCell _cellClone;

        public ClearCellCommand(InventoryCellViewModel inventoryCellViewModel)
        {
            _inventoryCellViewModel = inventoryCellViewModel;
        }

        public override async Task DoAsync()
        {
            var cell = _inventoryCellViewModel.InventoryCell;

            _cellClone = (InventoryCell)cell.Clone();
            cell.Clear();

            NotifyOfViewModelChange();
        }

        public override async Task UndoAsync()
        {
            var cell = _inventoryCellViewModel.InventoryCell;
            cell.RestoreFromClone(_cellClone);

            NotifyOfViewModelChange();
        }

        private void NotifyOfViewModelChange()
        {
            _inventoryCellViewModel.NotifyOfPropertyChange(nameof(_inventoryCellViewModel.Amount));
            _inventoryCellViewModel.NotifyOfPropertyChange(nameof(_inventoryCellViewModel.ImageSource));
        }
    }
}
