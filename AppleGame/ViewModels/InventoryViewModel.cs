using AppleGame.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AppleGame.ViewModels
{
    public class InventoryViewModel : Screen
    {
        private InventoryCellViewModel[][] _inventoryCells;

        public InventoryCellViewModel[][] InventoryCells
        {
            get => _inventoryCells;
            set
            {
                if (_inventoryCells == value)
                    return;
                _inventoryCells = value;
                NotifyOfPropertyChange(() => InventoryCells);
            }
        }

        public InventoryViewModel()
        {
            _inventoryCells = new InventoryCellViewModel[3][];

            for (int i = 0; i < 3; i++)
            {
                _inventoryCells[i] = new InventoryCellViewModel[3];
                for (int j = 0; j < 3; j++)
                {
                    _inventoryCells[i][j] = new InventoryCellViewModel();
                }
            }

            NotifyOfPropertyChange(() => InventoryCells);
        }
    }
}
