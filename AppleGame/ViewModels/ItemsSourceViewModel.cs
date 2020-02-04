using AppleGame.Database;
using AppleGame.Misc;
using AppleGame.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AppleGame.ViewModels
{
    /// <summary>
    /// ViewModel for the source of apples.
    /// </summary>
    public class ItemsSourceViewModel : Screen
    {
        /// <summary>
        /// Model of an item.
        /// </summary>
        private Item _item;

        /// <summary>
        /// Model of an item.
        /// </summary>
        public Item Item
        {
            get => _item;
            set
            {
                if (_item == value)
                    return;
                _item = value;
                NotifyOfPropertyChange(() => Item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemsRepository">Encapsulates DB operations.</param>
        /// <param name="itemId">Identifier of item.</param>
        public ItemsSourceViewModel(ItemsRepository itemsRepository, int itemId)
        {
            Item = itemsRepository.GetItemById(itemId);
        }

        /// <summary>
        /// Handler for MouseDown. It makes dragging from the source.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Mouse event arguments.</param>
        public void HandleMouseDown(ItemsSourceViewModel sender, MouseEventArgs args)
        {
            DependencyObject dragSource = args.Source as DependencyObject;

            DragDrop.DoDragDrop(dragSource, this, DragDropEffects.Copy);
        }
    }
}
