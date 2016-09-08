using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32Pluto.Models
{
    class Score
    {
        public SpriteFont font { set; get; }
        public int value { set; get; }
        public Vector2 position { set; get; }
        public Color color { set; get; }
        public String text { set; get; }
        public String type { set; get; }

        public Texture2D texture { set; get; }
        public Texture2D defaultTexture { set; get; }
        public Texture2D hoverTexture { set; get; }
        public Vector2 scale { set; get; }

        public Vector2 origin;

        public int GetScoreValue() {
            return value;
        }

        public Rectangle GetRect() {
            return new Rectangle((int)(position.X), (int)(position.Y - (texture.Height * scale.Y)/2), (int)(texture.Width * scale.X), texture.Height);
        }
    }
}
