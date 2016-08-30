using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Pluto.Custom_Class;
namespace Pluto
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D sunTexture;
        float sunCircularRotationOffset;
        Texture2D planetTexture;
        float rotationAngleInRadians;
        float rotationAngleInRadiansForMercury;
        float rotationAngleInRadiansForJupiter;
        float rotationAngleInRadiansForPluto;
        Vector2 mercuryPosition;
        Vector2 venusPosition;
        Vector2 earthPosition;
        Vector2 marsPosition;
        Vector2 jupiterPosition;
        Vector2 saturnPosition;
        Vector2 uranusPosition;
        Vector2 neptunePosition;
        Vector2 plutoPosition;
        int screenWidthCenter;
        int screenHeightCenter;
        float distanceFromCenter;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            initializeVariables();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            sunTexture = new Texture2D(graphics.GraphicsDevice, 50, 50);
           // planetTexture = new Texture2D(graphics.GraphicsDevice, 10, 10);
            planetTexture = Content.Load<Texture2D>("Pluto");
          
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            sunTexture.Dispose();
            planetTexture.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            customUpdate();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            customDraw();
   
            base.Draw(gameTime);
        }

        #region Game1 CustomAccessors
        public void initializeVariables() {
            sunCircularRotationOffset = 0;
            rotationAngleInRadians = 0;
            rotationAngleInRadiansForMercury = 0;
            rotationAngleInRadiansForJupiter = 0;
            rotationAngleInRadiansForPluto = 0;
            mercuryPosition = new Vector2();
        }

        #endregion

        #region Game1 CustomUpdate
        public void customUpdate()
        {
            MouseState state = Mouse.GetState();

            Color[] sunColorData = new Color[50 * 50];
            for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Chocolate;
            sunTexture.SetData(sunColorData);

            //Color[] planetColorData = new Color[10 * 10];
            //for (int i = 0; i < planetColorData.Length; ++i) planetColorData[i] = Color.SaddleBrown;
            //planetTexture.SetData(planetColorData);
            // Vector2 coor = new Vector2(10, 20);

            sunCircularRotationOffset = sunCircularRotationOffset + 0.01f;
            screenWidthCenter = GraphicsDevice.Viewport.Width / 2;
            screenHeightCenter = GraphicsDevice.Viewport.Height / 2;

            rotationAngleInRadians = rotationAngleInRadians + 0.01f;
            rotationAngleInRadiansForMercury = rotationAngleInRadiansForMercury + 0.1f;
            rotationAngleInRadiansForJupiter = rotationAngleInRadiansForJupiter + 0.01f;

            distanceFromCenter = Math.Max(sunTexture.Width, sunTexture.Height) + 10;

            Vector2 screenCenter = new Vector2(screenWidthCenter, screenHeightCenter);
            mercuryPosition = screenCenter + Vector2.Transform(new Vector2(distanceFromCenter, 0),
                                                          Matrix.CreateRotationZ(rotationAngleInRadiansForMercury));
            venusPosition = screenCenter + Vector2.Transform(new Vector2(distanceFromCenter + 20, 0),
                                                          Matrix.CreateRotationZ(rotationAngleInRadians + 0.5f));
            earthPosition = screenCenter + Vector2.Transform(new Vector2(distanceFromCenter + 40, 0),
                                                          Matrix.CreateRotationZ(rotationAngleInRadians + 0.1f));
            marsPosition = screenCenter + Vector2.Transform(new Vector2(distanceFromCenter + 60, 0),
                                                          Matrix.CreateRotationZ(rotationAngleInRadians + 0.9f));
            jupiterPosition = screenCenter + Vector2.Transform(new Vector2(distanceFromCenter + 80, 0),
                                              Matrix.CreateRotationZ(rotationAngleInRadiansForJupiter + 0.1f));
            saturnPosition = screenCenter + Vector2.Transform(new Vector2(distanceFromCenter + 100, 0),
                                              Matrix.CreateRotationZ(rotationAngleInRadiansForJupiter + 0.1f));
            uranusPosition = screenCenter + Vector2.Transform(new Vector2(distanceFromCenter + 120, 0),
                                              Matrix.CreateRotationZ(rotationAngleInRadiansForJupiter + 0.1f));
            neptunePosition = screenCenter + Vector2.Transform(new Vector2(distanceFromCenter + 140, 0),
                                              Matrix.CreateRotationZ(rotationAngleInRadiansForJupiter + 0.1f));
            plutoPosition = screenCenter + Vector2.Transform(new Vector2(distanceFromCenter + 160, 0),
                                              Matrix.CreateRotationZ(rotationAngleInRadiansForPluto + 0.1f));


            var destination = new Rectangle(100, 100, 500, 500);
            var destination2 = new Circle(screenCenter, distanceFromCenter + 80);
            bool mouseOver = destination2.Contains(new Vector2(state.X, state.Y));
            if (mouseOver)
            {
                Color[] newSunColorData = new Color[50 * 50];
                for (int i = 0; i < sunColorData.Length; ++i) sunColorData[i] = Color.Red;
                sunTexture.SetData(sunColorData);
                if (state.LeftButton == ButtonState.Pressed)
                {
                    // Do cool stuff here
                    rotationAngleInRadiansForPluto = rotationAngleInRadiansForPluto + 0.1f;
                }
                else {
                    rotationAngleInRadiansForPluto = rotationAngleInRadiansForPluto + 0.01f;
                }
                if (state.RightButton == ButtonState.Pressed)
                {
                    rotationAngleInRadiansForPluto = rotationAngleInRadiansForPluto + 0.001f;
                }
                else {
                    rotationAngleInRadiansForPluto = rotationAngleInRadiansForPluto + 0.01f;
                }
            }
            else
            {
                rotationAngleInRadiansForPluto = rotationAngleInRadiansForPluto + 0.01f;
            }
        }
        #endregion

        #region Game1 CustomDraw
        public void customDraw() {
            spriteBatch.Begin();

            spriteBatch.Draw(sunTexture, new Vector2(screenWidthCenter, screenHeightCenter), rotation: sunCircularRotationOffset, origin: new Vector2(sunTexture.Width / 2, sunTexture.Height / 2));

            int mercurySize = 50;
            spriteBatch.Draw(planetTexture, destinationRectangle:new Rectangle(Convert.ToInt32(mercuryPosition.X - mercurySize / 2) , Convert.ToInt32(mercuryPosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(venusPosition.X - mercurySize / 2), Convert.ToInt32(venusPosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(earthPosition.X - mercurySize / 2), Convert.ToInt32(earthPosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(marsPosition.X - mercurySize / 2), Convert.ToInt32(marsPosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(jupiterPosition.X - mercurySize / 2), Convert.ToInt32(jupiterPosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(saturnPosition.X - mercurySize / 2), Convert.ToInt32(saturnPosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(uranusPosition.X - mercurySize / 2), Convert.ToInt32(uranusPosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(neptunePosition.X - mercurySize / 2), Convert.ToInt32(neptunePosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.Draw(planetTexture, destinationRectangle: new Rectangle(Convert.ToInt32(plutoPosition.X - mercurySize / 2), Convert.ToInt32(plutoPosition.Y - mercurySize / 2), mercurySize, mercurySize));

            spriteBatch.End();
        }
        #endregion
    }
}
