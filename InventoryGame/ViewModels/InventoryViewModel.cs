using InventoryGame.Database;
using InventoryGame.Events;
using InventoryGame.Models;
using Caliburn.Micro;
using Ninject;
using Ninject.Parameters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

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
        private List<InventoryCellViewModel> _inventoryCells;

        /// <summary>
        /// View models of the cells.
        /// </summary>
        public List<InventoryCellViewModel> InventoryCells
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
            _eventAggregator.SubscribeOnPublishedThread(this);

            _inventoryDbRepository = inventoryDbRepository;
        }

        /// <summary>
        /// Handler for new game event. It cleans the inventory.
        /// </summary>
        /// <param name="message">Event's data.</param>
        public async Task HandleAsync(NewGameEvent message, CancellationToken cancellationToken)
        {
            _inventory = await _inventoryDbRepository.CreateNewInventoryAsync();
            _inventoryCells = new List<InventoryCellViewModel>();

            foreach (var cell in _inventory.Cells)
            {
                ConstructorArgument cellArgument = new ConstructorArgument("inventoryCell", cell);

                var cellViewModel = _kernel.Get<InventoryCellViewModel>(cellArgument);

                _inventoryCells.Add(cellViewModel);
            }

            NotifyOfPropertyChange(() => InventoryCells);
        }
    }
}
