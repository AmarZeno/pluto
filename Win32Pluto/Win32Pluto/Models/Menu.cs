using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32Pluto.Models
{
    enum MenuType
    {
        StartMenu,
        ExitMenu,
    }
    class Menu
    {
        public Sprite sprite { set; get; }
        public Texture2D defaultTexture { set; get; }
        public Texture2D hoverTexture { set; get; }

        public MenuType type { set; get; }

        public Menu() {
            sprite = new Sprite();
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite.texture, sprite.position, color: Color.White);
        }

        public Rectangle GetRect() {
            return new Rectangle((int)sprite.position.X ,(int)sprite.position.Y , sprite.texture.Width, sprite.texture.Height);
        }
    }
}
