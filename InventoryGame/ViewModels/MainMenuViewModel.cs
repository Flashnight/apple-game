using InventoryGame.Events;
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
    /// Main menu ViewModel.
    /// </summary>
    public class MainMenuViewModel : Screen
    {
        /// <summary>
        /// Enabled loosely-coupled publication of and subscription to events.
        /// </summary>
        private IEventAggregator _eventAggregator;

        /// <summary>
        /// Main menu ViewModel.
        /// </summary>
        /// <param name="eventAggregator">Enabled loosely-coupled publication of and subscription to events.</param>
        public MainMenuViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Runs new game.
        /// </summary>
        public async Task NewGameAsync()
        {
            await _eventAggregator.PublishOnUIThreadAsync(new NewGameEvent());

            await TryCloseAsync(true);
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
