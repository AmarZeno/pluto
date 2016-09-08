using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class MenuManager
    {
        List<Menu> menuCollection = new List<Menu>();

        public void Add(Menu menu) {
            menuCollection.Add(menu);
        }

        public void Update(ref GameState gameState, Game1 game) {
            checkMouseActions(ref gameState, game);
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Menu menu in menuCollection) {
                menu.Draw(spriteBatch);
            }
        }

        public void Dispose()
        {
            foreach (Menu menu in menuCollection)
            {
                menu.sprite.texture.Dispose();
            }
        }

        public void checkMouseActions(ref GameState gameState, Game1 game) {
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);
            foreach (Menu menu in menuCollection)
            {
                if (menu.GetRect().Contains(mousePosition))
                {
                    // Hover
                    menu.sprite.texture = menu.hoverTexture;
                    Console.WriteLine("Hovering");
                    // Left click
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        if (menu.type == MenuType.StartMenu)
                        {
                            Console.WriteLine("Left Button Pressed");
                            gameState = GameState.Gameplay;
                        }
                        else if (menu.type == MenuType.ExitMenu) {
                            game.Exit();
                        }
                    }
                }
                else {
                    menu.sprite.texture = menu.defaultTexture;
                }
            }
        }
    }
}
