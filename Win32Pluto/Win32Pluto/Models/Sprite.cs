using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32Pluto.Models
{
    class Sprite
    {
        public Texture2D texture { set; get; }
        public Vector2 position { set; get; }
        public Rectangle rectangle { set; get; }
        public Vector2 scale { set; get; }
        public Vector2 origin { set; get; }
        public float rotation { set; get; }
    }
}
