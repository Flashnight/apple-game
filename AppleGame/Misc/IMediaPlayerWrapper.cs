using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppleGame.Misc
{
    /// <summary>
    /// Wrapper for System.Windows.Media.MediaPlayer.
    /// </summary>
    public interface IMediaPlayerWrapper : IDisposable
    {
        /// <summary>
        /// Plays eating apple sound.
        /// </summary>
        void PlayEatingAppleCrunch();
    }
}
