using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class AsteroidManager
    {
        List<Asteroid> asteroidCollection = new List<Asteroid>();

        public void Add(Asteroid asteroid) {
            asteroidCollection.Add(asteroid);
        }

        public void Update(GameTime gameTime, Viewport viewport, SunManager sunManager) {
            foreach (Asteroid asteroid in asteroidCollection) {
                asteroid.Update(gameTime, viewport, sunManager);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Asteroid asteroid in asteroidCollection) {
                asteroid.Draw(spriteBatch);
            }
        }

        public void Dispose() {
            foreach (Asteroid asteroid in asteroidCollection) {
                asteroid.sprite.texture.Dispose();
            }
        }
    }
}
