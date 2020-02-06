using InventoryGame.Database;
using InventoryGame.Events;
using InventoryGame.Models;
using Caliburn.Micro;
using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace InventoryGame.ViewModels
{
    /// <summary>
    /// View model of the inventory.
    /// </summary>
    public class InventoryViewModel : Screen, IHandle<NewGameEvent>
    {
        /// <summary>
        /// Model for inventory.
        /// </summary>
        private Inventory _inventory;

        /// <summary>
        /// Dependency injection.
        /// </summary>
        private IKernel _kernel;

        /// <summary>
        /// Enables loosely-coupled publication of and subscription to events.
        /// </summary>
        private IEventAggregator _eventAggregator;

        /// <summary>
        /// Hides DB operations for the inventory.
        /// </summary>
        private IInventoryDbRepository _inventoryDbRepository;

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
        /// <param name="eventAggregator">Enables loosely-coupled publication of and subscription to events.</param>
        /// <param name="inventoryDbRepository">Hides DB operations for the inventory.</param>
        public InventoryViewModel(IKernel kernel,
                                  IEventAggregator eventAggregator,
                                  IInventoryDbRepository inventoryDbRepository)
        {
            _kernel = kernel;

            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _inventoryDbRepository = inventoryDbRepository;
        }

        /// <summary>
        /// Handler for new game event. It cleans the inventory.
        /// </summary>
        /// <param name="message">Event's data.</param>
        public void Handle(NewGameEvent message)
        {
            _inventoryCells = new InventoryCellViewModel[3][];

            _inventory = _inventoryDbRepository.CreateNewInventory();

            for (int i = 0; i < 3; i++)
            {
                _inventoryCells[i] = new InventoryCellViewModel[3];
                for (int j = 0; j < 3; j++)
                {
                    InventoryCell cell = _inventory.Cells
                                                   .First(p => p.Row == i && p.Column == j);

                    ConstructorArgument cellArgument = new ConstructorArgument("inventoryCell", cell);

                    _inventoryCells[i][j] = _kernel.Get<InventoryCellViewModel>(cellArgument);
                }
            }

            NotifyOfPropertyChange(() => InventoryCells);
        }
    }
}
