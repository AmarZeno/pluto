using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Extensions;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class SunManager
    {
        List<Sun> sunCollection = new List<Sun>();

        public void Add(Sun sun) {
            sunCollection.Add(sun);
        }

        public void Update(GameTime gameTime, ScoreManager scoreManager) {
            foreach (Sun sun in sunCollection) {
                sun.Update(gameTime);
                UpdateSunState(sun, scoreManager);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Sun sun in sunCollection) {
                sun.Draw(spriteBatch);
            }
        }

        public void Dispose() {
            foreach (Sun sun in sunCollection) {
                sun.sprite.texture.Dispose();
            }
        }

        public Rectangle GetFirstObjectRect() {
            return sunCollection.First<Sun>().GetRect();
        }

        public Circle GetFirstObjectCircle() {
            return sunCollection.First<Sun>().GetCircle();
        }

        public void UpdateSunState(Sun sun, ScoreManager scoreManager) {
            int coreHealth = scoreManager.GetCoreHealth();
            if (coreHealth == 80)
            {
                sun.sprite.texture = sun.starTextureState2;
            }
            else if (coreHealth == 60)
            {
                sun.sprite.texture = sun.starTextureState3;
            }
            else if (coreHealth == 40)
            {
                sun.sprite.texture = sun.starTextureState4;
            }
            else if (coreHealth == 20) {
                sun.sprite.texture = sun.starTextureState5;
            } else if (coreHealth == 0) {
                sun.state = "Dead";
                sun.sprite.texture = sun.starTextureState6;
            }
        }
    }
}
