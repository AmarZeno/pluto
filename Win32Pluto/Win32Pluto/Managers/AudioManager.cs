using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class AudioManager
    {
        public void turnBGMOn(Audio audioBGM)
        {
            MediaPlayer.Play(audioBGM.backgroundAudio);
            MediaPlayer.IsRepeating = true;
        }
    }
}
