using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32Pluto.Models
{
    class Orbit
    {
        public String name { set; get; }
        public Sprite sprite { set; get; }
        // Used to determine the mouse hover effect. Property is maintained like planet radius
        public int radius { set; get; }
        public Texture2D defaultTexture { set; get; }
        public Texture2D selectedTexture { set; get; }

        public Orbit() {
            sprite = new Sprite();
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite.texture, sprite.position, color: Color.White, scale: sprite.scale);
        }
    }
}
