using AppleGame.Events;
using AppleGame.Models;
using Caliburn.Micro;
using Ninject;
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
    /// <summary>
    /// View model of the inventory.
    /// </summary>
    public class InventoryViewModel : Screen, IHandle<NewGameEvent>
    {
        private IKernel _kernel;

        private IEventAggregator _eventAggregator;

        /// <summary>
        /// View models of the cells.
        /// </summary>
        private InventoryCellViewModel[][] _inventoryCells;

        /// <summary>
        /// View models of the cells.
        /// </summary>
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

        /// <summary>
        /// View model of the inventory.
        /// </summary>
        /// <param name="kernel">IoC kernel.</param>
        /// <param name="eventAggregator">Enabled loosely-coupled publication of and subscription to events.</param>
        public InventoryViewModel(IKernel kernel, IEventAggregator eventAggregator)
        {
            _kernel = kernel;

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        /// <summary>
        /// Handler for new game event. It cleans the inventory.
        /// </summary>
        /// <param name="message">Event's data.</param>
        public void Handle(NewGameEvent message)
        {
            _inventoryCells = new InventoryCellViewModel[3][];

            for (int i = 0; i < 3; i++)
            {
                _inventoryCells[i] = new InventoryCellViewModel[3];
                for (int j = 0; j < 3; j++)
                {
                    _inventoryCells[i][j] = _kernel.Get<InventoryCellViewModel>();
                }
            }

            NotifyOfPropertyChange(() => InventoryCells);
        }
    }
}
