using InventoryGame.Database;
using InventoryGame.Models;
using InventoryGame.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGame.Commands.SinglePlayer
{
    public class MoveItemsCommand : ACommand
    {
        private readonly InventoryCellViewModel _inventoryCellFromViewModel;
        private readonly InventoryCellViewModel _inventoryCellToViewModel;
        private readonly InventoryCell _inventoryCellFrom;
        private readonly InventoryCell _inventoryCellTo;
        private readonly IInventoryCellDbRepository _inventoryCellRepository;
        private InventoryCell _cellClone;

        public MoveItemsCommand(
            InventoryCellViewModel inventoryCellFromViewModel,
            InventoryCellViewModel inventoryCellToViewModel,
            InventoryCell inventoryCellFrom,
            InventoryCell inventoryCellTo,
            IInventoryCellDbRepository inventoryCellRepository)
        {
            _inventoryCellFromViewModel = inventoryCellFromViewModel;
            _inventoryCellToViewModel = inventoryCellToViewModel;
            _inventoryCellFrom = inventoryCellFrom;
            _inventoryCellTo = inventoryCellTo;
            _inventoryCellRepository = inventoryCellRepository;
        }

        public override async Task DoAsync()
        {
            _cellClone = (InventoryCell)_inventoryCellFromViewModel.InventoryCell.Clone();

            _inventoryCellTo.CopyFrom(_inventoryCellFrom);
            await _inventoryCellFromViewModel.ClearCellAsync();

            NotifyOfViewModelChange();

            await _inventoryCellRepository.UpdateCellAsync(_inventoryCellTo);
        }

        public override async Task UndoAsync()
        {
            throw new NotImplementedException();
        }

        private void NotifyOfViewModelChange()
        {
            _inventoryCellToViewModel.NotifyOfPropertyChange(nameof(_inventoryCellToViewModel.Amount));
            _inventoryCellToViewModel.NotifyOfPropertyChange(nameof(_inventoryCellToViewModel.ImageSource));
        }

    }
}
