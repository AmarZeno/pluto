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
    class SunManager
    {
        List<Sun> sunCollection = new List<Sun>();

        public void Add(Sun sun) {
            sunCollection.Add(sun);
        }

        public void Update() {
            foreach (Sun sun in sunCollection) {
                sun.Update();
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
    }
}
