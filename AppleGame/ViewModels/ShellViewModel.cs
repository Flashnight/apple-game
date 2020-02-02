using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AppleGame.ViewModels
{
    /// <summary>
    /// Main ViewModel.
    /// </summary>
    public class ShellViewModel : Screen
    {
        /// <summary>
        /// A service that manages windows.
        /// </summary>
        private IWindowManager _windowManager;

        /// <summary>
        /// View model of the inventory.
        /// </summary>
        private InventoryViewModel _inventory;

        /// <summary>
        /// View model of the items source.
        /// </summary>
        private ItemsSourceViewModel _itemsSource;

        /// <summary>
        /// View model of the inventory.
        /// </summary>
        public InventoryViewModel Inventory
        {
            get => _inventory;
            set
            {
                if (_inventory == value)
                    return;
                _inventory = value;
                NotifyOfPropertyChange(() => Inventory);
            }
        }

        /// <summary>
        /// View model of the items source.
        /// </summary>
        public ItemsSourceViewModel ItemsSource
        {
            get => _itemsSource;
            set
            {
                if (_itemsSource == value)
                    return;
                _itemsSource = value;
                NotifyOfPropertyChange(() => ItemsSource);
            }
        }

        /// <summary>
        /// Main ViewModel.
        /// </summary>
        public ShellViewModel(InventoryViewModel inventoryViewModel,
                              ItemsSourceViewModel itemsSourceViewModel,
                              IWindowManager windowManager)
        {
            _inventory = inventoryViewModel;
            _itemsSource = itemsSourceViewModel;
            _windowManager = windowManager;
        }

        /// <summary>
        /// Called when activating.
        /// </summary>
        protected override void OnActivate()
        {
            // Firstly, show the main menu.
            var mainMenu = MainMenuViewModel.MainMenuInstance;
            Execute.OnUIThread(() => 
            {
                bool? dialogResult = _windowManager.ShowDialog(mainMenu);

                // If user presses "Exit", then the application will be closed.
                if (dialogResult == false)
                    Application.Current.Shutdown();
            });

            base.OnActivate();
        }
    }
}
