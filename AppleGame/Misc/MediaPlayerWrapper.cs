using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AppleGame.Misc
{
    public class MediaPlayerWrapper: IMediaPlayerWrapper
    {
        private MediaPlayer _player;

        public MediaPlayerWrapper()
        {
            _player = new MediaPlayer();
        }

        public void Dispose()
        {
            _player.Close();
        }

        public void PlayEatingAppleCrunch()
        {
            _player.Open(new Uri("Resources//Sounds//eating_apple_crunch.mp3", UriKind.Relative));
            _player.Play();
            
        }
    }
}
