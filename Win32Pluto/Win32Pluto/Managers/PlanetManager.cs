using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class PlanetManager
    {
        List<Planet> planetCollection = new List<Planet>();

        public void Add(Planet planet) {
            planetCollection.Add(planet);
        }

        public void Update(Viewport viewport) {
            foreach (Planet planet in planetCollection) {
                planet.Update(viewport);
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Planet planet in planetCollection)
            {
                planet.Draw(spriteBatch);
            }
        }

        public void Dispose() {
            foreach (Planet planet in planetCollection) {
                planet.sprite.texture.Dispose();
            }
        }

        public void accelerateForward(Orbit orbit) {
            foreach (Planet planet in planetCollection) {
                if (planet.name == "Mercury" && orbit.name == "MercuryOrbit") {
                    planet.angle += 0.05f;
                } else if (planet.name == "Venus" && orbit.name == "VenusOrbit") {
                    planet.angle += 0.05f;
                } else if (planet.name == "Earth" && orbit.name == "EarthOrbit") {
                    planet.angle += 0.05f;
                }
            }
        }

        public void accelerateBackward(Orbit orbit) {
            foreach (Planet planet in planetCollection)
            {
                if (planet.name == "Mercury" && orbit.name == "MercuryOrbit")
                {
                    planet.angle -= 0.05f;
                }
                else if (planet.name == "Venus" && orbit.name == "VenusOrbit")
                {
                    planet.angle -= 0.05f;
                }
                else if (planet.name == "Earth" && orbit.name == "EarthOrbit")
                {
                    planet.angle -= 0.05f;
                }
            }
        }
    }
}
