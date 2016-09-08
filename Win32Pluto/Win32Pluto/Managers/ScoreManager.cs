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
    class ScoreManager
    {
        List<Score> scoreCollection = new List<Score>();
        float gameOverElapsedTime = 0;

        public void Add(Score score) {
            scoreCollection.Add(score);
        }

        public void Update(GameTime gameTime, SunManager sunManager) {
            CheckGameOver(sunManager, gameTime);
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Score score in scoreCollection) {

                if (score.type == "GameOver")
                {
                    spriteBatch.Draw(score.texture, score.position, color: Color.White, scale: score.scale);
                }
                else {
                    spriteBatch.DrawString(score.font, score.text + score.value.ToString(), score.position, score.color);
                }
                
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

        public void CheckGameOver(SunManager sunManager, GameTime gameTime) {
            foreach (Score score in scoreCollection) {
                string sunState = sunManager.getState();
                if (score.type == "GameOver" && sunState == "Dead") {
                    gameOverElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    while (gameOverElapsedTime > 0.1)
                    {
                        if (score.scale.X < 1.0f && score.scale.Y < 1.0f)
                        {
                            score.scale += new Vector2(0.03f, 0.03f);
                            score.position = new Vector2(score.position.X - (25 * score.scale.X), score.position.Y);
                        }
                        gameOverElapsedTime = 0;
                    }
                }
            }
        }
    }
}
