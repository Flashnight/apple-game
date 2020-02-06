using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryGame.ViewModels
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
        /// View model for main menu.
        /// </summary>
        private MainMenuViewModel _mainMenuViewModel;

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
        /// <param name="inventoryViewModel">View model of the inventory.</param>
        /// <param name="itemsSourceViewModel">View model of the items source.</param>
        /// <param name="mainMenuViewModel">View model for main menu.</param>
        /// <param name="windowManager">A service that manages windows.</param>
        public ShellViewModel(InventoryViewModel inventoryViewModel,
                              ItemsSourceViewModel itemsSourceViewModel,
                              MainMenuViewModel mainMenuViewModel,
                              IWindowManager windowManager)
        {
            _inventory = inventoryViewModel;
            _itemsSource = itemsSourceViewModel;
            _mainMenuViewModel = mainMenuViewModel;
            _windowManager = windowManager;
        }

        /// <summary>
        /// It's called when button "New Game" is pressed.
        /// </summary>
        public void ShowMainMenu()
        {
            Execute.OnUIThread(() =>
            {
                _windowManager.ShowDialog(_mainMenuViewModel);
            });
        }

        /// <summary>
        /// Called when activating.
        /// </summary>
        protected override void OnActivate()
        {
            // Firstly, show the main menu.
            Execute.OnUIThread(() =>
            {
                var dialogResult = _windowManager.ShowDialog(_mainMenuViewModel);

                if (dialogResult == false)
                {
                    Application.Current.Shutdown();
                }
            });

            base.OnActivate();
        }
    }
}
