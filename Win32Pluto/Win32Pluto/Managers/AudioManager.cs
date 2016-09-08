using Microsoft.Xna.Framework.Audio;
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
        SoundEffectInstance orbitSoundEffectInstance;
        SoundEffectInstance decreaseSoundEffectInstance;
        SoundEffectInstance increaseSoundEffectInstance;
       // public SoundEffect orbitHoverEffect;
        public void turnBGMOn(Audio audioBGM)
        {
            MediaPlayer.Play(audioBGM.backgroundAudio);
            MediaPlayer.IsRepeating = true;
        }

        public void pauseBGM() {
            MediaPlayer.Pause();
        }

        public void resumeBGM() {
            MediaPlayer.Resume();
        }

        //public void loadOrbitSound(Audio orbitAudio)
        //{
        //    MediaPlayer.Play(orbitAudio.backgroundAudio);
        //    MediaPlayer.IsRepeating = true;
        //}

        //public void pauseOrbitSound()
        //{
        //    MediaPlayer.Pause();
        //}

        //public void resumeOrbitSound()
        //{
        //    MediaPlayer.Resume();
        //}

        public void loadOrbitHoverSound(SoundEffect orbitHoverEffect) {
            orbitSoundEffectInstance = orbitHoverEffect.CreateInstance();
        }

        public void playOrbitHoverSound() {
            orbitSoundEffectInstance.Play();
        }

        public void loadDecreaseSound(SoundEffect decreaseEffect) {
            decreaseSoundEffectInstance = decreaseEffect.CreateInstance();
        }

        public void playDecreaseSound() {
            decreaseSoundEffectInstance.Play();
        }

        public void loadIncreaseSound(SoundEffect increaseEffect) {
            increaseSoundEffectInstance = increaseEffect.CreateInstance();
        }

        public void playIncreaseSound() {
            increaseSoundEffectInstance.Play();
        }
    }
}
