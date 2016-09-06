using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Helpers;
using Win32Pluto.Managers;

namespace Win32Pluto.Models
{
    class Asteroid
    {
        public Sprite sprite;
        public int frameIndex = 1;
        public int totalFrames = 29;
        public float elapsedTime;
        // duration of time to show each frame
        float frameTime = 0.02f;
        public Vector2 scalingFactor;

        public Asteroid() {
            sprite = new Sprite();
            scalingFactor = new Vector2();
        }

        public void Update(GameTime gameTime, Viewport viewport, SunManager sunManager) {
            CalculateAsteroidRectangle(gameTime);
            CalculateAsteroidPosition(gameTime, viewport);
            CheckAsteroidCollision(sunManager, viewport);
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(sprite.texture, sprite.position, sourceRectangle: sprite.rectangle, color: Color.White, rotation: sprite.rotation, origin: sprite.origin, scale: scalingFactor);
        }

        public Rectangle GetRect() {
            Rectangle asteroidRectangle = new Rectangle((int)sprite.position.X, (int)sprite.position.Y, sprite.rectangle.Width, sprite.rectangle.Height);
            return asteroidRectangle;
        }

        public void CalculateAsteroidRectangle(GameTime gameTime) {
            int requiredFrameWidth = sprite.texture.Width / totalFrames;
            float newSpriteWidth = requiredFrameWidth * totalFrames;
            float oldSpriteWidth = sprite.texture.Width;
            float scaleOffset = newSpriteWidth / oldSpriteWidth;
            scalingFactor = new Vector2(scaleOffset, scaleOffset);

            // Process elapsed time
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (elapsedTime > frameTime) {
                frameIndex++;
                elapsedTime = 0f;
            }

            // Reset the frames to the initial position
            if (frameIndex > totalFrames) {
                frameIndex = 1;
            }
            
            sprite.rectangle = new Rectangle(frameIndex * requiredFrameWidth, 0, requiredFrameWidth, (int)(sprite.texture.Height * scaleOffset));
        }

        public void CalculateAsteroidPosition(GameTime gameTime, Viewport viewport) {
            float asteroidSpeed = 5f;
            Vector2 targetDestination = new Vector2(viewport.Width/2, viewport.Height/2);
          //  sprite.rotation = (float)DirectionHelper.FaceObject(sprite.position, targetDestination);
            sprite.position += DirectionHelper.MoveTowards(sprite.position, targetDestination, asteroidSpeed);


            Vector2 direction = targetDestination - sprite.position;
            float rotation = (float)Math.Atan2(direction.Y, direction.X);
            sprite.rotation = (float)(rotation + (Math.PI * 0.5f));
            //double direction = (float)(Math.Atan2(target.Y - position.Y, target.X - position.X) * 180 / Math.PI);
        }

        public void CheckAsteroidCollision(SunManager sunManager, Viewport viewport) {
            bool didAsteroidCollideTheSun = this.GetRect().Intersects(sunManager.GetFirstObjectRect());
            if (didAsteroidCollideTheSun) {
                ResetAndRandomlyGenerateAsteroid(viewport);
            }
        }

        public void ResetAndRandomlyGenerateAsteroid(Viewport viewport) {
            Random r = new Random();
            int randomValue = r.Next(0, 360);
            var angle = randomValue;
            int radius = Math.Max(viewport.Width/2, viewport.Height/2);
            radius = radius + 500;
            sprite.position = new Vector2((float)(Math.Cos(angle)*radius), (float)(Math.Sin(angle)*radius));
            sprite.rotation = 0f;
        }
    }
}
