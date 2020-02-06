using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace InventoryGame.Misc
{
    /// <summary>
    /// Wrapper for System.Windows.Media.MediaPlayer.
    /// </summary>
    public class MediaPlayerWrapper: IMediaPlayerWrapper
    {
        /// <summary>
        /// Media player.
        /// </summary>
        private MediaPlayer _player;

        /// <summary>
        /// Wrapper for System.Windows.Media.MediaPlayer.
        /// </summary>
        public MediaPlayerWrapper()
        {
            _player = new MediaPlayer();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _player.Close();
        }

        /// <summary>
        /// Plays eating apple sound.
        /// </summary>
        public void PlayEatingAppleCrunch()
        {
            _player.Open(new Uri("Resources//Sounds//eating_apple_crunch.mp3", UriKind.Relative));
            _player.Play();
            
        }
    }
}
