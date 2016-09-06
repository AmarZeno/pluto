using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Utilities;

namespace Win32Pluto.Models
{
    class Planet
    {
        public String name { set; get; }
        public Sprite sprite { set; get; }
        public Vector2 screenCenter { set; get; }
        public int radius { set; get; }
        public float angle { set; get; }

        public Planet() {
            sprite = new Sprite();
        }

        public void Update(Viewport viewport) {
            if (name == "Mercury")
            {
                angle = angle + 0.02f;
            }
            else if (name == "Venus")
            {
                angle = angle + 0.01f;
            }
            else if (name == "Earth") {
                angle = angle + 0.005f;
            }
            screenCenter = new Vector2(viewport.Width/2, viewport.Height/2);
            sprite.position = CommonUtilities.RotateAboutOrigin(screenCenter, angle, radius);
            sprite.rotation = sprite.rotation + 0.1f;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite.texture, sprite.position, color: Color.White, scale: sprite.scale, rotation: sprite.rotation, origin: sprite.origin);
        }

    }
}
