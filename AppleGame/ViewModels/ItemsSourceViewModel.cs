using AppleGame.Models;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppleGame.ViewModels
{
    public class ItemsSourceViewModel : Screen
    {
        private IItem _item;

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

        public ItemsSourceViewModel()
        {

        }

        public void HandleMouseDown(Image sender, DragEventArgs args)
        {
            DragDrop.DoDragDrop(sender, sender.Source, DragDropEffects.Copy);
        }
    }
}
