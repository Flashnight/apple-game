using AppleGame.Misc;
using AppleGame.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AppleGame.ViewModels
{
    /// <summary>
    /// ViewModel for an invetory cell.
    /// </summary>
    public class InventoryCellViewModel : Screen
    {
        /// <summary>
        /// Media player for sounds.
        /// </summary>
        private IMediaPlayerWrapper _mediaPlayerWrapper;

        /// <summary>
        /// Model of an inventory cell.
        /// </summary>
        private InventoryCell _inventoryCell;

        /// <summary>
        /// Amount of items in the cell.
        /// </summary>
        public int Amount => _inventoryCell.Amount;

        /// <summary>
        /// Destination to item's image in file system.
        /// </summary>
        public string ImageSource => _inventoryCell.Item.ImageSource;

        /// <summary>
        /// ViewModel for an invetory cell.
        /// </summary>
        /// <param name="mediaPlayerWrapper">Media player for sounds.</param>
        public InventoryCellViewModel(IMediaPlayerWrapper mediaPlayerWrapper)
        {
            _mediaPlayerWrapper = mediaPlayerWrapper;

            if (_inventoryCell == null)
            {
                var inventoryCell = new InventoryCell
                {
                    Amount = 0,
                    Item = new Item
                    {
                        ImageSource = null,
                        ItemType = ItemType.Apple
                    }
                };

                _inventoryCell = inventoryCell;
            }
        }

        /// <summary>
        /// Handler for DragOver event. It describes conditions for dropping.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Drag&Drop arguments.</param>
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

        /// <summary>
        /// Handler for Drop event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Drag&Drop arguments.</param>
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
            else if (null != args.Data && args.Data.GetDataPresent(typeof(InventoryCellViewModel)))
            {
                InventoryCellViewModel data = (InventoryCellViewModel)args.Data.GetData(typeof(InventoryCellViewModel));

                if (_inventoryCell.Amount == 0)
                {
                    _inventoryCell.Item.ImageSource = data.ImageSource;
                    NotifyOfPropertyChange(() => ImageSource);
                }

                _inventoryCell.Amount += data.Amount;
                NotifyOfPropertyChange(() => Amount);

                data.ClearCell();
            }
        }

        /// <summary>
        /// Handler for MouseRightButtonUp. It removes item from the cell.
        /// </summary>
        public void HandleMouseRightButtonUp()
        {
            if (_inventoryCell.Amount == 0)
            {
                return;
            }

            _inventoryCell.Amount--;

            if (_inventoryCell.Amount == 0)
            {
                _inventoryCell.Item.ImageSource = null;
            }

            NotifyOfPropertyChange(() => Amount);
            NotifyOfPropertyChange(() => ImageSource);

            MediaPlayerWrapper player = new MediaPlayerWrapper();
            player.PlayEatingAppleCrunch();
        }

        /// <summary>
        /// Handler for event. It makes drag from the cell.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Mouse event arguments.</param>
        public void HandleMouseLeftButtonDown(InventoryCellViewModel sender, MouseEventArgs args)
        {
            DependencyObject dragSource = args.Source as DependencyObject;

            DragDrop.DoDragDrop(dragSource, this, DragDropEffects.Move);
        }

        /// <summary>
        /// Remove all items from the cell after Drag&Drop to other cell.
        /// </summary>
        public void ClearCell()
        {
            _inventoryCell.Amount = 0;
            _inventoryCell.Item.ImageSource = null;

            NotifyOfPropertyChange(() => Amount);
            NotifyOfPropertyChange(() => ImageSource);
        }
    }
}
