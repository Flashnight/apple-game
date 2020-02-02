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
        private IItem _item;

        /// <summary>
        /// Model of an item.
        /// </summary>
        public IItem Item
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
        /// Handler for MouseDown. It makes dragging from the source.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">Mouse event arguments.</param>
        public void HandleMouseDown(Image sender, MouseEventArgs args)
        {
            DragDrop.DoDragDrop(sender, sender.Source, DragDropEffects.Copy);
        }
    }
}
