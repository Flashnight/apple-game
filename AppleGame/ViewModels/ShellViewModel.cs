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
        private IWindowManager _windowManager;

        /// <summary>
        /// Main ViewModel.
        /// </summary>
        public ShellViewModel(IWindowManager windowManager)
        {
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
