using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Win32Pluto.Models;

namespace Win32Pluto.Managers
{
    class ScoreManager
    {
        List<Score> scoreCollection = new List<Score>();

        public void Add(Score score) {
            scoreCollection.Add(score);
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Score score in scoreCollection) {
                spriteBatch.DrawString(score.font, score.text + score.value.ToString(), score.position, score.color);
            }
        }

        public void DecreaseSunHealth() {
            foreach (Score score in scoreCollection) {
                if (score.type == "SunHealth") {
                    if (score.value == 0)
                        return;
                    score.value -= 10;
                }
            }
        }

        public void IncreaseScore(Planet planet) {
            int value = 0;
            if (planet.name == "Earth") {
                value = 25;
            }
            else if (planet.name == "Saturn") {
                value = 50;
            }
            else if (planet.name == "Pluto") {
                value = 100;
            }

            foreach (Score score in scoreCollection) {
                if (score.type == "MainScore") {
                    score.value += value;
                }
            }
        }

        public int GetCoreHealth() {
            foreach (Score score in scoreCollection) {
                if (score.type == "SunHealth") {
                    return score.GetScoreValue();
                }
            }
            return 0;
        }
    }
}
