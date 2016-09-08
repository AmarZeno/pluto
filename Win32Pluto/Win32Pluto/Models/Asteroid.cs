using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Extensions;
using Win32Pluto.Helpers;
using Win32Pluto.Managers;

namespace Win32Pluto.Models
{
    class Asteroid
    {
        public Sprite sprite;
        public string type { set; get; }
        public int frameIndex = 1;
        public int totalFrames = 29;
        public float elapsedTime;
        // duration of time to show each frame
        float frameTime = 0.02f;
        public Vector2 scalingFactor;
        public Vector2 targetPosition;
        // Test
        Texture2D sunTexture;
        Texture2D asteroidTexture;

        public Asteroid( ) {
            sprite = new Sprite();
            scalingFactor = new Vector2();
            targetPosition = new Vector2();
        }

        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice, SunManager sunManager) {
            CalculateAsteroidRectangle(gameTime);
            CalculateAsteroidPosition(gameTime, graphicsDevice.Viewport);

            // Test
            //sunTexture = new Texture2D(graphicsDevice, sunManager.GetFirstObjectRect().Width, sunManager.GetFirstObjectRect().Height);
            //Color[] sunColorData = new Color[sunManager.GetFirstObjectRect().Width * sunManager.GetFirstObjectRect().Height];
            //for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Chocolate;
            //sunTexture.SetData(sunColorData);

            //asteroidTexture = new Texture2D(graphicsDevice, this.GetRect().Width, this.GetRect().Height);
            //asteroidTexture.SetData(sunColorData);
        }

        public void Draw(SpriteBatch spriteBatch, SunManager sunManager) {
            spriteBatch.Draw(sprite.texture, sprite.position, sourceRectangle: sprite.rectangle, color: Color.White, rotation: sprite.rotation, origin: sprite.origin, scale: scalingFactor);
            //test
            // spriteBatch.Draw(sunTexture, sunManager.GetFirstObjectRect(), color: Color.White);
          //  spriteBatch.Draw(asteroidTexture, this.GetRect(), color: Color.White);
        }

        public Rectangle GetRect() {
            Rectangle asteroidRectangle = new Rectangle((int)(sprite.position.X - sprite.rectangle.Width/2), (int)(sprite.position.Y - sprite.rectangle.Height/2), sprite.rectangle.Width, sprite.rectangle.Height);
            return asteroidRectangle;
        }

        public Circle GetCircle() {
            Circle circle = new Circle(new Vector2(sprite.position.X, sprite.position.Y), Math.Max((sprite.texture.Width/29) / 2, sprite.texture.Height));
            return circle;
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

        public void CalculateAsteroidPosition(GameTime gameTime, Viewport viewport)
        {
            float asteroidSpeed = 5f;
            Vector2 targetDestination = new Vector2();
            if (this.type == "RedMeteor") {
                targetDestination = new Vector2(viewport.Width / 2, viewport.Height / 2);
                sprite.rotation = (float)DirectionHelper.FaceObject(sprite.position, targetDestination);
                sprite.position += DirectionHelper.MoveTowards(sprite.position, targetDestination, asteroidSpeed);
            }
            else if (this.type == "BlueMeteor") {
                // Randomly calculate the point the meteor should head
                //Random r = new Random();
                //int randomValue = r.Next(0, 360);
                //var angle = randomValue;
                //int radius = Math.Max(viewport.Width / 2, viewport.Height / 2);
                //radius = radius + 500;
                //targetDestination = new Vector2((float)(Math.Cos(angle) * radius), (float)(Math.Sin(angle) * radius));
                sprite.rotation = (float)DirectionHelper.FaceObject(sprite.position, this.targetPosition);
                sprite.position += DirectionHelper.MoveTowards(sprite.position, this.targetPosition, asteroidSpeed);
            }
            
        }
    }
}
