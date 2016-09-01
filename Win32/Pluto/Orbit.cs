using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pluto
{
    class Orbit
    {
        public Texture2D defaultTexture {
            get {
                return defaultTexture;
            }
            set {
                defaultTexture = value;
            }
        }
        public Texture2D selectedTexture {
            get {
                return selectedTexture;
            }
            set {
                selectedTexture = value;
            }
        }
        public Vector2 position {
            get {
                return position;
            }
            set {
                position = value;
            }
        }
        public Vector2 origin {
            get {
                return origin;
            }
            set {
                origin = value;
            }
        }
        public Rectangle sourceFrame {
            get {
                return sourceFrame;
            }
            set {
                sourceFrame = value;
            }
        }
    }
}
