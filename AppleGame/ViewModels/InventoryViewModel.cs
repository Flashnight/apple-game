﻿using AppleGame.Models;
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
        private InventoryCell[,] _inventoryCells;

        public InventoryCell[,] InventoryCells
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
            _inventoryCells = new InventoryCell[3,3];
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
                // handle the files here!
            }
        }
    }
}
