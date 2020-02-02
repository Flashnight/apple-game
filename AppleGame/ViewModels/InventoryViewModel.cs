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
        private InventoryCell[][] _inventoryCells;

        public InventoryCell[][] InventoryCells
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
            _inventoryCells = new InventoryCell[3][];

            for (int i = 0; i < 3; i++)
            {
                _inventoryCells[i] = new InventoryCell[3];
                for (int j = 0; j < 3; j++)
                {
                    _inventoryCells[i][j] = new InventoryCell
                    {
                        Item = new Item()
                    };
                }
            }
            
            _inventoryCells[0][0] = new InventoryCell
            {
                Amount = 5,
                Item = new Item
                {
                    ImageSource = "../Resources/Pictures/apple.png",
                    ItemType = ItemType.Apple
                }
            };

            _inventoryCells[1][1] = new InventoryCell
            {
                Amount = 5,
                Item = new Item
                {
                    ImageSource = "../Resources/Pictures/apple.png",
                    ItemType = ItemType.Apple
                }
            };

            NotifyOfPropertyChange(() => InventoryCells);
        }

        public void HandleDragOver(Grid sender, DragEventArgs args)
        {
            if (args.Data.GetDataPresent(typeof(BitmapImage)))
            {
                args.Effects = DragDropEffects.Copy;
            }
            else
            {
                args.Effects = DragDropEffects.None;
            }
        }

        public void HandleDrop(Grid sender, DragEventArgs args)
        {
            if (null != args.Data && args.Data.GetDataPresent(typeof(BitmapImage)))
            {
                BitmapFrame data = (BitmapFrame)args.Data.GetData(typeof(BitmapFrame));
                
                
            }
        }
    }
}
