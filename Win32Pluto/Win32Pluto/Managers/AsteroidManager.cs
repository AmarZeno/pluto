using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class AsteroidManager
    {
        List<Asteroid> asteroidCollection = new List<Asteroid>();

        public void Add(Asteroid asteroid) {
            asteroidCollection.Add(asteroid);
        }

        public void Update(GameTime gameTime, GraphicsDevice graphicsDevice, SunManager sunManager, ScoreManager scoreManager, PlanetManager planetManager) {
            foreach (Asteroid asteroid in asteroidCollection) {
                asteroid.Update(gameTime, graphicsDevice, sunManager);
                CheckAsteroidCollision(sunManager, graphicsDevice.Viewport, asteroid, scoreManager, planetManager);
            }
        }

        public void Draw(SpriteBatch spriteBatch, SunManager sunManager) {
            foreach (Asteroid asteroid in asteroidCollection) {
                asteroid.Draw(spriteBatch, sunManager);
            }
        }

        public void Dispose() {
            foreach (Asteroid asteroid in asteroidCollection) {
                asteroid.sprite.texture.Dispose();
            }
        }

        public void CheckAsteroidCollision(SunManager sunManager, Viewport viewport, Asteroid asteroid, ScoreManager scoreManager, PlanetManager planetManager)
        {
            bool didAsteroidCollideTheSun = asteroid.GetCircle().Intersects(sunManager.GetFirstObjectCircle());
            if (didAsteroidCollideTheSun)
            {
                ResetAndRandomlyGenerateAsteroid(viewport, asteroid);
                scoreManager.DecreaseSunHealth();
            }
            // Planet collision logic
            bool didAsteroidCollideThePlanets = planetManager.CheckPlanetCollision(asteroid, scoreManager);
            if (didAsteroidCollideThePlanets) {
                ResetAndRandomlyGenerateAsteroid(viewport, asteroid);
            }
        }

        public void ResetAndRandomlyGenerateAsteroid(Viewport viewport, Asteroid asteroid)
        {
            Random r = new Random();
            int randomValue = r.Next(0, 360);
            var angle = randomValue;
            int radius = Math.Max(viewport.Width / 2, viewport.Height / 2);
            radius = radius + 500;
            asteroid.sprite.position = new Vector2((float)(Math.Cos(angle) * radius), (float)(Math.Sin(angle) * radius));
            asteroid.sprite.rotation = 0f;
        }
    }
}
