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
    public class InventoryCellViewModel : Screen
    {
        private InventoryCell _inventoryCell;

        public int Amount => _inventoryCell.Amount;

        public string ImageSource => _inventoryCell.Item.ImageSource;

        public InventoryCellViewModel()
        {
            if (_inventoryCell == null)
            {
                var inventoryCell = new InventoryCell
                {
                    Amount = 0,
                    Item = new Item
                    {
                        ImageSource = "",
                        ItemType = ItemType.Apple
                    }
                };

                _inventoryCell = inventoryCell;
            }
        }

        public void HandleDragOver(InventoryCellViewModel sender, DragEventArgs args)
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

        public void HandleDrop(Image sender, DragEventArgs args)
        {
            if (null != args.Data && args.Data.GetDataPresent(typeof(BitmapImage)))
            {
                BitmapImage data = (BitmapImage)args.Data.GetData(typeof(BitmapImage));

                _inventoryCell.Amount++;
                _inventoryCell.Item.ImageSource = data.UriSource.OriginalString;

                NotifyOfPropertyChange(() => Amount);
                NotifyOfPropertyChange(() => ImageSource);
            }
        }
    }
}
