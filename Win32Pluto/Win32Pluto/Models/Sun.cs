using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Extensions;

namespace Win32Pluto.Models
{
    class Sun
    {
        public Sprite sprite { set; get; }

        // For spritesheet animations
        public int frameIndexX = 1;
        public int frameIndexY = 1;
        public int totalFramesX = 6;
        public int totalFramesY = 3;
        public float elapsedTime;
        public Vector2 scalingFactor;
        // duration of time to show each frame
        float frameTime = 0.18f;

        public Sun()
        {
            sprite = new Sprite();
        }

        public void Update(GameTime gameTime) {
            sprite.rotation = sprite.rotation + 0.01f;
            CalculatePlasmaBallRectangle(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite.texture, sprite.position, sourceRectangle: sprite.rectangle, color: Color.White, scale: sprite.scale, rotation: sprite.rotation, origin: sprite.origin);
        }

        public Rectangle GetRect() {
            return new Rectangle(Convert.ToInt32(sprite.position.X - (sprite.texture.Width * sprite.scale.X)/2), Convert.ToInt32(sprite.position.Y - (sprite.texture.Height * sprite.scale.Y)/2), Convert.ToInt32(sprite.texture.Width * sprite.scale.X), Convert.ToInt32(sprite.texture.Height * sprite.scale.Y));
        }

        public Circle GetCircle() {
            return new Circle(new Vector2(sprite.position.X, sprite.position.Y), ((sprite.texture.Width * sprite.scale.X)/ totalFramesX)/2);
        }

        public void CalculatePlasmaBallRectangle(GameTime gameTime)
        {
            int requiredFrameWidth = (int)(sprite.texture.Width) / totalFramesX;

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

            if (frameIndexY > totalFramesY)
            {
                frameIndexY = 1;
            }

            sprite.rectangle = new Rectangle(frameIndexX * requiredFrameWidth, 0, requiredFrameWidth, (int)(sprite.texture.Height) / totalFramesY);
        }
    }
}
