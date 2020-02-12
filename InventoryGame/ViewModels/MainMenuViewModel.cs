using InventoryGame.Events;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ninject;

namespace InventoryGame.ViewModels
{
    /// <summary>
    /// Main menu ViewModel.
    /// </summary>
    public class MainMenuViewModel : Screen
    {
        /// <summary>
        /// Enabled loosely-coupled publication of and subscription to events.
        /// </summary>
        private IEventAggregator _eventAggregator;

        private IKernel _kernel;

        private IWindowManager _windowManager;

        /// <summary>
        /// Main menu ViewModel.
        /// </summary>
        /// <param name="eventAggregator">Enabled loosely-coupled publication of and subscription to events.</param>
        public MainMenuViewModel(IEventAggregator eventAggregator,
                                 IWindowManager windowManager,
                                 IKernel kernel)
        {
            _eventAggregator = eventAggregator;

            _windowManager = windowManager;

            _kernel = kernel;
        }

        /// <summary>
        /// Runs new game.
        /// </summary>
        public void NewGame()
        {
            _eventAggregator.PublishOnUIThread(new NewGameEvent());

            this.TryClose(true);
        }

        public void ShowMultiplayerMenu()
        {
            var window = _kernel.Get<MultiplayerMenuViewModel>();

            Execute.OnUIThread(() =>
            {
                bool? dialogResult = _windowManager.ShowDialog(window);

                if (dialogResult == true)
                {
                    _eventAggregator.PublishOnUIThread(new StartMultiplayerEvent(window.ServerIsChecked));

                    this.TryClose(true);
                }
            });
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        public void Exit()
        {
            Application.Current.Shutdown();
        }
    }
}
