using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32Pluto.Models
{
    class Space
    {
        public Sprite sprite { set; get; }

        public Space() {
            sprite = new Sprite();
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite.texture, sprite.position, color: Color.White, rotation: sprite.rotation, scale: sprite.scale);
        }
    }
}
