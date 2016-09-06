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
                spriteBatch.DrawString(score.font, score.text + score.sunHealth.ToString(), score.position, score.color);
            }
        }

        public void DecreaseSunHealth() {
            foreach (Score score in scoreCollection) {
                if (score.type == "SunHealth") {
                    if (score.sunHealth == 0)
                        return;
                    score.sunHealth -= 20;
                }
            }
        }
    }
}
