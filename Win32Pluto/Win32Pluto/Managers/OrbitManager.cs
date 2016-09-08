using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Extensions;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
   
    class OrbitManager
    {
        List<Orbit> orbitCollection = new List<Orbit>();
        bool hoverSoundPlayed = false;
        public void Add(Orbit orbit) {
            orbitCollection.Add(orbit);
        }

        public void Update(GraphicsDevice graphicsDevice, PlanetManager planetManager, AudioManager audioManager) {

            MouseState state = Mouse.GetState();
            Vector2 screenCenter = new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2);
            foreach (Orbit orbit in orbitCollection) {
                var planetDestination = new Circle(screenCenter, orbit.radius + 20);
                bool planetMouseOver = planetDestination.Contains(new Vector2(state.X, state.Y));
                int nextPlanetOffset = 70;
                var nextPlanetDestination = new Circle(screenCenter, orbit.radius - nextPlanetOffset);
                bool nextPlanetMouseOver = nextPlanetDestination.Contains(new Vector2(state.X, state.Y));

                if (planetMouseOver)
                {
                    if (hoverSoundPlayed == false)
                    {
                       // audioManager.playOrbitHoverSound();
                        hoverSoundPlayed = true;
                    }
                }
                else {
                    hoverSoundPlayed = false;
                }

                if (planetMouseOver && !nextPlanetMouseOver)
                {
                    orbit.sprite.texture = orbit.selectedTexture;
                    if (state.LeftButton == ButtonState.Pressed)
                    {
                        planetManager.accelerateForward(orbit);
                    }
                    else if (state.RightButton == ButtonState.Pressed) {
                        planetManager.accelerateBackward(orbit);
                    }
                }
                else {
                    orbit.sprite.texture = orbit.defaultTexture;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Orbit orbit in orbitCollection) {
                orbit.Draw(spriteBatch);
            }
        }
    }
}
