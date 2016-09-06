using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class SpaceManager
    {
        List<Space> spaceCollection = new List<Space>();

        public void Add(Space space) {
            spaceCollection.Add(space);
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Space space in spaceCollection) {
                space.Draw(spriteBatch);
            }
        }

        public void Dispose() {
            foreach (Space space in spaceCollection) {
                space.sprite.texture.Dispose();
            }
        }
    }
}
