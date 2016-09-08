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
        public Vector2 scale { set; get; }

        public int GetScoreValue() {
            return value;
        }
    }
}
