using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32Pluto.Models
{
    class Sun
    {
        public Sprite sprite { set; get; }

        public Sun()
        {
            sprite = new Sprite();
        }

        public void Update() {
            sprite.rotation = sprite.rotation + 0.01f;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite.texture, sprite.position, color: Color.White, scale: sprite.scale, rotation: sprite.rotation, origin: sprite.origin);
        }

        public Rectangle GetRect() {
            return new Rectangle(Convert.ToInt32(sprite.position.X - (sprite.texture.Width * sprite.scale.X)/2), Convert.ToInt32(sprite.position.Y - (sprite.texture.Height * sprite.scale.Y)/2), Convert.ToInt32(sprite.texture.Width * sprite.scale.X), Convert.ToInt32(sprite.texture.Height * sprite.scale.Y));
        }
    }
}
