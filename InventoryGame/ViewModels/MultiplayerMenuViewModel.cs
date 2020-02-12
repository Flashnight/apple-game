using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryGame.ViewModels
{
    public class MultiplayerMenuViewModel : Screen
    {
        private bool _serverIsChecked = true;

        public bool ServerIsChecked
        {
            get => _serverIsChecked;
            set
            {
                if (_serverIsChecked == value)
                    return;
                _serverIsChecked = value;
                NotifyOfPropertyChange(() => ServerIsChecked);
            }
        }

        public bool ClientIsChecked => !_serverIsChecked;

        public void StartMultiplayer()
        {
            TryClose(true);
        }
    }
}
