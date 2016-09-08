using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Extensions;
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

        // For spritesheet animations
        public int frameIndexX = 1;
        public int frameIndexY = 1;
        public int totalFramesX = 13;
        public int totalFramesY = 4;
        public float elapsedTime;
        public Vector2 scalingFactor;
        // duration of time to show each frame
        float frameTime = 0.02f;

        public Planet() {
            sprite = new Sprite();
        }

        public void Update(Viewport viewport, GameTime gameTime) {
            if (name == "Earth")
            {
                angle = angle + 0.008f;
            }
            else if (name == "Saturn")
            {
                angle = angle + 0.005f;
            }
            else if (name == "Pluto") {
                angle = angle + 0.003f;
            }
            screenCenter = new Vector2(viewport.Width/2, viewport.Height/2);
            sprite.position = CommonUtilities.RotateAboutOrigin(screenCenter, angle, radius);
            sprite.rotation = sprite.rotation + 0.01f;
            CalculatePlasmaBallRectangle(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite.texture, sprite.position, sourceRectangle: sprite.rectangle, color: Color.White, scale: sprite.scale, rotation: sprite.rotation, origin: sprite.origin);
        }

        public Circle GetCircle() {
            Circle circle = new Circle(sprite.position, (sprite.texture.Width * sprite.scale.X)/2);
            return circle;
        }

        public Rectangle GetRect()
        {
            Rectangle rectangle = new Rectangle((int)sprite.position.X - (int)((sprite.texture.Width * sprite.scale.X)/13)/2, (int)sprite.position.Y - (int)((sprite.texture.Height * sprite.scale.Y)/4)/2, (int)((sprite.texture.Width * sprite.scale.X)/13), (int)((sprite.texture.Height * sprite.scale.Y)/4));
            return rectangle;
        }

        public void CalculatePlasmaBallRectangle(GameTime gameTime)
        {
            int requiredFrameWidth = (int)(sprite.texture.Width ) / totalFramesX;

            // Process elapsed time
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (elapsedTime > frameTime)
            {
                frameIndexX++;
                elapsedTime = 0f;
                if (frameIndexX > totalFramesX)
                {
                    frameIndexY++;
                }
            }

            // Reset the frames to the initial position
            if (frameIndexX >= totalFramesX)
            {
                frameIndexX = 1;
            }

            if (frameIndexY > totalFramesY) {
                frameIndexY = 1;
            }

            sprite.rectangle = new Rectangle(frameIndexX * requiredFrameWidth, 0, requiredFrameWidth, (int)(sprite.texture.Height )/4);
        }

    }
}
