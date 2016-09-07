using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win32Pluto.Managers
{
    class DisplayManager
    {
        public void EnableFullScreen(GraphicsDeviceManager graphics) {
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.ApplyChanges();
        }

        public void ShowMouseCursor(Game1 game) {
            game.IsMouseVisible = true;
        }
    }
}
