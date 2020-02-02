using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.ViewModels
{
    /// <summary>
    /// Main menu ViewModel.
    /// </summary>
    public class MainMenuViewModel : Screen
    {
        public static readonly MainMenuViewModel MainMenuInstance = new MainMenuViewModel();

        /// <summary>
        /// Runs new game.
        /// </summary>
        public void NewGame()
        {
            this.TryClose(true);
        }

        /// <summary>
        /// Closes the application.
        /// </summary>
        public void Exit()
        {
            this.TryClose(false);
        }
    }
}
