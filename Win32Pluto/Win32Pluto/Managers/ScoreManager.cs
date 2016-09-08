using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        public void Update(GameTime gameTime, SunManager sunManager, PlanetManager planetManager) {
            CheckGameOver(sunManager, gameTime);
            checkMouseActions(sunManager, planetManager);
        }

        public void Draw(SpriteBatch spriteBatch) {
            foreach (Score score in scoreCollection) {

                if (score.type == "GameOver" || score.type == "PlayAgain")
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

                // COme here
                gameOverElapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
                while (gameOverElapsedTime > 0.05)
                {
                    if (score.scale.X < 1.0f && score.scale.Y < 1.0f && score.type == "GameOver")
                    {
                        score.scale += new Vector2(0.03f, 0.03f);
                        score.position = new Vector2(score.position.X - (25 * score.scale.X), score.position.Y);
                    }
                    else if (score.scale.X < 0.5f && score.scale.Y < 0.5f && score.type == "PlayAgain")
                    {
                        score.scale += new Vector2(0.015f, 0.015f);
                        score.position = new Vector2(score.position.X - (25 * score.scale.X), score.position.Y);
                    }
                    gameOverElapsedTime = 0;
                }



                string sunState = sunManager.getState();
                if (score.type == "GameOver" && sunState == "Dead") {
                    
                }
            }
        }

        public void checkMouseActions(SunManager sunManager, PlanetManager planetManager)
        {
          //  if (sunManager.getState().Equals("Dead"))
          //  {
                var mouseState = Mouse.GetState();
                var mousePosition = new Point(mouseState.X, mouseState.Y);
                foreach (Score score in scoreCollection)
                {
                    if (score.type == "PlayAgain")
                    {
                        if (score.GetRect().Contains(mousePosition))
                        {
                            // Hover
                            score.texture = score.hoverTexture;
                            // Left click
                            if (mouseState.LeftButton == ButtonState.Pressed)
                            {
                                Console.WriteLine("Left Button Pressed");
                                RestartGame(sunManager, planetManager);
                                // gameState = GameState.Gameplay;
                            }
                        }
                        else
                        {
                            score.texture = score.defaultTexture;
                        }
                    }
                }
           // }
        }

        public void ResetScore() {
            foreach (Score score in scoreCollection) {
                if (score.type == "MainScore")
                {
                    score.value = 0;
                }
                else if (score.type == "SunHealth") {
                    score.value = 0;
                }
            }
        }

        public void RestartGame(SunManager sunManager, PlanetManager planetManager) {
            this.ResetScore();
            sunManager.ResetSunStates();
            planetManager.ResetPlanetStates();
        }
    }
}
