using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Extensions;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class PlanetManager
    {
        List<Planet> planetCollection = new List<Planet>();

        //Test
        Texture2D planetTexture;


        public void Add(Planet planet) {
            planetCollection.Add(planet);
        }

        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime) {
            foreach (Planet planet in planetCollection) {
                planet.Update(graphicsDevice.Viewport, gameTime);

                // Test
                //Color[] sunColorData = new Color[planet.GetRect().Width * planet.GetRect().Height];
                //for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Chocolate;

                //planetTexture = new Texture2D(graphicsDevice, planet.GetRect().Width, planet.GetRect().Height);
                //planetTexture.SetData(sunColorData);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Planet planet in planetCollection)
            {
                planet.Draw(spriteBatch);
                //test
             //   spriteBatch.Draw(planetTexture, planet.GetRect(), Color.White);
            }
        }

        public void Dispose() {
            foreach (Planet planet in planetCollection) {
                planet.sprite.texture.Dispose();
            }
        }

        public void accelerateForward(Orbit orbit) {
            foreach (Planet planet in planetCollection) {
                if (planet.name == "Earth" && orbit.name == "EarthOrbit") {
                    planet.angle += 0.02f;
                } else if (planet.name == "Saturn" && orbit.name == "SaturnOrbit") {
                    planet.angle += 0.02f;
                } else if (planet.name == "Pluto" && orbit.name == "PlutoOrbit") {
                    planet.angle += 0.02f;
                }
            }
        }

        public void accelerateBackward(Orbit orbit) {
            foreach (Planet planet in planetCollection)
            {
                if (planet.name == "Earth" && orbit.name == "EarthOrbit")
                {
                    planet.angle -= 0.0017f;
                }
                else if (planet.name == "Saturn" && orbit.name == "SaturnOrbit")
                {
                    planet.angle -= 0.0017f;
                }
                else if (planet.name == "Pluto" && orbit.name == "PlutoOrbit")
                {
                    planet.angle -= 0.0017f;
                }
            }
        }

        public bool CheckPlanetCollision(Asteroid asteroid, ScoreManager scoreManager) {
            //Test
            foreach (Planet planet in planetCollection)
            {

                bool didAsteroidCollide = planet.GetRect().Intersects(asteroid.GetRect());
                if (didAsteroidCollide)
                {
                    if (asteroid.type == "RedMeteor")
                    {
                        scoreManager.IncreaseScore(planet);
                    }
                    else if (asteroid.type == "BlueMeteor") {
                        // Do nothing
                    }
                    return true;
                }

            }
            return false;

            foreach (Planet planet in planetCollection) {
                Circle planetCircle = planet.GetCircle();
                Circle asteroidCircle = asteroid.GetCircle();
                bool didAsteroidCollide = planet.GetCircle().Intersects(asteroid.GetCircle());
                if (didAsteroidCollide) {
                    return true;
                }
            }
            return false;
        }
    }
}
